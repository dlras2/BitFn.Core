using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using NUnit.Framework;

// ReSharper disable InvokeAsExtensionMethod

namespace BitFn.Core.Tests.Extensions.ForExpression
{
	[TestFixture]
	public class GetPropertyInfo
	{
		[ExcludeFromCodeCoverage]
		private static TestDelegate TestDelegate<TSource, TProperty>(Expression<Func<TSource, TProperty>> lambda)
		{
			return () => Core.Extensions.ForExpression.GetPropertyInfo(lambda);
		}

		[ExcludeFromCodeCoverage]
		private class TestPoco
		{
#pragma warning disable 649
			public object Field;
#pragma warning restore 649
			public object Property { get; set; }
			public ChildPoco Child { get; set; }

			public object Method()
			{
				return new object();
			}
		}

		[ExcludeFromCodeCoverage]
		private class ChildPoco
		{
			public object Property { get; set; }
		}

		[Test]
		public void WhenGivenNull_ShouldThrowArgumentNullException()
		{
			// Arrange
			Expression<Func<TestPoco, object>> lambda = null;

			// Act
			// ReSharper disable once ExpressionIsAlwaysNull
			var code = TestDelegate(lambda);

			// Assert
			Assert.Throws<ArgumentNullException>(code);
		}

		[Test]
		public void WhenGivenPublicChildPropertyLambda_ShouldThrowArgumentException()
		{
			// Arrange
			Expression<Func<TestPoco, object>> lambda = (_ => _.Child.Property);

			// Act
			var code = TestDelegate(lambda);

			// Assert
			Assert.Throws<ArgumentException>(code);
		}

		[Test]
		public void WhenGivenPublicFieldLambda_ShouldThrowArgumentException()
		{
			// Arrange
			Expression<Func<TestPoco, object>> lambda = (_ => _.Field);

			// Act
			var code = TestDelegate(lambda);

			// Assert
			Assert.Throws<ArgumentException>(code);
		}

		[Test]
		public void WhenGivenPublicMethodLambda_ShouldThrowArgumentException()
		{
			// Arrange
			Expression<Func<TestPoco, object>> lambda = (_ => _.Method());

			// Act
			var code = TestDelegate(lambda);

			// Assert
			Assert.Throws<ArgumentException>(code);
		}

		[Test]
		public void WhenGivenPublicPropertyLambda_ShouldMatch()
		{
			// Arrange
			Expression<Func<TestPoco, object>> lambda = (_ => _.Property);

			// Act
			var actual = Core.Extensions.ForExpression.GetPropertyInfo(lambda);

			// Assert
			Assert.AreEqual(nameof(TestPoco.Property), actual.Name);
		}
	}
}