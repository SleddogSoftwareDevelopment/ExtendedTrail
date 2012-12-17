using System;
using FakeItEasy;
using Sleddog.ExtendedTrail.Internals;
using Sleddog.ExtendedTrail.Win32;
using Xunit;
using Xunit.Extensions;

namespace Sleddog.ExtendedTrail.Tests
{
	public class ServiceConnectionTests
	{
		[Theory, AutoFake]
		public void OpenServiceRequiresConnectionHandle(Fake<IAdvApi32> advApi32, string serviceName)
		{
			var sut = new ServiceConnection(advApi32.FakedObject);

			Assert.Throws<ArgumentNullException>(() => sut.Open(null, serviceName));
		}

		[Theory, AutoFake]
		public void OpenServiceRequiresServiceName(Fake<IAdvApi32> advApi32, int serviceControlManagerHandleValue)
		{
			var sut = new ServiceConnection(advApi32.FakedObject);

			var connectionHandle = new ConnectionHandle {ServiceManagerHandle = new IntPtr(serviceControlManagerHandleValue)};

			Assert.Throws<ArgumentNullException>(() => sut.Open(connectionHandle, null));
		}

		[Theory, AutoFake]
		public void OpenServiceRequiresOpenServiceControlManager(Fake<IAdvApi32> advApi32, string serviceName)
		{
			var sut = new ServiceConnection(advApi32.FakedObject);

			var connectionHandle = new ConnectionHandle();

			Assert.Throws<MissingServiceManagerConnectionException>(() => sut.Open(connectionHandle, serviceName));
		}

		[Theory, AutoFake]
		public void OpenServiceCallsUnderlyingApi(Fake<IAdvApi32> advApi32, int serviceControlManagerHandleValue, int serviceHandleValue,
		                                          string serviceName)
		{
			advApi32.CallsTo(_ => _.OpenService(A<IntPtr>._, A<string>._, A<ScmAccess>._))
			        .Returns(new IntPtr(serviceHandleValue));

			var sut = new ServiceConnection(advApi32.FakedObject);

			var connectionHandle = new ConnectionHandle {ServiceManagerHandle = new IntPtr(serviceControlManagerHandleValue)};

			sut.Open(connectionHandle, serviceName);

			advApi32.CallsTo(_ => _.OpenService(A<IntPtr>._, A<string>._, A<ScmAccess>._)).MustHaveHappened();
		}

		[Theory, AutoFake]
		public void OpenServiceSetsConnectionHandleToNotIntPtrZero(Fake<IAdvApi32> advApi32, int serviceControlManagerHandleValue,
		                                                           int serviceHandleValue, string serviceName)
		{
			advApi32.CallsTo(_ => _.OpenService(A<IntPtr>._, A<string>._, A<ScmAccess>._))
			        .Returns(new IntPtr(serviceHandleValue));

			var sut = new ServiceConnection(advApi32.FakedObject);

			var connectionHandle = new ConnectionHandle {ServiceManagerHandle = new IntPtr(serviceControlManagerHandleValue)};

			sut.Open(connectionHandle, serviceName);

			var actual = connectionHandle.ServiceHandle;

			var expected = new IntPtr(serviceHandleValue);

			Assert.Equal(expected, actual);
		}

		[Theory, AutoFake]
		public void CloseServiceRequiresServiceName(Fake<IAdvApi32> advApi32)
		{
			var sut = new ServiceConnection(advApi32.FakedObject);

			Assert.Throws<ArgumentNullException>(() => sut.Close(null));
		}

		[Theory, AutoFake]
		public void CloseServiceRequiresOpenServiceControlManager(Fake<IAdvApi32> advApi32)
		{
			var sut = new ServiceConnection(advApi32.FakedObject);

			var connectionHandle = new ConnectionHandle();

			Assert.Throws<MissingServiceManagerConnectionException>(() => sut.Close(connectionHandle));
		}

		[Theory, AutoFake]
		public void FailedCloseServiceDoesNotResetConnectionHandle(Fake<IAdvApi32> advApi32, int serviceControlManagerHandleValue,
		                                                           int serviceHandleValue)
		{
			advApi32.CallsTo(_ => _.CloseService(A<IntPtr>._))
			        .Returns(false);

			var connectionHandle = new ConnectionHandle
				{
					ServiceManagerHandle = new IntPtr(serviceControlManagerHandleValue),
					ServiceHandle = new IntPtr(serviceHandleValue)
				};

			var sut = new ServiceConnection(advApi32.FakedObject);

			sut.Close(connectionHandle);

			var actual = connectionHandle.ServiceHandle;
			var expected = new IntPtr(serviceHandleValue);

			Assert.Equal(expected, actual);
		}

		[Theory, AutoFake]
		public void CloseServiceCallsUnderlyingApi(Fake<IAdvApi32> advApi32, int serviceControlManagerHandleValue, int serviceHandleValue)
		{
			advApi32.CallsTo(_ => _.CloseService(A<IntPtr>._))
			        .Returns(true);

			var connectionHandle = new ConnectionHandle
				{
					ServiceManagerHandle = new IntPtr(serviceControlManagerHandleValue),
					ServiceHandle = new IntPtr(serviceHandleValue)
				};

			var sut = new ServiceConnection(advApi32.FakedObject);

			sut.Close(connectionHandle);

			advApi32.CallsTo(_ => _.CloseService(A<IntPtr>._)).MustHaveHappened();
		}

		[Theory, AutoFake]
		public void CloseServiceSetsConnectionHandleToIntPtr(Fake<IAdvApi32> advApi32, int serviceControlManagerHandleValue,
		                                                     int serviceHandleValue)
		{
			advApi32.CallsTo(_ => _.CloseService(A<IntPtr>._))
			        .Returns(true);

			var connectionHandle = new ConnectionHandle
				{
					ServiceManagerHandle = new IntPtr(serviceControlManagerHandleValue),
					ServiceHandle = new IntPtr(serviceHandleValue)
				};

			var sut = new ServiceConnection(advApi32.FakedObject);

			sut.Close(connectionHandle);

			var actual = connectionHandle.ServiceHandle;

			var expected = IntPtr.Zero;

			Assert.Equal(expected, actual);
		}
	}
}