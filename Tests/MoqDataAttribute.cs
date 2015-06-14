using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using Ploeh.AutoFixture.NUnit2;

namespace BitFn.Core.Tests
{
	public class MoqDataAttribute : AutoDataAttribute
	{
		public MoqDataAttribute()
			: base(new Fixture().Customize(new AutoMoqCustomization()))
		{
		}
	}
}
