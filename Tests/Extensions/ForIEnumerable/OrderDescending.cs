using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;

// ReSharper disable InvokeAsExtensionMethod

namespace BitFn.Core.Tests.Extensions.ForIEnumerable
{
	[TestFixture]
	public class OrderDescending
	{
		[ExcludeFromCodeCoverage]
		private static TestDelegate TestDelegate<T>(IEnumerable<T> source)
		{
			return () => Core.Extensions.ForIEnumerable.OrderDescending(source);
		}

		[Test]
		public void WhenGivenComparer_ShouldUse()
		{
			// Arrange
			var enumerable = new[] {"A", "a", "B", "b"};
			var expected = new[] {"b", "a", "B", "A"};
			var comparer = StringComparer.Ordinal;

			// Act
			var actual = Core.Extensions.ForIEnumerable.OrderDescending(enumerable, comparer);

			// Assert
			CollectionAssert.AreEqual(expected, actual);
		}

		[Test]
		public void WhenGivenEnumerable_ShouldOrderDescending()
		{
			// Arrange
			var enumerable = new[] {"A", "a", "B", "b"};
			var expected = new[] {"B", "b", "A", "a"};

			// Act
			var actual = Core.Extensions.ForIEnumerable.OrderDescending(enumerable);

			// Assert
			CollectionAssert.AreEqual(expected, actual);
		}

		[Test]
		public void WhenGivenNullComparer_ShouldDefaultComparer()
		{
			// Arrange
			var enumerable = new[] {"A", "a", "B", "b"};
			var expected = new[] {"B", "b", "A", "a"};
			var comparer = null as IComparer<string>;

			// Act
			// ReSharper disable once ExpressionIsAlwaysNull
			var actual = Core.Extensions.ForIEnumerable.OrderDescending(enumerable, comparer);

			// Assert
			CollectionAssert.AreEqual(expected, actual);
		}

		[Test]
		public void WhenGivenNullEnumerable_ShouldThrowArgumentNullException()
		{
			// Arrange
			var enumerable = null as IEnumerable<object>;

			// Act
			// ReSharper disable once ExpressionIsAlwaysNull
			var code = TestDelegate(enumerable);

			// Assert
			Assert.Throws<ArgumentNullException>(code);
		}
	}
}