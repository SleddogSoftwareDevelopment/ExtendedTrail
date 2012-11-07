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
		public void CanOpenServiceManager(Fake<IAdvApi32> advApi32, long pointerValue)
		{
			advApi32.CallsTo(_ => _.OpenServiceControlManager(A<string>._, A<string>._, A<ScmAccess>._))
				.Returns(new IntPtr(pointerValue));

			var sut = new ServiceDatabaseConnection(advApi32.FakedObject);

			sut.Open();

			advApi32.CallsTo(_ => _.OpenServiceControlManager(A<string>._, A<string>._, A<ScmAccess>._)).MustHaveHappened();
		}

		[Theory, AutoFake]
		public void CanCloseServiceManager(Fake<IAdvApi32> advApi32, int pointerValue)
		{
			advApi32.CallsTo(_ => _.OpenServiceControlManager(A<string>._, A<string>._, A<ScmAccess>._))
				.Returns(new IntPtr(pointerValue));

			advApi32.CallsTo(_ => _.CloseServiceControlManager(A<IntPtr>._))
				.Returns(true);

			var sut = new ServiceDatabaseConnection(advApi32.FakedObject);

			sut.Open();

			sut.Close();

			advApi32.CallsTo(_ => _.CloseServiceControlManager(A<IntPtr>._)).MustHaveHappened();
		}

		[Theory, AutoFake]
		public void CanNotAquireWriteLockWithoutAnOpenConnection(Fake<IAdvApi32> advApi32)
		{
			var sut = new ServiceDatabaseConnection(advApi32.FakedObject);

			Assert.Throws<ServiceDatabaseConnectionException>(() => sut.WriteLock());
		}

		[Theory, AutoFake]
		public void CanAquireWriteLock(Fake<IAdvApi32> advApi32, int serviceControlManagerHandlerValue, int serviceDatabaseLockHandleValue)
		{
			advApi32.CallsTo(_ => _.OpenServiceControlManager(A<string>._, A<string>._, A<ScmAccess>._))
				.Returns(new IntPtr(serviceControlManagerHandlerValue));

			advApi32.CallsTo(_ => _.AquireServiceDatabaseLock(A<IntPtr>._))
				.Returns(new IntPtr(serviceDatabaseLockHandleValue));

			var sut = new ServiceDatabaseConnection(advApi32.FakedObject);

			sut.Open();

			sut.WriteLock();

			advApi32.CallsTo(_ => _.AquireServiceDatabaseLock(A<IntPtr>._)).MustHaveHappened();
		}

		[Theory, AutoFake]
		public void CanReleaseWriteLock(Fake<IAdvApi32> advApi32, int serviceControlManagerHandlerValue, int serviceDatabaseLockHandleValue)
		{
			advApi32.CallsTo(_ => _.OpenServiceControlManager(A<string>._, A<string>._, A<ScmAccess>._))
				.Returns(new IntPtr(serviceControlManagerHandlerValue));

			advApi32.CallsTo(_ => _.AquireServiceDatabaseLock(A<IntPtr>._))
				.Returns(new IntPtr(serviceDatabaseLockHandleValue));

			var sut = new ServiceDatabaseConnection(advApi32.FakedObject);

			sut.Open();

			sut.WriteLock();

			sut.ReleaseLock();

			advApi32.CallsTo(_ => _.ReleaseServiceDatabaseLock(A<IntPtr>._)).MustHaveHappened();
		}
	}
}