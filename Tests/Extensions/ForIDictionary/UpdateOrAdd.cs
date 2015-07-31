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
		private static Func<T, T> UnusedTransformDelegate<T>() => _ => { throw new ApplicationException(); };

		[ExcludeFromCodeCoverage]
		private static Func<T> UnusedFactoryDelegate<T>() => () => { throw new ApplicationException(); };

		[Test]
		public void WhenValueMissing_ShouldAdd()
		{
			// Arrange
			var key = Guid.NewGuid().ToString();
			var expected = Guid.NewGuid().ToString();
			var dictionary = new Dictionary<string, string>();
			var valueTransform = UnusedTransformDelegate<string>();

			// Act
			var actual = Core.Extensions.ForIDictionary.UpdateOrAdd(dictionary, key, valueTransform, expected);

			// Assert
			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void WhenValueExists_ShouldUpdate()
		{
			// Arrange
			var key = Guid.NewGuid().ToString();
			const string expected = "transformed original";
			var unexpected = string.Empty;
			var dictionary = new Dictionary<string, string> {[key] = "original"};
			Func<string, string> valueTransform = _ => "transformed " + _;

			// Act
			var actual = Core.Extensions.ForIDictionary.UpdateOrAdd(dictionary, key, valueTransform, unexpected);

			// Assert
			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void WhenValueMissingWithFactory_ShouldAdd()
		{
			// Arrange
			var key = Guid.NewGuid().ToString();
			var expected = Guid.NewGuid().ToString();
			Func<string> expectedFactory = () => expected;
			var dictionary = new Dictionary<string, string>();
			var valueTransform = UnusedTransformDelegate<string>();

			// Act
			var actual = Core.Extensions.ForIDictionary.UpdateOrAdd(dictionary, key, valueTransform, expectedFactory);

			// Assert
			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void WhenValueExistsWithFactory_ShouldUpdate()
		{
			// Arrange
			var key = Guid.NewGuid().ToString();
			const string expected = "transformed original";
			var expectedFactory = UnusedFactoryDelegate<string>();
			var dictionary = new Dictionary<string, string> {[key] = "original"};
			Func<string, string> valueTransform = _ => "transformed " + _;

			// Act
			var actual = Core.Extensions.ForIDictionary.UpdateOrAdd(dictionary, key, valueTransform, expectedFactory);

			// Assert
			Assert.AreEqual(expected, actual);
		}
	}
}