using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using NUnit.Framework;

// ReSharper disable InvokeAsExtensionMethod

namespace BitFn.Core.Tests.Extensions.ForStringBuilder
{
	[TestFixture]
	public class Append
	{
		[TestCase(".", 0, Result = "")]
		[TestCase(null, 100, Result = "")]
		[TestCase("", 100, Result = "")]
		[TestCase(".", 1, Result = ".")]
		[TestCase(".", 5, Result = ".....")]
		[TestCase("..", 5, Result = "..........")]
		[TestCase("123", 5, Result = "123123123123123")]
		public string WhenGivenEmptyBuilder_ReturnsExpectedString(string value, int n)
		{
			// Arrange
			var sut = new StringBuilder();

			// Act / Assert
			return Core.Extensions.ForStringBuilder.Append(sut, value, n).ToString();
		}

		[TestCase("seed", ".", 0, Result = "seed")]
		[TestCase("seed", "", 100, Result = "seed")]
		[TestCase("seed", ".", 1, Result = "seed.")]
		[TestCase("seed", ".", 5, Result = "seed.....")]
		[TestCase("seed", "..", 5, Result = "seed..........")]
		[TestCase("seed", "123", 5, Result = "seed123123123123123")]
		public string WhenGivenNonemptyBuilder_ReturnsExpectedString(string seed, string value, int n)
		{
			// Arrange
			var sut = new StringBuilder(seed);

			// Act / Assert
			return Core.Extensions.ForStringBuilder.Append(sut, value, n).ToString();
		}

		[ExcludeFromCodeCoverage]
		private static TestDelegate TestDelegate(StringBuilder sb, string value, int repeatCount)
		{
			return () => Core.Extensions.ForStringBuilder.Append(sb, value, repeatCount);
		}

		[Test]
		public void WhenGivenAnyInput_ShouldReturnSelf()
		{
			// Arrange
			var sut = new StringBuilder();
			var s = Guid.NewGuid().ToString();
			const int n = 1;

			// Act
			var actual = Core.Extensions.ForStringBuilder.Append(sut, s, n);

			// Assert
			Assert.AreSame(sut, actual);
		}

		[Test]
		public void WhenGivenNegativeCount_ShouldThrowArgumentOutOfRangeException()
		{
			// Arrange
			var sut = new StringBuilder();
			var s = Guid.NewGuid().ToString();
			const int n = -1;

			// Act
			// ReSharper disable once ExpressionIsAlwaysNull
			var code = TestDelegate(sut, s, n);

			// Assert
			Assert.Throws<ArgumentOutOfRangeException>(code);
		}

		[Test]
		public void WhenGivenNull_ShouldThrowArgumentNullException()
		{
			// Arrange
			var sut = null as StringBuilder;
			var s = Guid.NewGuid().ToString();
			const int n = 1;

			// Act
			// ReSharper disable once ExpressionIsAlwaysNull
			var code = TestDelegate(sut, s, n);

			// Assert
			Assert.Throws<ArgumentNullException>(code);
		}
	}
}