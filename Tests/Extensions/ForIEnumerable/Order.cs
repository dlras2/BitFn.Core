using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;

// ReSharper disable InvokeAsExtensionMethod

namespace BitFn.Core.Tests.Extensions.ForIEnumerable
{
	[TestFixture]
	public class Order
	{
		[ExcludeFromCodeCoverage]
		private static TestDelegate TestDelegate<T>(IEnumerable<T> source)
		{
			return () => Core.Extensions.ForIEnumerable.Order(source);
		}

		[Test]
		public void WhenGivenComparer_ShouldUse()
		{
			// Arrange
			var enumerable = new[] {"A", "a", "B", "b"};
			var expected = new[] {"A", "B", "a", "b"};
			var comparer = StringComparer.Ordinal;

			// Act
			var actual = Core.Extensions.ForIEnumerable.Order(enumerable, comparer);

			// Assert
			CollectionAssert.AreEqual(expected, actual);
		}

		[Test]
		public void WhenGivenEnumerable_ShouldOrder()
		{
			// Arrange
			var enumerable = new[] {"A", "a", "B", "b"};
			var expected = new[] {"a", "A", "b", "B"};

			// Act
			var actual = Core.Extensions.ForIEnumerable.Order(enumerable);

			// Assert
			CollectionAssert.AreEqual(expected, actual);
		}

		[Test]
		public void WhenGivenNullComparer_ShouldDefaultComparer()
		{
			// Arrange
			var enumerable = new[] {"A", "a", "B", "b"};
			var expected = new[] {"a", "A", "b", "B"};
			var comparer = null as IComparer<string>;

			// Act
			// ReSharper disable once ExpressionIsAlwaysNull
			var actual = Core.Extensions.ForIEnumerable.Order(enumerable, comparer);

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