using System;
using Xunit;

namespace Sleddog.ExtendedTrail.Tests
{
	public class WindowsServiceInstallerTests
	{
		[Fact]
		public void FirstFailureOptionIsNotNullOnInitialize()
		{
			var sut = new WindowsServiceInstaller();

			Assert.NotNull(sut.FirstFailure);
		}

		[Fact]
		public void FirstFailureDoesNotAcceptNull()
		{
			var sut = new WindowsServiceInstaller();

			Assert.Throws<ArgumentNullException>(() => sut.FirstFailure = null);
		}

		[Fact]
		public void SecondFailureOptionIsNotNullOnInitialize()
		{
			var sut = new WindowsServiceInstaller();

			Assert.NotNull(sut.SecondFailure);
		}

		[Fact]
		public void SecondFailureDoesNotAcceptNull()
		{
			var sut = new WindowsServiceInstaller();

			Assert.Throws<ArgumentNullException>(() => sut.SecondFailure = null);
		}

		[Fact]
		public void SubsequentFailureOptionIsNotNullOnInitialize()
		{
			var sut = new WindowsServiceInstaller();

			Assert.NotNull(sut.SubsequentFailure);
		}

		[Fact]
		public void SubsequentFailureDoesNotAcceptNull()
		{
			var sut = new WindowsServiceInstaller();

			Assert.Throws<ArgumentNullException>(() => sut.SubsequentFailure = null);
		}
	}
}