using Xunit;

namespace Sleddog.ExtendedTrail.Tests
{
	public class WindowsServiceInstallerTests
	{
		[Fact]
		public void CanInstantiate()
		{
			Assert.DoesNotThrow(() => new WindowsServiceInstaller());
		}

		[Fact]
		public void FirstFailureOptionAreNotNullOnInitialize()
		{
			var sut = new WindowsServiceInstaller();

			Assert.NotNull(sut.FirstFailure);
		}

		[Fact]
		public void SecondFailureOptionAreNotNullOnInitialize()
		{
			var sut = new WindowsServiceInstaller();

			Assert.NotNull(sut.SecondFailure);
		}

		[Fact]
		public void SubsequentFailureOptionAreNotNullOnInitialize()
		{
			var sut = new WindowsServiceInstaller();

			Assert.NotNull(sut.SubsequentFailure);
		}
	}
}