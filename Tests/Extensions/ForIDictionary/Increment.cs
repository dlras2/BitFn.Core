using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;

// ReSharper disable InvokeAsExtensionMethod

namespace BitFn.Core.Tests.Extensions.ForIDictionary
{
	[TestFixture]
	public class Increment
	{
		[ExcludeFromCodeCoverage]
		private static TestDelegate TestDelegate<TKey>(IDictionary<TKey, int> dictionary, TKey key)
		{
			return () => Core.Extensions.ForIDictionary.Increment(dictionary, key);
		}

		[ExcludeFromCodeCoverage]
		private static TestDelegate TestDelegate<TKey>(IDictionary<TKey, int> dictionary, TKey key, int step)
		{
			return () => Core.Extensions.ForIDictionary.Increment(dictionary, key, step);
		}

		[Test]
		public void WhenGivenNullDictionary_ShouldThrowArgumentNullException()
		{
			// Arrange
			var dictionary = null as IDictionary<object, int>;
			var key = new object();

			// Act
			// ReSharper disable once ExpressionIsAlwaysNull
			var code = TestDelegate(dictionary, key);

			// Assert
			Assert.Throws<ArgumentNullException>(code);
		}

		[Test]
		public void WhenGivenNullDictionaryAndStep_ShouldThrowArgumentNullException()
		{
			// Arrange
			var dictionary = null as IDictionary<object, int>;
			var key = new object();
			const int step = 1;

			// Act
			// ReSharper disable once ExpressionIsAlwaysNull
			var code = TestDelegate(dictionary, key, step);

			// Assert
			Assert.Throws<ArgumentNullException>(code);
		}

		[Test]
		public void WhenGivenNullKey_ShouldThrowArgumentNullException()
		{
			// Arrange
			var dictionary = new Dictionary<object, int>();
			var key = null as object;

			// Act
			// ReSharper disable once ExpressionIsAlwaysNull
			var code = TestDelegate(dictionary, key);

			// Assert
			Assert.Throws<ArgumentNullException>(code);
		}

		[Test]
		public void WhenGivenNullKeyAndStep_ShouldThrowArgumentNullException()
		{
			// Arrange
			var dictionary = new Dictionary<object, int>();
			var key = null as object;
			const int step = 1;

			// Act
			// ReSharper disable once ExpressionIsAlwaysNull
			var code = TestDelegate(dictionary, key, step);

			// Assert
			Assert.Throws<ArgumentNullException>(code);
		}

		[Test]
		public void WhenIncrementingExistingValue_ShouldReturnOneHigher([Values(-10, 0, 10)] int value)
		{
			// Arrange
			var key = Guid.NewGuid().ToString();
			var expected = value + 1;
			var dictionary = new Dictionary<string, int> {[key] = value};

			// Act
			var actual = Core.Extensions.ForIDictionary.Increment(dictionary, key);

			// Assert
			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void WhenIncrementingExistingValue_ShouldReturnOneStepHigher(
			[Values(-10, 0, 10)] int value, [Values(-1, 0, 1)] int step)
		{
			// Arrange
			var key = Guid.NewGuid().ToString();
			var dictionary = new Dictionary<string, int> {[key] = value};
			var expected = value + step;

			// Act
			var actual = Core.Extensions.ForIDictionary.Increment(dictionary, key, step);

			// Assert
			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void WhenIncrementingExistingValue_ShouldSetToOneHigher([Values(-10, 0, 10)] int value)
		{
			// Arrange
			var key = Guid.NewGuid().ToString();
			var expected = value + 1;
			var dictionary = new Dictionary<string, int> {[key] = value};

			// Act
			Core.Extensions.ForIDictionary.Increment(dictionary, key);
			var actual = dictionary[key];

			// Assert
			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void WhenIncrementingExistingValue_ShouldSetToOneStepHigher(
			[Values(-10, 0, 10)] int value, [Values(-1, 0, 1)] int step)
		{
			// Arrange
			var key = Guid.NewGuid().ToString();
			var dictionary = new Dictionary<string, int> {[key] = value};
			var expected = value + step;

			// Act
			Core.Extensions.ForIDictionary.Increment(dictionary, key, step);
			var actual = dictionary[key];

			// Assert
			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void WhenIncrementingMissingValue_ShouldReturnOne()
		{
			// Arrange
			var key = Guid.NewGuid().ToString();
			const int expected = 1;
			var dictionary = new Dictionary<string, int>();

			// Act
			var actual = Core.Extensions.ForIDictionary.Increment(dictionary, key);

			// Assert
			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void WhenIncrementingMissingValue_ShouldReturnStep([Values(-1, 0, 1)] int step)
		{
			// Arrange
			var key = Guid.NewGuid().ToString();
			var dictionary = new Dictionary<string, int>();
			var expected = step;

			// Act
			var actual = Core.Extensions.ForIDictionary.Increment(dictionary, key, step);

			// Assert
			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void WhenIncrementingMissingValue_ShouldSetToOne()
		{
			// Arrange
			var key = Guid.NewGuid().ToString();
			const int expected = 1;
			var dictionary = new Dictionary<string, int>();

			// Act
			Core.Extensions.ForIDictionary.Increment(dictionary, key);
			var actual = dictionary[key];

			// Assert
			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void WhenIncrementingMissingValue_ShouldSetToStep([Values(-1, 0, 1)] int step)
		{
			// Arrange
			var key = Guid.NewGuid().ToString();
			var dictionary = new Dictionary<string, int>();
			var expected = step;

			// Act
			Core.Extensions.ForIDictionary.Increment(dictionary, key, step);
			var actual = dictionary[key];

			// Assert
			Assert.AreEqual(expected, actual);
		}
	}
}