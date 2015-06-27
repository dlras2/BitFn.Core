using System;
using NUnit.Framework;

// ReSharper disable InvokeAsExtensionMethod

namespace BitFn.Core.Tests.Extensions.ForString
{
	[TestFixture]
	public class RemoveDiacritics
	{
		[TestCase("à la mode", Result = "a la mode")]
		[TestCase("cañón", Result = "canon")]
		[TestCase("daïs", Result = "dais")]
		[TestCase("El Niño", Result = "El Nino")]
		[TestCase("façade", Result = "facade")]
		[TestCase("phở", Result = "pho")]
		[TestCase("tête-à-tête", Result = "tete-a-tete")]
		[TestCase("zoölogy", Result = "zoology")]
		public string WhenGivenDiacritics_ReturnsStripped(string s)
		{
			return Core.Extensions.ForString.RemoveDiacritics(s);
		}

		[Test]
		public void WhenGivenEmptyString_ReturnsEmpty()
		{
			// Arrange
			var expected = string.Empty;
			// Act
			var actual = Core.Extensions.ForString.RemoveDiacritics(string.Empty);
			// Assert
			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void WhenGivenNull_ThrowsArgumentNullException()
		{
			// Arrange
			TestDelegate code = () => Core.Extensions.ForString.RemoveDiacritics(null);
			// Act
			// Assert
			Assert.Throws<ArgumentNullException>(code);
		}
	}
}