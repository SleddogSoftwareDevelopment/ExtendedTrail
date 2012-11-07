using System;
using Xunit;

namespace Sleddog.ExtendedTrail.Tests
{
	public class WindowsServiceInstallerTests
	{
		[Fact]
		public void FirstFailureOptionAreNotNullOnInitialize()
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
		public void SecondFailureOptionAreNotNullOnInitialize()
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
		public void SubsequentFailureOptionAreNotNullOnInitialize()
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