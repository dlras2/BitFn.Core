using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;

// ReSharper disable InvokeAsExtensionMethod

namespace BitFn.Core.Tests.Extensions.ForIDictionary
{
	[TestFixture]
	public class AddTo
	{
		[ExcludeFromCodeCoverage]
		private static TestDelegate TestDelegate<TKey, TValue>(
			IDictionary<TKey, IList<TValue>> dictionary, TKey key, TValue value)
		{
			return () => Core.Extensions.ForIDictionary.AddTo(dictionary, key, value);
		}

		[ExcludeFromCodeCoverage]
		private static TestDelegate TestDelegate<TKey, TValue>(
			IDictionary<TKey, ISet<TValue>> dictionary, TKey key, TValue value)
		{
			return () => Core.Extensions.ForIDictionary.AddTo(dictionary, key, value);
		}

		[Test]
		public void WhenGivenNullListDictionary_ShouldThrowArgumentNullException()
		{
			// Arrange
			var dictionary = null as IDictionary<string, IList<Guid>>;
			var key = Guid.NewGuid().ToString();

			// Act
			// ReSharper disable once ExpressionIsAlwaysNull
			var code = TestDelegate(dictionary, key, Guid.NewGuid());

			// Assert
			Assert.Throws<ArgumentNullException>(code);
		}

		[Test]
		public void WhenGivenNullListKey_ShouldThrowArgumentNullException()
		{
			// Arrange
			var dictionary = new Dictionary<string, IList<Guid>>();
			var key = null as string;

			// Act
			// ReSharper disable once ExpressionIsAlwaysNull
			var code = TestDelegate(dictionary, key, Guid.NewGuid());

			// Assert
			Assert.Throws<ArgumentNullException>(code);
		}

		[Test]
		public void WhenGivenNullSetDictionary_ShouldThrowArgumentNullException()
		{
			// Arrange
			var dictionary = null as IDictionary<string, ISet<Guid>>;
			var key = Guid.NewGuid().ToString();

			// Act
			// ReSharper disable once ExpressionIsAlwaysNull
			var code = TestDelegate(dictionary, key, Guid.NewGuid());

			// Assert
			Assert.Throws<ArgumentNullException>(code);
		}

		[Test]
		public void WhenGivenNullSetKey_ShouldThrowArgumentNullException()
		{
			// Arrange
			var dictionary = new Dictionary<string, ISet<Guid>>();
			var key = null as string;

			// Act
			// ReSharper disable once ExpressionIsAlwaysNull
			var code = TestDelegate(dictionary, key, Guid.NewGuid());

			// Assert
			Assert.Throws<ArgumentNullException>(code);
		}

		[Test]
		public void WhenListMissing_ShouldAddNewList()
		{
			// Arrange
			var key = Guid.NewGuid().ToString();
			var value = Guid.NewGuid();
			var dictionary = new Dictionary<string, IList<Guid>>();

			// Act
			Core.Extensions.ForIDictionary.AddTo(dictionary, key, value);
			var actual = dictionary[key];

			// Assert
			Assert.IsTrue(actual.Contains(value));
		}

		[Test]
		public void WhenSetMissing_ShouldAddNewSet()
		{
			// Arrange
			var key = Guid.NewGuid().ToString();
			var value = Guid.NewGuid();
			var dictionary = new Dictionary<string, ISet<Guid>>();

			// Act
			Core.Extensions.ForIDictionary.AddTo(dictionary, key, value);
			var actual = dictionary[key];

			// Assert
			Assert.IsTrue(actual.Contains(value));
		}

		[Test]
		public void WhenSetMissing_ShouldReturnTrue()
		{
			// Arrange
			var key = Guid.NewGuid().ToString();
			var value = Guid.NewGuid();
			var dictionary = new Dictionary<string, ISet<Guid>>();

			// Act
			var actual = Core.Extensions.ForIDictionary.AddTo(dictionary, key, value);

			// Assert
			Assert.IsTrue(actual);
		}

		[Test]
		public void WhenListExistsButValueMissing_ShouldAddToList()
		{
			// Arrange
			var key = Guid.NewGuid().ToString();
			var value = Guid.NewGuid();
			var list = new List<Guid>();
			var dictionary = new Dictionary<string, IList<Guid>> {[key] = list };

			// Act
			Core.Extensions.ForIDictionary.AddTo(dictionary, key, value);
			var actual = dictionary[key];

			// Assert
			Assert.AreSame(list, actual);
			Assert.IsTrue(actual.Contains(value));
		}

		[Test]
		public void WhenSetExistsButValueMissing_ShouldAddToSet()
		{
			// Arrange
			var key = Guid.NewGuid().ToString();
			var value = Guid.NewGuid();
			var set = new HashSet<Guid>();
			var dictionary = new Dictionary<string, ISet<Guid>> {[key] = set };

			// Act
			Core.Extensions.ForIDictionary.AddTo(dictionary, key, value);
			var actual = dictionary[key];

			// Assert
			Assert.AreSame(set, actual);
			Assert.IsTrue(actual.Contains(value));
		}

		[Test]
		public void WhenSetExistsButValueMissing_ShouldReturnTrue()
		{
			// Arrange
			var key = Guid.NewGuid().ToString();
			var value = Guid.NewGuid();
			var set = new HashSet<Guid>();
			var dictionary = new Dictionary<string, ISet<Guid>> {[key] = set };

			// Act
			var actual = Core.Extensions.ForIDictionary.AddTo(dictionary, key, value);

			// Assert
			Assert.IsTrue(actual);
		}

		[Test]
		public void WhenListAndValueExists_ShouldAddToList()
		{
			// Arrange
			var key = Guid.NewGuid().ToString();
			var value = Guid.NewGuid();
			var list = new List<Guid> { value };
			var dictionary = new Dictionary<string, IList<Guid>> {[key] = list };
			const int expectedCount = 2;

			// Act
			Core.Extensions.ForIDictionary.AddTo(dictionary, key, value);
			var actual = dictionary[key];

			// Assert
			Assert.AreSame(list, actual);
			Assert.AreEqual(expectedCount, actual.Count);
		}

		[Test]
		public void WhenSetAndValueExists_ShouldNotAddToSet()
		{
			// Arrange
			var key = Guid.NewGuid().ToString();
			var value = Guid.NewGuid();
			var set = new HashSet<Guid> { value };
			var dictionary = new Dictionary<string, ISet<Guid>> {[key] = set };
			const int expectedCount = 1;

			// Act
			Core.Extensions.ForIDictionary.AddTo(dictionary, key, value);
			var actual = dictionary[key];

			// Assert
			Assert.AreSame(set, actual);
			Assert.AreEqual(expectedCount, actual.Count);
		}

		[Test]
		public void WhenSetAndValueExists_ShouldReturnFalse()
		{
			// Arrange
			var key = Guid.NewGuid().ToString();
			var value = Guid.NewGuid();
			var set = new HashSet<Guid> {value};
			var dictionary = new Dictionary<string, ISet<Guid>> {[key] = set };

			// Act
			var actual = Core.Extensions.ForIDictionary.AddTo(dictionary, key, value);

			// Assert
			Assert.IsFalse(actual);
		}
	}
}