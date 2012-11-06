using System;
using Sleddog.ExtendedTrail.Win32;

namespace Sleddog.ExtendedTrail.Internals
{
	public class ServiceDatabaseConnection : IDisposable
	{
		private IntPtr serviceDatabaseLockHandle = IntPtr.Zero;
		private IntPtr serviceManagerHandle = IntPtr.Zero;

		public void Dispose()
		{
			Close();
		}

		public void Open()
		{
			serviceManagerHandle = advApi32.OpenSCManager(null, null, (uint) ScmAccess.ScManagerAllAccess);

			if (serviceManagerHandle.ToInt32() <= 0)
			{
				throw new ServiceDatabaseConnectionException("Unable to open Service Control Manager");
			}
		}

		public void WriteLock()
		{
			serviceDatabaseLockHandle = advApi32.LockServiceDatabase(serviceManagerHandle);

			if (serviceDatabaseLockHandle.ToInt32() <= 0)
			{
				throw new ServiceDatabaseConnectionException("Unabled to get write lock to the service database");
			}
		}

		public void ReleaseLock()
		{
			if (serviceDatabaseLockHandle != IntPtr.Zero)
			{
				advApi32.UnlockServiceDatabase(serviceDatabaseLockHandle);

				serviceDatabaseLockHandle = IntPtr.Zero;
			}
		}

		public void Close()
		{
			if (serviceManagerHandle != IntPtr.Zero)
			{
				ReleaseLock();

				advApi32.CloseServiceHandle(serviceManagerHandle);

				serviceManagerHandle = IntPtr.Zero;
			}
		}
	}
}