using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;

// ReSharper disable InvokeAsExtensionMethod

namespace BitFn.Core.Tests.Extensions.ForIDictionary
{
	[TestFixture]
	public class UpdateOrAdd
	{
		[ExcludeFromCodeCoverage]
		private static TestDelegate TestDelegate<TKey, TValue>(
			IDictionary<TKey, TValue> dictionary, TKey key, Func<TValue, TValue> valueTransform, TValue newValue)
		{
			return () => Core.Extensions.ForIDictionary.UpdateOrAdd(dictionary, key, valueTransform, newValue);
		}

		[ExcludeFromCodeCoverage]
		private static TestDelegate TestDelegate<TKey, TValue>(
			IDictionary<TKey, TValue> dictionary, TKey key, Func<TValue, TValue> valueTransform, Func<TValue> newValueFactory)
		{
			return () => Core.Extensions.ForIDictionary.UpdateOrAdd(dictionary, key, valueTransform, newValueFactory);
		}

		[ExcludeFromCodeCoverage]
		private static Func<T, T> TransformDelegate<T>() => _ => { throw new ApplicationException(); };

		[ExcludeFromCodeCoverage]
		private static Func<T> FactoryDelegate<T>() => () => { throw new ApplicationException(); };

		[Test]
		public void WhenValueMissing_ShouldReturnNewValue()
		{
			// Arrange
			var key = Guid.NewGuid().ToString();
			var expected = Guid.NewGuid();
			var dictionary = new Dictionary<string, Guid>();

			// Act
			var actual = Core.Extensions.ForIDictionary.UpdateOrAdd(dictionary, key, TransformDelegate<Guid>(), expected);

			// Assert
			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void WhenValueExists_ShouldReturnTransformedOldValue()
		{
			// Arrange
			var key = Guid.NewGuid().ToString();
			var original = Guid.NewGuid().ToString();
			var transform = Guid.NewGuid().ToString();
			var expected = $"{original}-{transform}";
			var dictionary = new Dictionary<string, string> {[key] = original};
			Func<string, string> valueTransform = _ => $"{_}-{transform}";

			// Act
			var actual = Core.Extensions.ForIDictionary.UpdateOrAdd(dictionary, key, valueTransform, null as string);

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
			var actual = Core.Extensions.ForIDictionary.UpdateOrAdd(dictionary, key, TransformDelegate<Guid>(), expectedFactory);

			// Assert
			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void WhenValueExistsWithFactory_ShouldReturnTransformedOldValue()
		{
			// Arrange
			var key = Guid.NewGuid().ToString();
			var original = Guid.NewGuid().ToString();
			var transform = Guid.NewGuid().ToString();
			var expected = $"{original}-{transform}";
			var dictionary = new Dictionary<string, string> {[key] = original};
			Func<string, string> valueTransform = _ => $"{_}-{transform}";

			// Act
			var actual = Core.Extensions.ForIDictionary.UpdateOrAdd(dictionary, key, valueTransform, FactoryDelegate<string>());

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
			Core.Extensions.ForIDictionary.UpdateOrAdd(dictionary, key, TransformDelegate<Guid>(), expected);
			var actual = dictionary[key];

			// Assert
			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void WhenValueExists_ShouldSetToTransformedOldValue()
		{
			// Arrange
			var key = Guid.NewGuid().ToString();
			var original = Guid.NewGuid().ToString();
			var transform = Guid.NewGuid().ToString();
			var expected = $"{original}-{transform}";
			var dictionary = new Dictionary<string, string> {[key] = original};
			Func<string, string> valueTransform = _ => $"{_}-{transform}";

			// Act
			Core.Extensions.ForIDictionary.UpdateOrAdd(dictionary, key, valueTransform, null as string);
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
			Core.Extensions.ForIDictionary.UpdateOrAdd(dictionary, key, TransformDelegate<Guid>(), expectedFactory);
			var actual = dictionary[key];

			// Assert
			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void WhenValueExistsWithFactory_ShouldSetToTransformedOldValue()
		{
			// Arrange
			var key = Guid.NewGuid().ToString();
			var original = Guid.NewGuid().ToString();
			var transform = Guid.NewGuid().ToString();
			var expected = $"{original}-{transform}";
			var dictionary = new Dictionary<string, string> {[key] = original};
			Func<string, string> valueTransform = _ => $"{_}-{transform}";

			// Act
			Core.Extensions.ForIDictionary.UpdateOrAdd(dictionary, key, valueTransform, FactoryDelegate<string>());
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
			var code = TestDelegate(dictionary, key, TransformDelegate<Guid>(), Guid.NewGuid());

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
			var code = TestDelegate(dictionary, key, TransformDelegate<Guid>(), Guid.NewGuid());

			// Assert
			Assert.Throws<ArgumentNullException>(code);
		}

		[Test]
		public void WhenGivenNullTransform_ShouldThrowArgumentNullException()
		{
			// Arrange
			var dictionary = new Dictionary<string, Guid>();
			var key = Guid.NewGuid().ToString();
			var transform = null as Func<Guid, Guid>;

			// Act
			// ReSharper disable once ExpressionIsAlwaysNull
			var code = TestDelegate(dictionary, key, transform, Guid.NewGuid());

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
			var code = TestDelegate(dictionary, key, TransformDelegate<Guid>(), FactoryDelegate<Guid>());

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
			var code = TestDelegate(dictionary, key, TransformDelegate<Guid>(), FactoryDelegate<Guid>());

			// Assert
			Assert.Throws<ArgumentNullException>(code);
		}

		[Test]
		public void WhenGivenNullTransformWithFactory_ShouldThrowArgumentNullException()
		{
			// Arrange
			var dictionary = new Dictionary<string, Guid>();
			var key = Guid.NewGuid().ToString();
			var transform = null as Func<Guid, Guid>;

			// Act
			// ReSharper disable once ExpressionIsAlwaysNull
			var code = TestDelegate(dictionary, key, transform, FactoryDelegate<Guid>());

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
			var code = TestDelegate(dictionary, key, TransformDelegate<Guid>(), factory);

			// Assert
			Assert.Throws<ArgumentNullException>(code);
		}
	}
}