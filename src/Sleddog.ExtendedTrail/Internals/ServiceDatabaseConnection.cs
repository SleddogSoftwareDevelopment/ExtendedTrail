using System;
using Sleddog.ExtendedTrail.Win32;

namespace Sleddog.ExtendedTrail.Internals
{
	public class ServiceDatabaseConnection
	{
		private IntPtr serviceDatabaseLockHandle = IntPtr.Zero;
		private IntPtr serviceHandle = IntPtr.Zero;
		private IntPtr serviceManagerHandle = IntPtr.Zero;

		public void Open()
		{
			serviceManagerHandle = advApi32.OpenSCManager(null, null, (uint) ScmAccess.ScManagerAllAccess);

			if(serviceManagerHandle.ToInt32()<=0)
			{
			}
		}
	}
}