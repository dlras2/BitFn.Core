using NUnit.Framework;

namespace BitFn.Core.Tests.ThreadSafeRandom
{
	[TestFixture]
	public class RandomIntBetween
	{
		[Test]
		public void WhenGotten_ShouldReturnDelegate()
		{
			// Arrange / Act
			var actual = Core.ThreadSafeRandom.RandomIntBetween;

			// Assert
			Assert.IsNotNull(actual);
			Assert.IsInstanceOf<Core.RandomIntBetween>(actual);
		}

		[Test]
		public void WhenInvoked_ShouldReturnIntBetween()
		{
			// Arrange
			const int min = 0;
			const int max = 1;
			const int expected = 0;
			var rng = Core.ThreadSafeRandom.RandomIntBetween;

			// Act
			var actual = rng.Invoke(min, max);

			// Assert
			Assert.AreEqual(expected, actual);
		}
	}
}