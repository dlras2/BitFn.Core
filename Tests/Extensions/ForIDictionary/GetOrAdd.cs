using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;

// ReSharper disable InvokeAsExtensionMethod

namespace BitFn.Core.Tests.Extensions.ForIDictionary
{
	[TestFixture]
	public class GetOrAdd
	{
		[ExcludeFromCodeCoverage]
		private static TestDelegate TestDelegate<TKey, TValue>(
			IDictionary<TKey, TValue> dictionary, TKey key, TValue newValue)
		{
			return () => Core.Extensions.ForIDictionary.GetOrAdd(dictionary, key, newValue);
		}

		[ExcludeFromCodeCoverage]
		private static TestDelegate TestDelegate<TKey, TValue>(
			IDictionary<TKey, TValue> dictionary, TKey key, Func<TValue> newValueFactory)
		{
			return () => Core.Extensions.ForIDictionary.GetOrAdd(dictionary, key, newValueFactory);
		}

		[ExcludeFromCodeCoverage]
		private static Func<T> FactoryDelegate<T>() => () => { throw new Exception(); };

		[Test]
		public void WhenValueExists_ShouldReturnOldValue()
		{
			// Arrange
			var key = Guid.NewGuid().ToString();
			var expected = Guid.NewGuid();
			var dictionary = new Dictionary<string, Guid> {[key] = expected};

			// Act
			var actual = Core.Extensions.ForIDictionary.GetOrAdd(dictionary, key, Guid.NewGuid());

			// Assert
			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void WhenValueMissing_ShouldReturnNewValue()
		{
			// Arrange
			var key = Guid.NewGuid().ToString();
			var expected = Guid.NewGuid();
			var dictionary = new Dictionary<string, Guid>();

			// Act
			var actual = Core.Extensions.ForIDictionary.GetOrAdd(dictionary, key, expected);

			// Assert
			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void WhenValueExistsWithFactory_ShouldReturnOldValue()
		{
			// Arrange
			var key = Guid.NewGuid().ToString();
			var expected = Guid.NewGuid();
			var dictionary = new Dictionary<string, Guid> {[key] = expected};

			// Act
			var actual = Core.Extensions.ForIDictionary.GetOrAdd(dictionary, key, FactoryDelegate<Guid>());

			// Assert
			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void WhenValueMissingWithFactory_ShouldReturnNewValue()
		{
			// Arrange
			var key = Guid.NewGuid().ToString();
			var expected = Guid.NewGuid();
			var expectedFactory = (Func<Guid>) (() => expected);
			var dictionary = new Dictionary<string, Guid>();

			// Act
			var actual = Core.Extensions.ForIDictionary.GetOrAdd(dictionary, key, expectedFactory);

			// Assert
			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void WhenValueExists_ShouldRetainOldValue()
		{
			// Arrange
			var key = Guid.NewGuid().ToString();
			var expected = Guid.NewGuid();
			var dictionary = new Dictionary<string, Guid> {[key] = expected};

			// Act
			Core.Extensions.ForIDictionary.GetOrAdd(dictionary, key, Guid.NewGuid());
			var actual = dictionary[key];

			// Assert
			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void WhenValueMissing_ShouldSetToNewValue()
		{
			// Arrange
			var key = Guid.NewGuid().ToString();
			var expected = Guid.NewGuid();
			var dictionary = new Dictionary<string, Guid>();

			// Act
			Core.Extensions.ForIDictionary.GetOrAdd(dictionary, key, expected);
			var actual = dictionary[key];

			// Assert
			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void WhenValueExistsWithFactory_ShouldRetainOldValue()
		{
			// Arrange
			var key = Guid.NewGuid().ToString();
			var expected = Guid.NewGuid();
			var dictionary = new Dictionary<string, Guid> {[key] = expected};

			// Act
			Core.Extensions.ForIDictionary.GetOrAdd(dictionary, key, FactoryDelegate<Guid>());
			var actual = dictionary[key];

			// Assert
			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void WhenValueMissingWithFactory_ShouldSetToNewValue()
		{
			// Arrange
			var key = Guid.NewGuid().ToString();
			var expected = Guid.NewGuid();
			var expectedFactory = (Func<Guid>) (() => expected);
			var dictionary = new Dictionary<string, Guid>();

			// Act
			Core.Extensions.ForIDictionary.GetOrAdd(dictionary, key, expectedFactory);
			var actual = dictionary[key];

			// Assert
			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void WhenGivenNullDictionary_ShouldThrowArgumentNullException()
		{
			// Arrange
			var dictionary = null as IDictionary<string, Guid>;
			var key = Guid.NewGuid().ToString();

			// Act
			// ReSharper disable once ExpressionIsAlwaysNull
			var code = TestDelegate(dictionary, key, Guid.NewGuid());

			// Assert
			Assert.Throws<ArgumentNullException>(code);
		}

		[Test]
		public void WhenGivenNullKey_ShouldThrowArgumentNullException()
		{
			// Arrange
			var dictionary = new Dictionary<string, Guid>();
			var key = null as string;

			// Act
			// ReSharper disable once ExpressionIsAlwaysNull
			var code = TestDelegate(dictionary, key, Guid.NewGuid());

			// Assert
			Assert.Throws<ArgumentNullException>(code);
		}

		[Test]
		public void WhenGivenNullDictionaryWithFactory_ShouldThrowArgumentNullException()
		{
			// Arrange
			var dictionary = null as IDictionary<string, Guid>;
			var key = Guid.NewGuid().ToString();

			// Act
			// ReSharper disable once ExpressionIsAlwaysNull
			var code = TestDelegate(dictionary, key, FactoryDelegate<Guid>());

			// Assert
			Assert.Throws<ArgumentNullException>(code);
		}

		[Test]
		public void WhenGivenNullKeyWithFactory_ShouldThrowArgumentNullException()
		{
			// Arrange
			var dictionary = new Dictionary<string, Guid>();
			var key = null as string;

			// Act
			// ReSharper disable once ExpressionIsAlwaysNull
			var code = TestDelegate(dictionary, key, FactoryDelegate<Guid>());

			// Assert
			Assert.Throws<ArgumentNullException>(code);
		}

		[Test]
		public void WhenGivenNullFactory_ShouldThrowArgumentNullException()
		{
			// Arrange
			var dictionary = new Dictionary<string, Guid>();
			var key = Guid.NewGuid().ToString();
			var factory = null as Func<Guid>;

			// Act
			// ReSharper disable once ExpressionIsAlwaysNull
			var code = TestDelegate(dictionary, key, factory);

			// Assert
			Assert.Throws<ArgumentNullException>(code);
		}
	}
}