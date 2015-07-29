using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using NUnit.Framework;

// ReSharper disable InvokeAsExtensionMethod

namespace BitFn.Core.Tests.Extensions.ForIQueryable
{
	[TestFixture]
	public class OrderDescending
	{
		[ExcludeFromCodeCoverage]
		private static TestDelegate TestDelegate<T>(IQueryable<T> source)
		{
			return () => Core.Extensions.ForIQueryable.OrderDescending(source);
		}

		[Test]
		public void WhenGivenComparer_ShouldUse()
		{
			// Arrange
			var queryable = new[] {"A", "a", "B", "b"}.AsQueryable();
			var expected = new[] {"b", "a", "B", "A"};
			var comparer = StringComparer.Ordinal;

			// Act
			var actual = Core.Extensions.ForIQueryable.OrderDescending(queryable, comparer);

			// Assert
			CollectionAssert.AreEqual(expected, actual);
		}

		[Test]
		public void WhenGivenNullComparer_ShouldDefaultComparer()
		{
			// Arrange
			var queryable = new[] {"A", "a", "B", "b"}.AsQueryable();
			var expected = new[] {"B", "b", "A", "a"};
			var comparer = null as IComparer<string>;

			// Act
			// ReSharper disable once ExpressionIsAlwaysNull
			var actual = Core.Extensions.ForIQueryable.OrderDescending(queryable, comparer);

			// Assert
			CollectionAssert.AreEqual(expected, actual);
		}

		[Test]
		public void WhenGivenNullQueryable_ShouldThrowArgumentNullException()
		{
			// Arrange
			var queryable = null as IQueryable<object>;

			// Act
			// ReSharper disable once ExpressionIsAlwaysNull
			var code = TestDelegate(queryable);

			// Assert
			Assert.Throws<ArgumentNullException>(code);
		}

		[Test]
		public void WhenGivenQueryable_ShouldOrderDescending()
		{
			// Arrange
			var queryable = new[] {"A", "a", "B", "b"}.AsQueryable();
			var expected = new[] {"B", "b", "A", "a"};

			// Act
			var actual = Core.Extensions.ForIQueryable.OrderDescending(queryable);

			// Assert
			CollectionAssert.AreEqual(expected, actual);
		}
	}
}