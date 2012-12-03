using System;
using FakeItEasy;
using Sleddog.ExtendedTrail.Internals;
using Sleddog.ExtendedTrail.Win32;
using Xunit;
using Xunit.Extensions;

namespace Sleddog.ExtendedTrail.Tests
{
	public class ServiceDatabaseConnectionTests
	{
		[Theory, AutoFake]
		public void OpenServiceManagerGetsCorrectConnectionHandle(Fake<IAdvApi32> advApi32, long pointerValue)
		{
			advApi32.CallsTo(_ => _.OpenServiceControlManager(A<string>._, A<string>._, A<ScmAccess>._))
			        .Returns(new IntPtr(pointerValue));

			var sut = new ServiceDatabaseConnection(advApi32.FakedObject);

			var connectionHandle = sut.Open();

			var actual = connectionHandle.ServiceManagerHandle;
			var expected = new IntPtr(pointerValue);

			Assert.Equal(expected, actual);
		}

		[Theory, AutoFake]
		public void CloseServiceManagerResetsConnectionHandleToIntPtrZero(Fake<IAdvApi32> advApi32, int serviceManagerHandleValue)
		{
			advApi32.CallsTo(_ => _.CloseServiceControlManager(A<IntPtr>._))
			        .Returns(true);

			var sut = new ServiceDatabaseConnection(advApi32.FakedObject);

			var connectionHandle = new ConnectionHandle {ServiceManagerHandle = new IntPtr(serviceManagerHandleValue)};

			sut.Close(connectionHandle);

			var actual = connectionHandle.ServiceManagerHandle;
			var expected = IntPtr.Zero;

			Assert.Equal(expected, actual);
		}

		[Theory, AutoFake]
		public void AquireWriteLockRequiresConnectionHandle(Fake<IAdvApi32> advApi32)
		{
			var sut = new ServiceDatabaseConnection(advApi32.FakedObject);

			Assert.Throws<ArgumentNullException>(() => sut.WriteLock(null));
		}

		[Theory, AutoFake]
		public void WriteLockSetsServiceDatabaseLockHandle(Fake<IAdvApi32> advApi32, int serviceControlManagerHandlerValue,
		                                                   int serviceDatabaseLockHandleValue)
		{
			advApi32.CallsTo(_ => _.AquireServiceDatabaseLock(A<IntPtr>._))
			        .Returns(new IntPtr(serviceDatabaseLockHandleValue));

			var sut = new ServiceDatabaseConnection(advApi32.FakedObject);

			var connectionHandle = new ConnectionHandle {ServiceManagerHandle = new IntPtr(serviceControlManagerHandlerValue)};

			sut.WriteLock(connectionHandle);

			var actual = connectionHandle.ServiceDatabaseLockHandle;
			var expected = new IntPtr(serviceDatabaseLockHandleValue);

			Assert.Equal(expected, actual);
		}

		[Theory, AutoFake]
		public void WriteLockCallsUnderlyingAPI(Fake<IAdvApi32> advApi32, int serviceControlManagerHandlerValue,
		                                        int serviceDatabaseLockHandleValue)
		{
			advApi32.CallsTo(_ => _.AquireServiceDatabaseLock(A<IntPtr>._))
			        .Returns(new IntPtr(serviceDatabaseLockHandleValue));

			var sut = new ServiceDatabaseConnection(advApi32.FakedObject);

			var connectionHandle = new ConnectionHandle {ServiceManagerHandle = new IntPtr(serviceControlManagerHandlerValue)};

			sut.WriteLock(connectionHandle);

			advApi32.CallsTo(_ => _.AquireServiceDatabaseLock(A<IntPtr>._)).MustHaveHappened();
		}

		[Theory, AutoFake]
		public void ReleaseWriteLockResetsDatabaseLockHandle(Fake<IAdvApi32> advApi32, int serviceControlManagerHandlerValue,
		                                                     int serviceDatabaseLockHandleValue)
		{
			advApi32.CallsTo(_ => _.ReleaseServiceDatabaseLock(A<IntPtr>._))
			        .Returns(true);

			var sut = new ServiceDatabaseConnection(advApi32.FakedObject);

			var connectionHandle = new ConnectionHandle
				{
					ServiceManagerHandle = new IntPtr(serviceControlManagerHandlerValue),
					ServiceDatabaseLockHandle = new IntPtr(serviceDatabaseLockHandleValue)
				};

			sut.ReleaseLock(connectionHandle);

			var actual = connectionHandle.ServiceDatabaseLockHandle;
			var expected = IntPtr.Zero;

			Assert.Equal(expected, actual);
		}

		[Theory, AutoFake]
		public void ReleaseWriteLockCallsUnderlyingAPI(Fake<IAdvApi32> advApi32, int serviceControlManagerHandlerValue,
		                                               int serviceDatabaseLockHandleValue)
		{
			var sut = new ServiceDatabaseConnection(advApi32.FakedObject);

			var connectionHandle = new ConnectionHandle
				{
					ServiceManagerHandle = new IntPtr(serviceControlManagerHandlerValue),
					ServiceDatabaseLockHandle = new IntPtr(serviceDatabaseLockHandleValue)
				};

			sut.ReleaseLock(connectionHandle);

			advApi32.CallsTo(_ => _.ReleaseServiceDatabaseLock(A<IntPtr>._)).MustHaveHappened();
		}
	}
}