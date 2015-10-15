using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using NUnit.Framework;

// ReSharper disable InvokeAsExtensionMethod

namespace BitFn.Core.Tests.Extensions.ForIEnumerable
{
	[TestFixture]
	public class Flatten
	{
		[ExcludeFromCodeCoverage]
		private static TestDelegate TestDelegate<T>(IEnumerable<IEnumerable<T>> source)
		{
			return () => Core.Extensions.ForIEnumerable.Flatten(source);
		}

		[ExcludeFromCodeCoverage]
		private static TestDelegate TestDelegate<T>(IEnumerable<IEnumerable<IEnumerable<T>>> source)
		{
			return () => Core.Extensions.ForIEnumerable.Flatten(source);
		}

		[ExcludeFromCodeCoverage]
		private static TestDelegate TestDelegate<T>(IEnumerable<IEnumerable<IEnumerable<IEnumerable<T>>>> source)
		{
			return () => Core.Extensions.ForIEnumerable.Flatten(source);
		}

		[Test]
		public void WhenGivenMultipleDoublyWrappedEnumerables_ShouldReturnConcatenated()
		{
			// Arrange
			var expected = Enumerable.Range(1, 16);
			var enumerable = new[]
			{
				new[] {new[] {1, 2, 3, 4}, new[] {5, 6, 7, 8}},
				new[] {new[] {9, 10, 11, 12}, new[] {13, 14, 15, 16}}
			};

			// Act
			// ReSharper disable once ExpressionIsAlwaysNull
			var actual = Core.Extensions.ForIEnumerable.Flatten<int>(enumerable);

			// Assert
			CollectionAssert.AreEquivalent(expected, actual);
		}

		[Test]
		public void WhenGivenMultipleSinglyWrappedEnumerables_ShouldReturnConcatenated()
		{
			// Arrange
			var expected = Enumerable.Range(1, 8);
			var enumerable = new[] {new[] {1, 2, 3, 4}, new[] {5, 6, 7, 8}};

			// Act
			// ReSharper disable once ExpressionIsAlwaysNull
			var actual = Core.Extensions.ForIEnumerable.Flatten(enumerable);

			// Assert
			CollectionAssert.AreEquivalent(expected, actual);
		}

		[Test]
		public void WhenGivenMultipleTriplyWrappedEnumerables_ShouldReturnConcatenated()
		{
			// Arrange
			var expected = Enumerable.Range(1, 32);
			var enumerable = new[]
			{
				new[]
				{
					new[] {new[] {1, 2, 3, 4}, new[] {5, 6, 7, 8}},
					new[] {new[] {9, 10, 11, 12}, new[] {13, 14, 15, 16}}
				},
				new[]
				{
					new[] {new[] {17, 18, 19, 20}, new[] {21, 22, 23, 24}},
					new[] {new[] {25, 26, 27, 28}, new[] {29, 30, 31, 32}}
				}
			};

			// Act
			// ReSharper disable once ExpressionIsAlwaysNull
			var actual = Core.Extensions.ForIEnumerable.Flatten<int>(enumerable);

			// Assert
			CollectionAssert.AreEquivalent(expected, actual);
		}

		[Test]
		public void WhenGivenNullDoublyWrappedEnumerable_ShouldThrowArgumentNullException()
		{
			// Arrange
			var enumerable = null as IEnumerable<IEnumerable<IEnumerable<object>>>;

			// Act
			// ReSharper disable once ExpressionIsAlwaysNull
			var code = TestDelegate(enumerable);

			// Assert
			Assert.Throws<ArgumentNullException>(code);
		}

		[Test]
		public void WhenGivenNullSinglyWrappedEnumerable_ShouldThrowArgumentNullException()
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
		public void WhenGivenNullTriplyWrappedEnumerable_ShouldThrowArgumentNullException()
		{
			// Arrange
			var enumerable = null as IEnumerable<IEnumerable<IEnumerable<IEnumerable<object>>>>;

			// Act
			// ReSharper disable once ExpressionIsAlwaysNull
			var code = TestDelegate(enumerable);

			// Assert
			Assert.Throws<ArgumentNullException>(code);
		}

		[Test]
		public void WhenGivenOneDoublyWrappedEnumerable_ShouldReturnEquivalent()
		{
			// Arrange
			var expected = new[] {1, 2, 3, 4};
			var enumerable = new[] {new[] {expected}};

			// Act
			// ReSharper disable once ExpressionIsAlwaysNull
			var actual = Core.Extensions.ForIEnumerable.Flatten<int>(enumerable);

			// Assert
			CollectionAssert.AreEquivalent(expected, actual);
		}

		[Test]
		public void WhenGivenOneSinglyWrappedEnumerable_ShouldReturnEquivalent()
		{
			// Arrange
			var expected = new[] {1, 2, 3, 4};
			var enumerable = new[] {expected};

			// Act
			// ReSharper disable once ExpressionIsAlwaysNull
			var actual = Core.Extensions.ForIEnumerable.Flatten(enumerable);

			// Assert
			CollectionAssert.AreEquivalent(expected, actual);
		}

		[Test]
		public void WhenGivenOneTriplyWrappedEnumerable_ShouldReturnEquivalent()
		{
			// Arrange
			var expected = new[] {1, 2, 3, 4};
			var enumerable = new[] {new[] {new[] {expected}}};

			// Act
			// ReSharper disable once ExpressionIsAlwaysNull
			var actual = Core.Extensions.ForIEnumerable.Flatten<int>(enumerable);

			// Assert
			CollectionAssert.AreEquivalent(expected, actual);
		}
	}
}