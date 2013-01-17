using System;

namespace Sleddog.ExtendedTrail.Internals
{
	public class ConnectionHandle
	{
		public IntPtr ServiceDatabaseLockHandle { get; set; }
		public IntPtr ServiceManagerHandle { get; set; }
		public IntPtr ServiceHandle { get; set; }

		public bool IsServiceManagerOpen
		{
			get { return ServiceManagerHandle != IntPtr.Zero; }
		}

		public bool IsServiceDatabaseLocked
		{
			get { return ServiceDatabaseLockHandle != IntPtr.Zero; }
		}

		public bool IsServiceOpen
		{
			get { return ServiceHandle != IntPtr.Zero; }
		}
	}
}