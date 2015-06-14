using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

// ReSharper disable InvokeAsExtensionMethod

namespace BitFn.Core.Tests.Extensions.ForIEnumerable
{
	[TestFixture]
	public class Shuffle
	{
		[Test]
		public void WhenGivenNullEnumerable_ShouldThrowArgumentNullExceptionBeforeEnumeration()
		{
			// Arrange
			var enumerable = null as IEnumerable<object>;
			var rng = new Random();

			// Act
			// ReSharper disable once ExpressionIsAlwaysNull
			TestDelegate code = () => { Core.Extensions.ForIEnumerable.Shuffle(enumerable, rng); };

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
			TestDelegate code = () => { Core.Extensions.ForIEnumerable.Shuffle(enumerable, rng); };

			// Assert
			Assert.Throws<ArgumentNullException>(code);
		}

		[Test]
		public void WhenGivenSingleItem_ShouldReturnEqual()
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
		public void WhenGivenMultipleItems_ShouldReturnEquivalent()
		{
			// Arrange
			var enumerable = new[] {1, 2, 3};
			var rng = new Random();

			// Act
			var actual = Core.Extensions.ForIEnumerable.Shuffle(enumerable, rng);

			// Assert
			CollectionAssert.AreEquivalent(enumerable, actual);
		}

		[Test]
		public void WhenGivenNoRng_ShouldReturn()
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