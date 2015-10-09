using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;

// ReSharper disable InvokeAsExtensionMethod

namespace BitFn.Core.Tests.Extensions.ForIEnumerable
{
	[TestFixture]
	public class Aggregate
	{
		[ExcludeFromCodeCoverage]
		private static TestDelegate TestDelegate<T>(IEnumerable<IEnumerable<T>> source)
		{
			return () => Core.Extensions.ForIEnumerable.Aggregate(source);
		}

		[Test]
		public void WhenGivenMultipleEnumerables_ShouldReturnConcatenated()
		{
			// Arrange
			var source1 = new[] {1, 2, 3, 4};
			var source2 = new[] {10, 9, 8};
			var expected = new[] {1, 2, 3, 4, 10, 9, 8};
			var enumerable = new[] {source1, source2};

			// Act
			// ReSharper disable once ExpressionIsAlwaysNull
			var actual = Core.Extensions.ForIEnumerable.Aggregate(enumerable);

			// Assert
			CollectionAssert.AreEquivalent(expected, actual);
		}

		[Test]
		public void WhenGivenNullEnumerable_ShouldThrowArgumentNullException()
		{
			// Arrange
			var enumerable = null as IEnumerable<IEnumerable<object>>;

			// Act
			// ReSharper disable once ExpressionIsAlwaysNull
			var code = TestDelegate(enumerable);

			// Assert
			Assert.Throws<ArgumentNullException>(code);
		}

		[Test]
		public void WhenGivenSingleEnumerable_ShouldReturnEquivalent()
		{
			// Arrange
			var expected = new[] {1, 2, 3, 4};
			var enumerable = new[] {expected};

			// Act
			// ReSharper disable once ExpressionIsAlwaysNull
			var actual = Core.Extensions.ForIEnumerable.Aggregate(enumerable);

			// Assert
			CollectionAssert.AreEquivalent(expected, actual);
		}
	}
}