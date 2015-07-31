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
		private static TestDelegate TestDelegate<TKey, TValue>(IDictionary<TKey, TValue> dictionary, TKey key, TValue value)
		{
			return () => Core.Extensions.ForIDictionary.GetOrAdd(dictionary, key, value);
		}

		[ExcludeFromCodeCoverage]
		private static TestDelegate TestDelegate<TKey, TValue>(IDictionary<TKey, TValue> dictionary, TKey key,
			Func<TValue> valueFactory)
		{
			return () => Core.Extensions.ForIDictionary.GetOrAdd(dictionary, key, valueFactory);
		}

		[ExcludeFromCodeCoverage]
		private static object FactoryDelegate(object obj) => obj;

		[Test]
		public void WhenValueExists_ShouldGet()
		{
			// Arrange
			var key = Guid.NewGuid().ToString();
			var expected = new object();
			var unexpected = new object();
			var dictionary = new Dictionary<string, object> {[key] = expected};

			// Act
			var actual = Core.Extensions.ForIDictionary.GetOrAdd(dictionary, key, unexpected);

			// Assert
			Assert.AreSame(expected, actual);
		}

		[Test]
		public void WhenValueMissing_ShouldGet()
		{
			// Arrange
			var key = Guid.NewGuid().ToString();
			var expected = new object();
			var dictionary = new Dictionary<string, object>();

			// Act
			var actual = Core.Extensions.ForIDictionary.GetOrAdd(dictionary, key, expected);

			// Assert
			Assert.AreSame(expected, actual);
		}

		[Test]
		public void WhenValueExistsWithFactory_ShouldGet()
		{
			// Arrange
			var key = Guid.NewGuid().ToString();
			var expected = new object();
			var unexpected = new object();
			var dictionary = new Dictionary<string, object> {[key] = expected};

			// Act
			var actual = Core.Extensions.ForIDictionary.GetOrAdd(dictionary, key, FactoryDelegate(unexpected));

			// Assert
			Assert.AreSame(expected, actual);
		}

		[Test]
		public void WhenValueMissingWithFactory_ShouldGet()
		{
			// Arrange
			var key = Guid.NewGuid().ToString();
			var expected = new object();
			Func<object> expectedFactory = () => expected;
			var dictionary = new Dictionary<string, object>();

			// Act
			var actual = Core.Extensions.ForIDictionary.GetOrAdd(dictionary, key, expectedFactory);

			// Assert
			Assert.AreSame(expected, actual);
		}

		[Test]
		public void WhenGivenNullDictionary_ShouldThrowArgumentNullException()
		{
			// Arrange
			var dictionary = null as Dictionary<object, object>;
			var key = new object();
			var value = new object();

			// Act
			// ReSharper disable once ExpressionIsAlwaysNull
			var code = TestDelegate(dictionary, key, value);

			// Assert
			Assert.Throws<ArgumentNullException>(code);
		}

		[Test]
		public void WhenGivenNullKey_ShouldThrowArgumentNullException()
		{
			// Arrange
			var dictionary = new Dictionary<object, object>();
			var key = null as object;
			var value = new object();

			// Act
			// ReSharper disable once ExpressionIsAlwaysNull
			var code = TestDelegate(dictionary, key, value);

			// Assert
			Assert.Throws<ArgumentNullException>(code);
		}

		[Test]
		public void WhenGivenNullDictionaryWithFactory_ShouldThrowArgumentNullException()
		{
			// Arrange
			var dictionary = null as Dictionary<object, object>;
			var key = new object();
			var value = new object();

			// Act
			// ReSharper disable once ExpressionIsAlwaysNull
			var code = TestDelegate(dictionary, key, FactoryDelegate(value));

			// Assert
			Assert.Throws<ArgumentNullException>(code);
		}

		[Test]
		public void WhenGivenNullKeyWithFactory_ShouldThrowArgumentNullException()
		{
			// Arrange
			var dictionary = new Dictionary<object, object>();
			var key = null as object;
			var value = new object();

			// Act
			// ReSharper disable once ExpressionIsAlwaysNull
			var code = TestDelegate(dictionary, key, FactoryDelegate(value));

			// Assert
			Assert.Throws<ArgumentNullException>(code);
		}

		[Test]
		public void WhenGivenNullFactory_ShouldThrowArgumentNullException()
		{
			// Arrange
			var dictionary = new Dictionary<object, object>();
			var key = new object();
			var valueFactory = null as Func<object>;

			// Act
			// ReSharper disable once ExpressionIsAlwaysNull
			var code = TestDelegate(dictionary, key, valueFactory);

			// Assert
			Assert.Throws<ArgumentNullException>(code);
		}
	}
}