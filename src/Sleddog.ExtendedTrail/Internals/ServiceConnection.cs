using System;
using Sleddog.ExtendedTrail.Win32;

namespace Sleddog.ExtendedTrail.Internals
{
	public class ServiceConnection
	{
		private readonly IAdvApi32 advApi32;

		public ServiceConnection(IAdvApi32 advApi32)
		{
			this.advApi32 = advApi32;
		}

		public void Open(ConnectionHandle connectionHandle, string serviceName)
		{
			if (connectionHandle == null)
			{
				throw new ArgumentNullException("connectionHandle");
			}

			if (serviceName == null)
			{
				throw new ArgumentNullException("serviceName");
			}

			if (!connectionHandle.ServiceManagerIsOpen)
			{
				throw new MissingServiceManagerConnectionException("Unable to open service connection without an open service manager connection");
			}

			var serviceHandle = advApi32.OpenService(connectionHandle.ServiceManagerHandle, serviceName, ScmAccess.ScManagerAllAccess);

			if (serviceHandle == IntPtr.Zero)
			{
				throw new ServiceConnectionException("Unable to open service connection");
			}

			connectionHandle.ServiceHandle = serviceHandle;
		}

		public void Close(ConnectionHandle connectionHandle)
		{
			if (connectionHandle == null)
			{
				throw new ArgumentNullException("connectionHandle");
			}

			if (!connectionHandle.ServiceManagerIsOpen)
			{
				throw new MissingServiceManagerConnectionException("Unable to close a service connection without an open service manager connection");
			}

			if (!connectionHandle.ServiceIsOpen)
			{
				throw new ArgumentException("Unable to close a service that is not open yet");
			}

			if (advApi32.CloseService(connectionHandle.ServiceHandle))
			{
				connectionHandle.ServiceHandle = IntPtr.Zero;
			}
		}
	}
}