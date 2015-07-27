using NUnit.Framework;

namespace BitFn.Core.Tests.ThreadSafeRandom
{
	[TestFixture]
	public class RandomInt
	{
		[Test]
		public void WhenGotten_ShouldReturnDelegate()
		{
			// Arrange / Act
			var actual = Core.ThreadSafeRandom.RandomInt;

			// Assert
			Assert.IsNotNull(actual);
			Assert.IsInstanceOf<Core.RandomInt>(actual);
		}

		[Test]
		public void WhenInvoked_ShouldReturnPositiveInt()
		{
			// Arrange
			var rng = Core.ThreadSafeRandom.RandomInt;

			// Act
			var actual = rng.Invoke();

			// Assert
			Assert.True(actual >= 0);
		}
	}
}