using NUnit.Framework;

namespace BitFn.Core.Tests.ThreadSafeRandom
{
	[TestFixture]
	public class RandomIntUnder
	{
		[Test]
		public void WhenGotten_ShouldReturnDelegate()
		{
			// Arrange / Act
			var actual = Core.ThreadSafeRandom.RandomIntUnder;

			// Assert
			Assert.IsNotNull(actual);
			Assert.IsInstanceOf<Core.RandomIntUnder>(actual);
		}

		[Test]
		public void WhenInvoked_ShouldReturnIntBetween()
		{
			// Arrange
			const int max = 1;
			const int expected = 0;
			var rng = Core.ThreadSafeRandom.RandomIntUnder;

			// Act
			var actual = rng.Invoke(max);

			// Assert
			Assert.AreEqual(expected, actual);
		}
	}
}