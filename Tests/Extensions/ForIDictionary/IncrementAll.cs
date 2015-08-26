using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;

// ReSharper disable InvokeAsExtensionMethod

namespace BitFn.Core.Tests.Extensions.ForIDictionary
{
	[TestFixture]
	public class IncrementAll
	{
		[ExcludeFromCodeCoverage]
		private static TestDelegate TestDelegate<TKey>(IDictionary<TKey, int> dictionary, IEnumerable<TKey> keys)
		{
			return () => Core.Extensions.ForIDictionary.IncrementAll(dictionary, keys);
		}

		[ExcludeFromCodeCoverage]
		private static TestDelegate TestDelegate<TKey>(IDictionary<TKey, int> dictionary, IEnumerable<TKey> keys, int step)
		{
			return () => Core.Extensions.ForIDictionary.IncrementAll(dictionary, keys, step);
		}

		[ExcludeFromCodeCoverage]
		private static TestDelegate TestDelegate<TKey>(
			IDictionary<TKey, int> dictionary, IEnumerable<KeyValuePair<TKey, int>> keyStepPairs)
		{
			return () => Core.Extensions.ForIDictionary.IncrementAll(dictionary, keyStepPairs);
		}

		[Test]
		public void WhenGivenEnumerableOfKvpsWithNullKey_ShouldThrowArgumentException()
		{
			// Arrange
			var dictionary = new Dictionary<object, int>();
			var keys = new[] {new KeyValuePair<object, int>(null, 1)};

			// Act
			var code = TestDelegate(dictionary, keys);

			// Assert
			Assert.Throws<ArgumentException>(code);
		}

		[Test]
		public void WhenGivenEnumerableWithNull_ShouldThrowArgumentException()
		{
			// Arrange
			var dictionary = new Dictionary<object, int>();
			var keys = new object[] {null};

			// Act
			var code = TestDelegate(dictionary, keys);

			// Assert
			Assert.Throws<ArgumentException>(code);
		}

		[Test]
		public void WhenGivenEnumerableWithNullAndStep_ShouldThrowArgumentException()
		{
			// Arrange
			var dictionary = new Dictionary<object, int>();
			var keys = new object[] {null};
			const int step = 1;

			// Act
			var code = TestDelegate(dictionary, keys, step);

			// Assert
			Assert.Throws<ArgumentException>(code);
		}

		[Test]
		public void WhenGivenExistingKeys_ShouldIncrement([Values(-10, 0, 10)] int value1, [Values(-10, 0, 10)] int value2)
		{
			// Arrange
			var key1 = Guid.NewGuid().ToString();
			var key2 = Guid.NewGuid().ToString();
			var keys = new[] {key1, key2};
			var expected1 = value1 + 1;
			var expected2 = value2 + 1;
			var dictionary = new Dictionary<string, int> {[key1] = value1, [key2] = value2};

			// Act
			Core.Extensions.ForIDictionary.IncrementAll(dictionary, keys);

			// Assert
			Assert.AreEqual(expected1, dictionary[key1]);
			Assert.AreEqual(expected2, dictionary[key2]);
		}

		[Test]
		[Pairwise]
		public void WhenGivenExistingKeys_ShouldIncrementByStep(
			[Values(-10, 0, 10)] int value1, [Values(-10, 0, 10)] int value2, [Values(-1, 0, 1)] int step)
		{
			// Arrange
			var key1 = Guid.NewGuid().ToString();
			var key2 = Guid.NewGuid().ToString();
			var keys = new[] {key1, key2};
			var expected1 = value1 + step;
			var expected2 = value2 + step;
			var dictionary = new Dictionary<string, int> {[key1] = value1, [key2] = value2};

			// Act
			Core.Extensions.ForIDictionary.IncrementAll(dictionary, keys, step);

			// Assert
			Assert.AreEqual(expected1, dictionary[key1]);
			Assert.AreEqual(expected2, dictionary[key2]);
		}

		[Test]
		[Pairwise]
		public void WhenGivenExistingKeys_ShouldSetToPairedStep(
			[Values(-10, 0, 10)] int value1, [Values(-10, 0, 10)] int value2,
			[Values(-1, 0, 1)] int step1, [Values(-1, 0, 1)] int step2)
		{
			// Arrange
			var key1 = Guid.NewGuid().ToString();
			var key2 = Guid.NewGuid().ToString();
			var kvps = new Dictionary<string, int> {[key1] = step1, [key2] = step2};
			var expected1 = value1 + step1;
			var expected2 = value2 + step2;
			var dictionary = new Dictionary<string, int> {[key1] = value1, [key2] = value2};

			// Act
			Core.Extensions.ForIDictionary.IncrementAll(dictionary, kvps);

			// Assert
			Assert.AreEqual(expected1, dictionary[key1]);
			Assert.AreEqual(expected2, dictionary[key2]);
		}

