using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Sleddog.ExtendedTrail.Win32
{
	internal class AdvApi32 : IAdvApi32
	{
		public IntPtr OpenServiceControlManager(string machineName, string databaseName, ScmAccess serviceControlManagerAccess)
		{
			var handle = OpenSCManagerW(machineName, databaseName, (uint) serviceControlManagerAccess);

			if (handle == IntPtr.Zero)
			{
				throw new Win32Exception(Marshal.GetLastWin32Error());
			}

			return handle;
		}

		public bool CloseServiceControlManager(IntPtr serviceControlManagerHandle)
		{
			return CloseServiceHandle(serviceControlManagerHandle);
		}

		public bool CloseService(IntPtr serviceHandle)
		{
			return CloseServiceHandle(serviceHandle);
		}

		public IntPtr OpenService(IntPtr serviceControlManagerHandle, string serviceName, ScmAccess serviceControlManagerAccess)
		{
			var handle = OpenService(serviceControlManagerHandle, serviceName, (uint) serviceControlManagerAccess);

			if (handle == IntPtr.Zero)
			{
				throw new Win32Exception(Marshal.GetLastWin32Error());
			}

			return handle;
		}

		public IntPtr AquireServiceDatabaseLock(IntPtr serviceControlManagerHandle)
		{
			var handle = LockServiceDatabase(serviceControlManagerHandle);

			if (handle == IntPtr.Zero)
			{
				throw new Win32Exception(Marshal.GetLastWin32Error());
			}

			return handle;
		}

		public bool ReleaseServiceDatabaseLock(IntPtr serviceControlManagerHandler)
		{
			return UnlockServiceDatabase(serviceControlManagerHandler);
		}

		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern IntPtr OpenSCManagerW(string machineName, string databaseName, uint dwAccess);

		[DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Auto)]
		private static extern IntPtr OpenService(IntPtr hSCManager, string lpServiceName, uint dwDesiredAccess);

		[DllImport("advapi32.dll", SetLastError = true)]
		private static extern IntPtr LockServiceDatabase(IntPtr hSCManager);

		[DllImport("advapi32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool UnlockServiceDatabase(IntPtr hSCManager);

		[DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Auto)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool ChangeServiceConfig2(IntPtr hService, int dwInfoLevel, IntPtr lpInfo);

		[DllImport("advapi32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool CloseServiceHandle(IntPtr hSCObject);
	}
}