using System;
using NUnit.Framework;
using Ploeh.AutoFixture.NUnit2;

// ReSharper disable InvokeAsExtensionMethod

namespace BitFn.Core.Tests.Extensions.ForString
{
	[TestFixture]
	public class ToSlug
	{
		[TestCase("A", Result= "a")]
		[TestCase("AB", Result = "ab")]
		[TestCase("AbC", Result = "abc")]
		[TestCase("A B", Result = "a-b")]
		[TestCase("A_B", Result = "a_b")]
		[TestCase("ABCDEFGHIJKLMNOPQRSTUVWXYZ", Result = "abcdefghijklmnopqrstuvwxyz")]
		[TestCase("0123456789", Result = "0123456789")]
		[TestCase("AEIÖU", Result = "aeiou")]
		[TestCase("AE Æ", Result = "ae-ae")]
		[TestCase("OE Œ", Result = "oe-oe")]
		[TestCase("SS ẞ", Result = "ss-ss")]
		public string WhenConvertingToLowercase_ReturnsLowercase(string s)
		{
			return Core.Extensions.ForString.ToSlug(s, lowercase: true);
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
		public void WhenGivenGuid_ShouldReturnEqual()
		{
			// Arrange
			var expected = Guid.NewGuid().ToString();
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

		[TestCase("", Result= "")]
		[TestCase("_", Result = "_")]
		[TestCase("a", Result = "a")]
		[TestCase("ab", Result = "ab")]
		[TestCase("a\u0001b", Result = "ab")]
		[TestCase("A", Result = "A")]
		[TestCase("AB", Result = "AB")]
		[TestCase("AbC", Result = "AbC")]
		[TestCase("a b", Result = "a-b")]
		[TestCase("a\u00A0b", Result = "a-b")]
		[TestCase("a  b", Result = "a-b")]
		[TestCase(" a  b ", Result = "a-b")]
		[TestCase("a-b", Result = "a-b")]
		[TestCase("a—b", Result = "a-b")]
		[TestCase("a\u2015b", Result = "a-b")]
		[TestCase("a--b", Result = "a-b")]
		[TestCase("-a--b-", Result = "a-b")]
		[TestCase("\na\t \nb\t", Result = "a-b")]
		[TestCase("a'\"!@#$%^&*:;b", Result = "ab")]
		[TestCase("a(b)c[d]e{f}g/h\\i|j", Result = "a-b-c-d-e-f-g-h-i-j")]
		[TestCase("abcdefghijklmnopqrstuvwxyz", Result = "abcdefghijklmnopqrstuvwxyz")]
		[TestCase("ABCDEFGHIJKLMNOPQRSTUVWXYZ", Result = "ABCDEFGHIJKLMNOPQRSTUVWXYZ")]
		[TestCase("0123456789", Result = "0123456789")]
		[TestCase("(1+2)i=3i", Result = "1-2-i-3i")]
		[TestCase("abcdé", Result = "abcde")]
		[TestCase("AEIÖU", Result = "AEIOU")]
		[TestCase("ae æ", Result = "ae-ae")]
		[TestCase("AE Æ", Result = "AE-AE")]
		[TestCase("oe œ", Result = "oe-oe")]
		[TestCase("OE Œ", Result = "OE-OE")]
		[TestCase("ss ß", Result = "ss-ss")]
		[TestCase("SS ẞ", Result = "SS-SS")]
		public string WhenGivenValidInput_ReturnsValidSlug(string s)
		{
			return Core.Extensions.ForString.ToSlug(s);
		}

		[TestCase("a[b]c", Result = "a(b)c")]
		[TestCase("a{b}c", Result = "a(b)c")]
		[TestCase("a(b)c", Result = "a(b)c")]
		[TestCase("(a(b)c)", Result = "(a(b)c)")]
		[TestCase(" (a(b)c) ", Result = "(a(b)c)")]
		[TestCase(" a ( ( b ) c ) d ", Result = "a-((b)-c)-d")]
		public string WhenParenthetical_ReturnsBreaksCorrectly(string s)
		{
			return Core.Extensions.ForString.ToSlug(s, parenthetical: true);
		}

		[TestCase("a(b", Result = "a(b)")]
		[TestCase("a[(b", Result = "a((b))")]
		[TestCase("a{[(b", Result = "a(((b)))")]
		public string WhenParenthetical_ReturnsClosedPunctuation(string s)
		{
			return Core.Extensions.ForString.ToSlug(s, parenthetical: true);
		}

		[TestCase("a)b", Result = "a-b")]
		[TestCase("a])b", Result = "a-b")]
		[TestCase("a}])b", Result = "a-b")]
		public string WhenParenthetical_ReturnsWithoutUnmatchedClosePunctuation(string s)
		{
			return Core.Extensions.ForString.ToSlug(s, parenthetical: true);
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

		[TestCase("à la mode", Result = "a-la-mode")]
		[TestCase("cañón", Result = "canon")]
		[TestCase("daïs", Result = "dais")]
		[TestCase("El Niño", Result = "El-Nino")]
		[TestCase("façade", Result = "facade")]
		[TestCase("phở", Result = "pho")]
		[TestCase("tête-à-tête", Result = "tete-a-tete")]
		[TestCase("zoölogy", Result = "zoology")]
		public string WhenGivenDiacritics_ReturnsStripped(string s)
		{
			return Core.Extensions.ForString.ToSlug(s);
		}
	}
}