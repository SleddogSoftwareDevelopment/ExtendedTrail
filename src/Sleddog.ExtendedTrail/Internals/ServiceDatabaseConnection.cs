using System;
using Sleddog.ExtendedTrail.Win32;

namespace Sleddog.ExtendedTrail.Internals
{
	public class ServiceDatabaseConnection : IDisposable
	{
		private readonly IAdvApi32 advApi32;
		private IntPtr serviceDatabaseLockHandle = IntPtr.Zero;
		private IntPtr serviceManagerHandle = IntPtr.Zero;

		public ServiceDatabaseConnection(IAdvApi32 advApi32)
		{
			this.advApi32 = advApi32;
		}

		public bool IsOpen
		{
			get { return serviceManagerHandle != IntPtr.Zero; }
		}

		public bool HasWriteLock
		{
			get { return serviceDatabaseLockHandle != IntPtr.Zero; }
		}

		public void Dispose()
		{
			Close();
		}

		public void Open()
		{
			serviceManagerHandle = advApi32.OpenServiceControlManager(null, null, ScmAccess.ScManagerAllAccess);

			if (serviceManagerHandle.ToInt32() <= 0)
			{
				throw new ServiceDatabaseConnectionException("Unable to open Service Control Manager");
			}
		}

		public void WriteLock()
		{
			serviceDatabaseLockHandle = advApi32.AquireServiceDatabaseLock(serviceManagerHandle);

			if (serviceDatabaseLockHandle.ToInt32() <= 0)
			{
				throw new ServiceDatabaseConnectionException("Unable to get write lock to the service database");
			}
		}

		public void ReleaseLock()
		{
			if (serviceDatabaseLockHandle != IntPtr.Zero)
			{
				if (advApi32.ReleaseServiceDatabaseLock(serviceDatabaseLockHandle))
				{
					serviceDatabaseLockHandle = IntPtr.Zero;
				}
			}
		}

		public void Close()
		{
			if (serviceManagerHandle != IntPtr.Zero)
			{
				if (advApi32.CloseServiceControlManager(serviceManagerHandle))
				{
					serviceManagerHandle = IntPtr.Zero;
				}
			}
		}
	}
}