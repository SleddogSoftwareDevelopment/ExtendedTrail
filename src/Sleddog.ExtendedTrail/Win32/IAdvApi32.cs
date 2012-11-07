using System;

namespace Sleddog.ExtendedTrail.Win32
{
	public interface IAdvApi32
	{
		IntPtr OpenServiceControlManager(string machineName, string databaseName, ScmAccess serviceControlManagerAccess);
		bool CloseServiceControlManager(IntPtr serviceControlManagerHandle);
		IntPtr OpenService(IntPtr serviceControlManagerHandle, string serviceName, ScmAccess serviceControlManagerAccess);
		IntPtr AquireServiceDatabaseLock(IntPtr serviceControlManagerHandle);
		bool ReleaseServiceDatabaseLock(IntPtr serviceControlManagerHandler);
	}
}