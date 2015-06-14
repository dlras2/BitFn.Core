using System;
using NUnit.Framework;

namespace BitFn.Core.Tests.ThreadSafeRandom
{
	[TestFixture]
	public class Local
	{
		[Test]
		public void WhenGotten_ShouldReturnRandom()
		{
			// Arrange / Act
			var actual = Core.ThreadSafeRandom.Local;

			// Assert
			Assert.IsInstanceOf<Random>(actual);
		}
	}
}