using NUnit.Framework;

namespace BitFn.Core.Tests.ThreadSafeRandom
{
	[TestFixture]
	public class RandomDouble
	{
		[Test]
		public void WhenGotten_ShouldReturnDelegate()
		{
			// Arrange / Act
			var actual = Core.ThreadSafeRandom.RandomDouble;

			// Assert
			Assert.IsNotNull(actual);
			Assert.IsInstanceOf<Core.RandomDouble>(actual);
		}

		[Test]
		public void WhenInvoked_ShouldReturnDoubleBetweenZeroAndOne()
		{
			// Arrange
			var rng = Core.ThreadSafeRandom.RandomDouble;

			// Act
			var actual = rng.Invoke();

			// Assert
			Assert.True(actual >= 0);
			Assert.True(actual < 1);
		}
	}
}