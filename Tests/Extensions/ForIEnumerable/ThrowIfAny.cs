using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using NUnit.Framework;

// ReSharper disable InvokeAsExtensionMethod

namespace BitFn.Core.Tests.Extensions.ForIEnumerable
{
	[TestFixture]
	public class ThrowIfAny
	{
		[ExcludeFromCodeCoverage]
		public static TestDelegate TestDelegate<TSource, TException>(
			IEnumerable<TSource> source, Func<TSource, bool> predicate)
			where TException : Exception, new()
		{
			// ReSharper disable once ReturnValueOfPureMethodIsNotUsed
			return () => Core.Extensions.ForIEnumerable.ThrowIfAny<TSource, TException>(source, predicate).ToList();
		}

		[ExcludeFromCodeCoverage]
		public static TestDelegate TestDelegate<TSource, TException>(
			IEnumerable<TSource> source, Func<TSource, bool> predicate, TException exception)
			where TException : Exception
		{
			// ReSharper disable once ReturnValueOfPureMethodIsNotUsed
			return () => Core.Extensions.ForIEnumerable.ThrowIfAny(source, predicate, exception).ToList();
		}

		[ExcludeFromCodeCoverage]
		public static TestDelegate TestDelegate<TSource, TException>(
			IEnumerable<TSource> source, Func<TSource, bool> predicate, Func<TSource, TException> exceptionFactory)
			where TException : Exception
		{
			// ReSharper disable once ReturnValueOfPureMethodIsNotUsed
			return () => Core.Extensions.ForIEnumerable.ThrowIfAny(source, predicate, exceptionFactory).ToList();
		}

		[Test]
		public void WhenNoElementsMatch_ShouldMaintainSource()
		{
			// Arrange
			var expected = new[] {1, 1, 2, 3, 3, 4};
			var predicate = (Func<int, bool>) (_ => _ <= 0);

			// Act
			var actual = Core.Extensions.ForIEnumerable.ThrowIfAny<int, Exception>(expected, predicate).ToList();

			// Assert
			CollectionAssert.AreEqual(expected, actual);
		}

		[Test]
		public void WhenAnElementMatches_ShouldThrow()
		{
			// Arrange
			var expected = new[] {1, 1, 2, 3, 3, 4, 5};
			var predicate = (Func<int, bool>) (_ => _ >= 5);

			// Act
			var code = TestDelegate<int, IndexOutOfRangeException>(expected, predicate);

			// Assert
			Assert.Throws<IndexOutOfRangeException>(code);
		}

		[Test]
		public void WhenAnElementMatchesWithException_ShouldThrow()
		{
			// Arrange
			var expected = new[] {1, 1, 2, 3, 3, 4, 5};
			var predicate = (Func<int, bool>) (_ => _ >= 5);

			// Act
			var code = TestDelegate(expected, predicate, new IndexOutOfRangeException("Message"));

			// Assert
			Assert.Throws<IndexOutOfRangeException>(code, "Message");
		}

		[Test]
		public void WhenAnElementMatchesWithFactory_ShouldThrow()
		{
			// Arrange
			var expected = new[] {1, 1, 2, 3, 3, 4, 5};
			var predicate = (Func<int, bool>) (_ => _ >= 5);

			// Act
			var code = TestDelegate(expected, predicate, _ => new IndexOutOfRangeException($"'{_}' >= 5"));

			// Assert
			Assert.Throws<IndexOutOfRangeException>(code, "'5' >= 5");
		}
	}
}