		[Test]
		public void WhenGivenMissingKeys_ShouldSet()
		{
			// Arrange
			var key1 = Guid.NewGuid().ToString();
			var key2 = Guid.NewGuid().ToString();
			var keys = new[] {key1, key2};
			const int expected = 1;
			var dictionary = new Dictionary<string, int>();

			// Act
			Core.Extensions.ForIDictionary.IncrementAll(dictionary, keys);

			// Assert
			Assert.AreEqual(expected, dictionary[key1]);
			Assert.AreEqual(expected, dictionary[key2]);
		}

		[Test]
		public void WhenGivenMissingKeys_ShouldSetToPairedStep([Values(-1, 0, 1)] int step1, [Values(-1, 0, 1)] int step2)
		{
			// Arrange
			var key1 = Guid.NewGuid().ToString();
			var key2 = Guid.NewGuid().ToString();
			var kvps = new Dictionary<string, int> {[key1] = step1, [key2] = step2};
			var expected1 = step1;
			var expected2 = step2;
			var dictionary = new Dictionary<string, int>();

			// Act
			Core.Extensions.ForIDictionary.IncrementAll(dictionary, kvps);

			// Assert
			Assert.AreEqual(expected1, dictionary[key1]);
			Assert.AreEqual(expected2, dictionary[key2]);
		}

		[Test]
		public void WhenGivenMissingKeys_ShouldSetToStep([Values(-1, 0, 1)] int step)
		{
			// Arrange
			var key1 = Guid.NewGuid().ToString();
			var key2 = Guid.NewGuid().ToString();
			var keys = new[] {key1, key2};
			var expected = step;
			var dictionary = new Dictionary<string, int>();

			// Act
			Core.Extensions.ForIDictionary.IncrementAll(dictionary, keys, step);

			// Assert
			Assert.AreEqual(expected, dictionary[key1]);
			Assert.AreEqual(expected, dictionary[key2]);
		}

		[Test]
		public void WhenGivenNullDictionary_ShouldThrowArgumentNullException()
		{
			// Arrange
			var dictionary = null as IDictionary<object, int>;
			var keys = new object[0];

			// Act
			// ReSharper disable once ExpressionIsAlwaysNull
			var code = TestDelegate(dictionary, keys);

			// Assert
			Assert.Throws<ArgumentNullException>(code);
		}

		[Test]
		public void WhenGivenNullDictionaryAndKvps_ShouldThrowArgumentNullException()
		{
			// Arrange
			var dictionary = null as IDictionary<object, int>;
			var keys = new KeyValuePair<object, int>[0];

			// Act
			// ReSharper disable once ExpressionIsAlwaysNull
			var code = TestDelegate(dictionary, keys);

			// Assert
			Assert.Throws<ArgumentNullException>(code);
		}

		[Test]
		public void WhenGivenNullDictionaryAndStep_ShouldThrowArgumentNullException()
		{
			// Arrange
			var dictionary = null as IDictionary<object, int>;
			var keys = new object[0];
			const int step = 1;

			// Act
			// ReSharper disable once ExpressionIsAlwaysNull
			var code = TestDelegate(dictionary, keys, step);

			// Assert
			Assert.Throws<ArgumentNullException>(code);
		}

		[Test]
		public void WhenGivenNullEnumerable_ShouldThrowArgumentNullException()
		{
			// Arrange
			var dictionary = new Dictionary<object, int>();
			var keys = null as object[];

			// Act
			// ReSharper disable once ExpressionIsAlwaysNull
			var code = TestDelegate(dictionary, keys);

			// Assert
			Assert.Throws<ArgumentNullException>(code);
		}

		[Test]
		public void WhenGivenNullEnumerableAndStep_ShouldThrowArgumentNullException()
		{
			// Arrange
			var dictionary = new Dictionary<object, int>();
			var keys = null as object[];
			const int step = 1;

			// Act
			// ReSharper disable once ExpressionIsAlwaysNull
			var code = TestDelegate(dictionary, keys, step);

			// Assert
			Assert.Throws<ArgumentNullException>(code);
		}

		[Test]
		public void WhenGivenNullEnumerableOfKvps_ShouldThrowArgumentNullException()
		{
			// Arrange
			var dictionary = new Dictionary<object, int>();
			var keys = null as KeyValuePair<object, int>[];

			// Act
			// ReSharper disable once ExpressionIsAlwaysNull
			var code = TestDelegate(dictionary, keys);

			// Assert
			Assert.Throws<ArgumentNullException>(code);
		}
	}
}