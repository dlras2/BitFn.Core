using System;
using NUnit.Framework;
using Ploeh.AutoFixture.NUnit2;

// ReSharper disable InvokeAsExtensionMethod

namespace BitFn.Core.Tests.Extensions.ForString
{
	[TestFixture]
	public class ToSlug
	{
		[TestCase("A", "a")]
		[TestCase("AB", "ab")]
		[TestCase("AbC", "abc")]
		[TestCase("A B", "a-b")]
		[TestCase("A_B", "a_b")]
		[TestCase("ABCDEFGHIJKLMNOPQRSTUVWXYZ", "abcdefghijklmnopqrstuvwxyz")]
		[TestCase("0123456789", "0123456789")]
		[TestCase("AEIÖU", "aeiou")]
		[TestCase("AE Æ", "ae-ae")]
		[TestCase("OE Œ", "oe-oe")]
		[TestCase("SS ẞ", "ss-ss")]
		public void WhenConvertingToLowercase_ShouldReturnValidSlug(string s, string expected)
		{
			// Arrange
			// Act
			var actual = Core.Extensions.ForString.ToSlug(s, true);
			// Assert
			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void WhenGivenExtendedASCII_ShouldNotThrowException()
		{
			for (var ctr = 1; ctr <= 255; ctr++)
			{
				var ch = (char) ctr;
				Core.Extensions.ForString.ToSlug(ch.ToString());
			}
		}

		[Test]
		[AutoData]
		public void WhenGivenGuid_ShouldReturnEqual(Guid guid)
		{
			// Arrange
			var expected = guid.ToString();
			// Act
			var actual = Core.Extensions.ForString.ToSlug(expected);
			// Assert
			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void WhenGivenNullInput_ShouldThrowArgumentNullException()
		{
			// Arrange
			TestDelegate code = () => Core.Extensions.ForString.ToSlug(null);
			// Act
			// Assert
			Assert.Throws<ArgumentNullException>(code);
		}

		[TestCase("", "")]
		[TestCase("_", "_")]
		[TestCase("a", "a")]
		[TestCase("ab", "ab")]
		[TestCase("a\u0001b", "ab")]
		[TestCase("A", "A")]
		[TestCase("AB", "AB")]
		[TestCase("AbC", "AbC")]
		[TestCase("a b", "a-b")]
		[TestCase("a\u00A0b", "a-b")]
		[TestCase("a  b", "a-b")]
		[TestCase(" a  b ", "a-b")]
		[TestCase("a-b", "a-b")]
		[TestCase("a—b", "a-b")]
		[TestCase("a\u2015b", "a-b")]
		[TestCase("a--b", "a-b")]
		[TestCase("-a--b-", "a-b")]
		[TestCase("\na\t \nb\t", "a-b")]
		[TestCase("a'\"!@#$%^&*:;b", "ab")]
		[TestCase("a(b)c[d]e{f}g/h\\i|j", "a-b-c-d-e-f-g-h-i-j")]
		[TestCase("abcdefghijklmnopqrstuvwxyz", "abcdefghijklmnopqrstuvwxyz")]
		[TestCase("ABCDEFGHIJKLMNOPQRSTUVWXYZ", "ABCDEFGHIJKLMNOPQRSTUVWXYZ")]
		[TestCase("0123456789", "0123456789")]
		[TestCase("(1+2)i=3i", "1-2-i-3i")]
		[TestCase("abcdé", "abcde")]
		[TestCase("AEIÖU", "AEIOU")]
		[TestCase("ae æ", "ae-ae")]
		[TestCase("AE Æ", "AE-AE")]
		[TestCase("oe œ", "oe-oe")]
		[TestCase("OE Œ", "OE-OE")]
		[TestCase("ss ß", "ss-ss")]
		[TestCase("SS ẞ", "SS-SS")]
		public void WhenGivenValidInput_ShouldReturnValidSlug(string s, string expected)
		{
			// Arrange
			// Act
			var actual = Core.Extensions.ForString.ToSlug(s);
			// Assert
			Assert.AreEqual(expected, actual);
		}

		[TestCase("a[b]c", "a(b)c")]
		[TestCase("a{b}c", "a(b)c")]
		[TestCase("a(b)c", "a(b)c")]
		[TestCase("(a(b)c)", "(a(b)c)")]
		[TestCase(" (a(b)c) ", "(a(b)c)")]
		[TestCase(" a ( ( b ) c ) d ", "a-((b)-c)-d")]
		public void WhenParenthetical_ShouldBreakCorrectly(string s, string expected)
		{
			// Arrange
			// Act
			var actual = Core.Extensions.ForString.ToSlug(s, parenthetical: true);
			// Assert
			Assert.AreEqual(expected, actual);
		}

		[TestCase("a(b", "a(b)")]
		[TestCase("a[(b", "a((b))")]
		[TestCase("a{[(b", "a(((b)))")]
		public void WhenParenthetical_ShouldCloseUnmatchedOpenPunctuation(string s, string expected)
		{
			// Arrange
			// Act
			var actual = Core.Extensions.ForString.ToSlug(s, parenthetical: true);
			// Assert
			Assert.AreEqual(expected, actual);
		}

		[TestCase("a)b", "a-b")]
		[TestCase("a])b", "a-b")]
		[TestCase("a}])b", "a-b")]
		public void WhenParenthetical_ShouldIgnoreUnmatchedClosePunctuation(string s, string expected)
		{
			// Arrange
			// Act
			var actual = Core.Extensions.ForString.ToSlug(s, parenthetical: true);
			// Assert
			Assert.AreEqual(expected, actual);
		}

		[TestCase("a\u0001b")]
		public void WhenStrictAndGivenInvalidInput_ShouldThrowArgumentOutOfRangeException(string s)
		{
			// Arrange
			TestDelegate code = () => Core.Extensions.ForString.ToSlug(s, strict: true);
			// Act
			// Assert
			Assert.Throws<ArgumentOutOfRangeException>(code);
		}
	}
}