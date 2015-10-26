using System;
using NUnit.Framework;

// ReSharper disable InvokeAsExtensionMethod

namespace BitFn.Core.Tests.Extensions.ForString
{
	[TestFixture]
	public class Unescape
	{
		[TestCase(@"Line:\t1\r\nLine:	2", Result = "Line:\t1\r\nLine:	2")]
		[TestCase(@"Line:\u00091\xaLine:\u00092", Result = "Line:\t1\nLine:\t2")]
		[TestCase(@"Les Mise\U00000301rables", Result = "Les Mise\U00000301rables")]
		[TestCase(@"Les Mise\x301rables", Result = "Les Mise\x301rables")]
		[TestCase(@"Les Mise\x0301rables", Result = "Les Mise\x0301rables")]
		[TestCase(@"\'\""\\\0\a\b\f\n\r\t\v", Result = "\'\"\\\0\a\b\f\n\r\t\v")]
		public string WhenEscapedString_ReturnsUnescaped(string s)
		{
			return Core.Extensions.ForString.Unescape(s);
		}

		[TestCase(@"Line: 1; Line: 2", Result = "Line: 1; Line: 2")]
		public string WhenNonEscapedString_ReturnsSameString(string s)
		{
			return Core.Extensions.ForString.Unescape(s);
		}

		[TestCase(@"\")]
		[TestCase(@"\d")]
		[TestCase(@"\x")]
		[TestCase(@"\u123")]
		[TestCase(@"\u123X")]
		[TestCase(@"\U1234567")]
		[TestCase(@"\U1234567X")]
		public void WhenGivenInvalidEscapedString_ThrowsFormatException(string s)
		{
			// Arrange
			TestDelegate code = () => Core.Extensions.ForString.Unescape(s);
			// Act
			// Assert
			Assert.Throws<FormatException>(code);
		}

		[Test]
		public void WhenGivenEmptyString_ReturnsEmpty()
		{
			// Arrange
			var expected = string.Empty;
			// Act
			var actual = Core.Extensions.ForString.Unescape(string.Empty);
			// Assert
			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void WhenGivenNull_ThrowsArgumentNullException()
		{
			// Arrange
			TestDelegate code = () => Core.Extensions.ForString.Unescape(null);
			// Act
			// Assert
			Assert.Throws<ArgumentNullException>(code);
		}
	}
}