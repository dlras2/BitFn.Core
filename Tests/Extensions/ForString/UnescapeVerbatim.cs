using System;
using NUnit.Framework;

// ReSharper disable InvokeAsExtensionMethod

namespace BitFn.Core.Tests.Extensions.ForString
{
	[TestFixture]
	public class UnescapeVerbatim
	{
		[TestCase(@"Line:	""""1"""".", Result = @"Line:	""1"".")]
		public string WhenEscapedString_ReturnsUnescaped(string s)
		{
			return Core.Extensions.ForString.UnescapeVerbatim(s);
		}

		[TestCase(@"Line: 1; Line: 2", Result = @"Line: 1; Line: 2")]
		public string WhenNonEscapedString_ReturnsSameString(string s)
		{
			return Core.Extensions.ForString.UnescapeVerbatim(s);
		}

		[TestCase(@"One Double-Quote: """)]
		[TestCase(@"Three Double-Quote: '""""""'")]
		public void WhenGivenInvalidEscapedString_ThrowsFormatException(string s)
		{
			// Arrange
			TestDelegate code = () => Core.Extensions.ForString.UnescapeVerbatim(s);
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
			var actual = Core.Extensions.ForString.UnescapeVerbatim(string.Empty);
			// Assert
			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void WhenGivenNull_ThrowsArgumentNullException()
		{
			// Arrange
			TestDelegate code = () => Core.Extensions.ForString.UnescapeVerbatim(null);
			// Act
			// Assert
			Assert.Throws<ArgumentNullException>(code);
		}
	}
}