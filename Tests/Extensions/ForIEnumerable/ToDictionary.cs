using System;
using System.Collections.Generic;
using NUnit.Framework;

// ReSharper disable InvokeAsExtensionMethod

namespace BitFn.Core.Tests.Extensions.ForIEnumerable
{
	[TestFixture]
	public class ToDictionary
	{
		[Test]
		public void WhenGivenKeyValues_ShouldReturnEqualDictionary()
		{
			// Arrange
			var enumerable = new[]
			{
				new KeyValuePair<string, int>("One", 1),
				new KeyValuePair<string, int>("Two", 2)
			};
			var expected = enumerable;

			// Act
			var actual = Core.Extensions.ForIEnumerable.ToDictionary(enumerable);

			// Assert
			CollectionAssert.AreEqual(expected, actual);
		}

		[Test]
		public void WhenGivenComparer_ShouldUseForResult()
		{
			// Arrange
			var enumerable = new[]
			{
				new KeyValuePair<string, int>("One", 1),
				new KeyValuePair<string, int>("Two", 2)
			};
			var comparer = StringComparer.OrdinalIgnoreCase;

			// Act
			var actual = Core.Extensions.ForIEnumerable.ToDictionary(enumerable, comparer);

			// Assert
			Assert.IsTrue(actual.ContainsKey("one"));
		}

		[Test]
		public void WhenGivenNullEnumerable_ShouldThrowArgumentNullException()
		{
			// Arrange
			var enumerable = null as IEnumerable<KeyValuePair<string, int>>;

			// Act
			// ReSharper disable once ExpressionIsAlwaysNull
			TestDelegate code = () => { Core.Extensions.ForIEnumerable.ToDictionary(enumerable); };

			// Assert
			Assert.Throws<ArgumentNullException>(code);
		}

		[Test]
		public void WhenGivenNullComparer_ShouldUseDefaultComparer()
		{
			// Arrange
			var enumerable = new[]
			{
				new KeyValuePair<string, int>("One", 1),
				new KeyValuePair<string, int>("Two", 2)
			};
			var comparer = null as IEqualityComparer<string>;

			// Act
			// ReSharper disable once ExpressionIsAlwaysNull
			var actual = Core.Extensions.ForIEnumerable.ToDictionary(enumerable, comparer);

			// Assert
			Assert.IsFalse(actual.ContainsKey("one"));
		}
	}
}