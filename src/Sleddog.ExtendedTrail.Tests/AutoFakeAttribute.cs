using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoFakeItEasy;
using Ploeh.AutoFixture.Xunit;

namespace Sleddog.ExtendedTrail.Tests
{
	public class AutoFakeAttribute : AutoDataAttribute
	{
		public AutoFakeAttribute() : base(new Fixture().Customize(new AutoFakeItEasyCustomization()))
		{
		}
	}
}