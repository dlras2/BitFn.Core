using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using NUnit.Framework;

// ReSharper disable InvokeAsExtensionMethod

namespace BitFn.Core.Tests.Extensions.ForIEnumerable
{
	[TestFixture]
	public class Shuffle
	{
		[ExcludeFromCodeCoverage]
		private static TestDelegate TestDelegate<T>(IEnumerable<T> source)
		{
			return () => Core.Extensions.ForIEnumerable.Shuffle(source);
		}

		[ExcludeFromCodeCoverage]
		private static TestDelegate TestDelegate<T>(IEnumerable<T> source, Random rng)
		{
			return () => Core.Extensions.ForIEnumerable.Shuffle(source, rng);
		}

		[ExcludeFromCodeCoverage]
		private static TestDelegate TestDelegate<T>(IEnumerable<T> source, RandomIntBetween rng)
		{
			return () => Core.Extensions.ForIEnumerable.Shuffle(source, rng);
		}

		[Test]
		public void WhenGivenMultipleItems_ShouldReturnEquivalent()
		{
			// Arrange
			var enumerable = new[] {1, 2, 3};

			// Act
			var actual = Core.Extensions.ForIEnumerable.Shuffle(enumerable);

			// Assert
			CollectionAssert.AreEquivalent(enumerable, actual);
		}

		[Test]
		public void WhenGivenNullEnumerable_ShouldThrowArgumentNullExceptionBeforeEnumeration()
		{
			// Arrange
			var enumerable = null as IEnumerable<object>;

			// Act
			// ReSharper disable once ExpressionIsAlwaysNull
			var code = TestDelegate(enumerable);

			// Assert
			Assert.Throws<ArgumentNullException>(code);
		}

		[Test]
		public void WhenGivenNullRandom_ShouldThrowArgumentNullExceptionBeforeEnumeration()
		{
			// Arrange
			var enumerable = Enumerable.Empty<object>();
			var rng = null as Random;

			// Act
			// ReSharper disable once ExpressionIsAlwaysNull
			var code = TestDelegate(enumerable, rng);

			// Assert
			Assert.Throws<ArgumentNullException>(code);
		}

		[Test]
		public void WhenGivenNullRandomDelegate_ShouldThrowArgumentNullExceptionBeforeEnumeration()
		{
			// Arrange
			var enumerable = Enumerable.Empty<object>();
			var rng = null as RandomIntBetween;

			// Act
			// ReSharper disable once ExpressionIsAlwaysNull
			var code = TestDelegate(enumerable, rng);

			// Assert
			Assert.Throws<ArgumentNullException>(code);
		}

		[Test]
		public void WhenGivenRandom_ShouldUseNext()
		{
			// Arrange
			var enumerable = new[] {1};
			var rng = new Random();

			// Act
			var actual = Core.Extensions.ForIEnumerable.Shuffle(enumerable, rng);

			// Assert
			CollectionAssert.AreEqual(enumerable, actual);
		}

		[Test]
		public void WhenGivenSingleItem_ShouldReturnEqual()
		{
			// Arrange
			var enumerable = new[] {1};

			// Act
			var actual = Core.Extensions.ForIEnumerable.Shuffle(enumerable);

			// Assert
			CollectionAssert.AreEqual(enumerable, actual);
		}
	}
}