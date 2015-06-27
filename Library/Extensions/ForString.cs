using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace BitFn.Core.Extensions
{
	public static class ForString
	{
		/// <summary>
		///     Removes non-spacing marks from all characters, such as the accent in 'resumé'.
		/// </summary>
		/// <param name="s">The string to use.</param>
		/// <returns>A string with non-spacing marks removed.</returns>
		/// <seealso cref="UnicodeCategory.NonSpacingMark" />
		/// <exception cref="ArgumentNullException">The value of 'this' cannot be null. </exception>
		public static string RemoveDiacritics(this string s)
		{
			if (s == null) throw new ArgumentNullException(nameof(s));

			var d = s.Normalize(NormalizationForm.FormD);
			var sb = new StringBuilder(s.Length);
			// ReSharper disable LoopCanBePartlyConvertedToQuery
			foreach (var ch in d)
			{
				var uc = CharUnicodeInfo.GetUnicodeCategory(ch);
				if (uc != UnicodeCategory.NonSpacingMark)
					sb.Append(ch);
			}
			// ReSharper restore LoopCanBePartlyConvertedToQuery
			return (sb.ToString().Normalize(NormalizationForm.FormC));
		}

		/// <summary>
		///     Converts the string to a url-safe slug containing only alphanumerics and the dash character. Whitespace and
		///     punctuation are either ignored or replaced with dashes, while diacritics are removed and certain common Latin
		///     ligatures are replaced.
		/// </summary>
		/// <remarks>
		///     This method takes a very literal and technically incorrect approach to certain foreign characters, such as mu (µ)
		///     to u, thorn (þ) to p, etc. Any more-delicate handling of these characters should be done beforehand.
		/// </remarks>
		/// <param name="s">The string to use.</param>
		/// <param name="lowercase">Whether or not to convert all alpha characters to lowercase.</param>
		/// <param name="parenthetical">Whether or not to convert open and close punctuations to matched parentheses.</param>
		/// <param name="strict">Whether or not to fail when encountering unhandled characters.</param>
		/// <returns>A url-safe slug containing only alphanumerics and the dash character.</returns>
		/// <exception cref="ArgumentOutOfRangeException">String contains an unhandled letter or number character.</exception>
		/// <seealso cref="RemoveDiacritics" />
		/// <seealso cref="UnicodeCategory" />
		/// <exception cref="ArgumentNullException">The value of 'this' cannot be null. </exception>
		public static string ToSlug(this string s, bool lowercase = false,
			bool parenthetical = false, bool strict = false)
		{
			if (s == null) throw new ArgumentNullException(nameof(s));
			if (s.Length == 0) return string.Empty;

			// Normalize to FormD in order to remove diacritics.
			var d = s.Normalize(NormalizationForm.FormD);
			var sb = new StringBuilder(s.Length);

			var wordbreak = false;
			var skipbreak = false;
			var parenCount = 0;
			foreach (var ch in d)
			{
				string append;
				var skipnext = false;
				if ((ch >= 'A' && ch <= 'Z') || (ch >= 'a' && ch <= 'z') || (ch >= '0' && ch <= '9') || ch == '_')
				{
					// Character is an alphanumeric
					if (lowercase && (ch >= 'A' && ch <= 'Z'))
					{
						// Append the lowercase form of this character
						append = char.ToLower(ch).ToString();
					}
					else
					{
						// Append this character as-is
						append = ch.ToString();
					}
				}
				else if (char.IsWhiteSpace(ch) || char.IsSeparator(ch) ||
				         ch == '-' || ch == '–' || ch == '—' || ch == '/' || ch == '\\')
				{
					// Break on this character.
					// Catches control-characters and other punctuations that UnicodeCategory does not.
					// It also catches certain common characters to avoid dictionary and category lookups.
					wordbreak = true;
					continue;
				}
				else if (CharacterSlugs.TryGetValue(ch, out append))
				{
					// We have an (approximate) slug equivalent for this character.
					if (lowercase)
					{
						// We don't know if the value is lowercase or not, so we'll be safe.
						append = append.ToLowerInvariant();
					}
				}
				else
				{
					var uc = CharUnicodeInfo.GetUnicodeCategory(ch);
					switch (uc)
					{
						case UnicodeCategory.SpaceSeparator:
						case UnicodeCategory.DashPunctuation:
						case UnicodeCategory.ConnectorPunctuation:
							// Character causes word-breaks when between two alphanumeric characters.
							wordbreak = true;
							continue;
						case UnicodeCategory.OpenPunctuation:
							if (parenthetical)
							{
								// Append parenthese and skip next wordbreak
								append = "(";
								parenCount++;
								skipnext = true;
								break;
							}
							wordbreak = true;
							goto default;
						case UnicodeCategory.ClosePunctuation:
							if (parenthetical && parenCount > 0)
							{
								// Skip current wordbreak, append parenthese,
								append = ")";
								parenCount--;
								skipbreak = true;
								break;
							}
							wordbreak = true;
							goto default;
						case UnicodeCategory.MathSymbol:
							wordbreak = true;
							goto default;
						default:
							if (!strict) continue;
							var character = uc != UnicodeCategory.Control ? ch.ToString() : string.Empty;
							throw new ArgumentOutOfRangeException(nameof(s), ch,
								string.Format("Unhandled character in UnicodeCategory.{0}: '{1}' (0x{2}).", uc, character, ch.ToHex()));
					}
				}
				if (wordbreak && !skipbreak && sb.Length != 0)
				{
					// We've added at least one alphanumeric character, we're
					// about to add another, and there was a break inbetween.
					sb.Append('-');
				}
				// Append character and reset whether or not we're in a break.
				sb.Append(append);
				wordbreak = false;
				skipbreak = skipnext;
			}
			if (parenthetical)
			{
				for (; parenCount > 0; parenCount--)
					sb.Append(")");
			}
			// Return slug re-normalized to FormC.
			return (sb.ToString().Normalize(NormalizationForm.FormC));
		}

		private static readonly IDictionary<char, string> CharacterSlugs = new Dictionary<char, string>
		{
			{'\u00B5', "u"}, // µ — Greek letter mu
			{'\u00D0', "D"}, // Ð — Latin capital letter eth
			{'\u00F0', "d"}, // ð — Latin small letter eth
			{'\u00D8', "O"}, // Ø — Latin capital letter o with stroke
			{'\u00F8', "o"}, // ø — Latin small letter o with stroke
			{'\u00DE', "P"}, // Þ — Latin capital letter thorn
			{'\u00FE', "p"}, // þ — Latin small letter thorn
			{'\u00E6', "ae"}, // æ
			{'\u00C6', "AE"}, // Æ
			{'\u0153', "oe"}, // œ
			{'\u0152', "OE"}, // Œ
			{'\u1E9E', "SS"}, // ẞ — German capital eszett
			{'\u00DF', "ss"} // ß — German small eszett
		};
	}
}