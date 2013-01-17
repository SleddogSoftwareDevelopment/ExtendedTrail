using System;
using System.ServiceProcess;
using Ploeh.AutoFixture.Xunit;
using Xunit;
using Xunit.Extensions;

namespace Sleddog.ExtendedTrail.Tests
{
	public class ServiceIdentityTests
	{
		[Fact]
		public void DoesNotAllowUserServiceAccountWithoutCredentials()
		{
			Assert.Throws<ArgumentOutOfRangeException>(() => new ServiceIdentity(ServiceAccount.User));
		}

		[Theory, InlineAutoData]
		public void ServiceAccountUsersIsSetBySupplyingLoginAndPass(string username, string password)
		{
			var sut = new ServiceIdentity(username, password);

			var expected = ServiceAccount.User;
			var actual = sut.ServiceAccount;

			Assert.Equal(expected, actual);
		}

		[Theory, InlineAutoData]
		public void UsernameIsSetCorrectly(string expected, string password)
		{
			var sut = new ServiceIdentity(expected, password);

			var actual = sut.UserName;

			Assert.Equal(expected, actual);
		}

		[Theory, InlineAutoData]
		public void PasswordIsSetCorrectly(string username, string expected)
		{
			var sut = new ServiceIdentity(username, expected);

			var actual = sut.Password;

			Assert.Equal(expected, actual);
		}

		[Theory]
		[InlineData(ServiceAccount.LocalService)]
		[InlineData(ServiceAccount.LocalSystem)]
		[InlineData(ServiceAccount.NetworkService)]
		public void AcceptsServiceAccountsThatAreNotUser(ServiceAccount serviceAccount)
		{
			Assert.DoesNotThrow(() => new ServiceIdentity(serviceAccount));
		}
	}
}