using System.Linq;
using NUnit.Framework;

namespace BitFn.Core.Tests.ThreadSafeRandom
{
	[TestFixture]
	public class RandomBytes
	{
		[Test]
		public void WhenGotten_ShouldReturnDelegate()
		{
			// Arrange / Act
			var actual = Core.ThreadSafeRandom.RandomBytes;

			// Assert
			Assert.IsNotNull(actual);
			Assert.IsInstanceOf<Core.RandomBytes>(actual);
		}

		[Test]
		public void WhenInvoked_ShouldFillBytes()
		{
			// Arrange
			var buffer = new byte[1024];
			var rng = Core.ThreadSafeRandom.RandomBytes;

			// Act
			rng.Invoke(buffer);

			// Assert
			Assert.IsTrue(buffer.Any(b => b != default(byte)));
		}
	}
}