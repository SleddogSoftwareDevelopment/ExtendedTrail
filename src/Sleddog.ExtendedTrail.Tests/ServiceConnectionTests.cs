using System;
using FakeItEasy;
using Sleddog.ExtendedTrail.Internals;
using Sleddog.ExtendedTrail.Win32;
using Xunit.Extensions;

namespace Sleddog.ExtendedTrail.Tests
{
	public class ServiceConnectionTests
	{
		[Theory, AutoFake]
		public void CanOpenService(Fake<IAdvApi32> advApi32, int serviceControlManagerHandleValue, int serviceHandleValue, string serviceName)
		{
			advApi32.CallsTo(_ => _.OpenService(A<IntPtr>._, A<string>._, A<ScmAccess>._))
				.Returns(new IntPtr(serviceHandleValue));

			var sut = new ServiceConnection(advApi32.FakedObject);

			var connectionHandle = new ConnectionHandle {ServiceManagerHandle = new IntPtr(serviceControlManagerHandleValue)};

			sut.Open(connectionHandle, serviceName);

			advApi32.CallsTo(_ => _.OpenService(A<IntPtr>._, A<string>._, A<ScmAccess>._)).MustHaveHappened();
		}
	}
}