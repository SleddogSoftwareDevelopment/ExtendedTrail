using System;
using Sleddog.ExtendedTrail.Win32;

namespace Sleddog.ExtendedTrail.Internals
{
	public class ServiceDatabaseConnection
	{
		private readonly IAdvApi32 advApi32;

		public ServiceDatabaseConnection(IAdvApi32 advApi32)
		{
			this.advApi32 = advApi32;
		}

		public ConnectionHandle Open()
		{
			var serviceManagerHandle = advApi32.OpenServiceControlManager(null, null, ScmAccess.ScManagerAllAccess);

			return new ConnectionHandle {ServiceManagerHandle = serviceManagerHandle};
		}

		public void WriteLock(ConnectionHandle connectionHandle)
		{
			if (connectionHandle == null)
			{
				throw new ArgumentNullException("connectionHandle");
			}

			if (!connectionHandle.ServiceManagerIsOpen)
			{
				throw new ServiceConnectionException("Unable to lock service database without an open connection to the service manager");
			}

			var serviceDatabaseLockHandle = advApi32.AquireServiceDatabaseLock(connectionHandle.ServiceManagerHandle);

			connectionHandle.ServiceDatabaseLockHandle = serviceDatabaseLockHandle;
		}

		public void ReleaseLock(ConnectionHandle connectionHandle)
		{
			if (connectionHandle == null)
			{
				throw new ArgumentNullException("connectionHandle");
			}

			if (!connectionHandle.ServiceManagerIsOpen)
			{
				throw new ServiceConnectionException("Unable to unlock service database without an open connection to the service manager");
			}

			if (!connectionHandle.ServiceDatabaseIsLocked)
			{
				throw new ServiceConnectionException("Unable to unlock service database when it is not locked");
			}

			if (advApi32.ReleaseServiceDatabaseLock(connectionHandle.ServiceDatabaseLockHandle))
			{
				connectionHandle.ServiceDatabaseLockHandle = IntPtr.Zero;
			}
		}

		public void Close(ConnectionHandle connectionHandle)
		{
			if (connectionHandle == null)
			{
				throw new ArgumentNullException("connectionHandle");
			}

			if (!connectionHandle.ServiceManagerIsOpen)
			{
				throw new ServiceConnectionException("Unable to close service manager connection when it is not open");
			}

			if (advApi32.CloseServiceControlManager(connectionHandle.ServiceManagerHandle))
			{
				connectionHandle.ServiceManagerHandle = IntPtr.Zero;
			}
		}
	}
}