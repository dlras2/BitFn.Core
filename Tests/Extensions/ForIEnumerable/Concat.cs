using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;

// ReSharper disable InvokeAsExtensionMethod

namespace BitFn.Core.Tests.Extensions.ForIEnumerable
{
	[TestFixture]
	public class Concat
	{
		[ExcludeFromCodeCoverage]
		private static TestDelegate TestDelegate<T>(
			IEnumerable<T> first, params IEnumerable<T>[] subsequent)
		{
			return () => Core.Extensions.ForIEnumerable.Concat(first, subsequent);
		}

		[Test]
		public void WhenGivenNoSubsequentEnumerables_ShouldReturnSameEnumerable()
		{
			// Arrange
			var expected = new[] {new object()};

			// Act
			var actual = Core.Extensions.ForIEnumerable.Concat(expected);

			// Assert
			Assert.AreSame(expected, actual);
		}

		[Test]
		public void WhenGivenNullFirstEnumerable_ShouldThrowArgumentNullException()
		{
			// Arrange
			var first = null as IEnumerable<object>;
			var subsequent = new[] {new object()};

			// Act
			// ReSharper disable once ExpressionIsAlwaysNull
			var code = TestDelegate(first, subsequent, subsequent);

			// Assert
			Assert.Throws<ArgumentNullException>(code);
		}

		[Test]
		public void WhenGivenNullSubsequentEnumerables_ShouldThrowArgumentNullException()
		{
			// Arrange
			var first = new[] {new object()};
			var subsequents = null as IEnumerable<object>[];

			// Act
			// ReSharper disable once ExpressionIsAlwaysNull
			var code = TestDelegate(first, subsequents);

			// Assert
			Assert.Throws<ArgumentNullException>(code);
		}

		[Test]
		public void WhenGivenSubsequentEnumerables_ShouldReturnConcated()
		{
			// Arrange
			var first = new[] {1, 2, 3};
			var subsequent1 = new[] {4, 5};
			var subsequent2 = new[] {6};
			var expected = new[] {1, 2, 3, 4, 5, 6};

			// Act
			var actual = Core.Extensions.ForIEnumerable.Concat(first, subsequent1, subsequent2);

			// Assert
			CollectionAssert.AreEqual(expected, actual);
		}
	}
}