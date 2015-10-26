using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;

// ReSharper disable InvokeAsExtensionMethod

namespace BitFn.Core.Tests.Extensions.ForIEnumerable
{
	[TestFixture]
	public class Append
	{
		[ExcludeFromCodeCoverage]
		private static TestDelegate TestDelegate<T>(
			IEnumerable<T> enumerable, params T[] elements)
		{
			return () => Core.Extensions.ForIEnumerable.Append(enumerable, elements);
		}

		[Test]
		public void WhenGivenElements_ShouldReturnAppended()
		{
			// Arrange
			var enumerable = new[] {1, 2, 3};
			var expected = new[] {1, 2, 3, 4, 5, 6};

			// Act
			var actual = Core.Extensions.ForIEnumerable.Append(enumerable, 4, 5, 6);

			// Assert
			CollectionAssert.AreEqual(expected, actual);
		}

		[Test]
		public void WhenGivenNoElements_ShouldReturnSameEnumerable()
		{
			// Arrange
			var expected = new[] {new object()};

			// Act
			var actual = Core.Extensions.ForIEnumerable.Append(expected);

			// Assert
			Assert.AreSame(expected, actual);
		}

		[Test]
		public void WhenGivenNullElements_ShouldThrowArgumentNullException()
		{
			// Arrange
			var enumerable = new[] {new object()};
			var elements = null as object[];

			// Act
			// ReSharper disable once ExpressionIsAlwaysNull
			var code = TestDelegate(enumerable, elements);

			// Assert
			Assert.Throws<ArgumentNullException>(code);
		}

		[Test]
		public void WhenGivenNullEnumerable_ShouldThrowArgumentNullException()
		{
			// Arrange
			var enumerable = null as IEnumerable<object>;
			var elements = new[] {new object()};

			// Act
			// ReSharper disable once ExpressionIsAlwaysNull
			var code = TestDelegate(enumerable, elements);

			// Assert
			Assert.Throws<ArgumentNullException>(code);
		}
	}
}