using System;

namespace Sleddog.ExtendedTrail.Internals
{
	public class ConnectionHandle
	{
		public IntPtr ServiceDatabaseLockHandle { get; set; }
		public IntPtr ServiceManagerHandle { get; set; }
		public IntPtr ServiceHandle { get; set; }

		public bool ServiceManagerIsOpen
		{
			get { return ServiceManagerHandle != IntPtr.Zero; }
		}

		public bool ServiceDatabaseIsLocked
		{
			get { return ServiceDatabaseLockHandle != IntPtr.Zero; }
		}

		public bool ServiceIsOpen
		{
			get { return ServiceHandle != IntPtr.Zero; }
		}
	}
}