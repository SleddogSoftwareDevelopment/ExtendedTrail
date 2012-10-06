using System;
using System.ServiceProcess;
using System.Linq;

namespace Sleddog.ExtendedTrail
{
	public class WindowsServiceInstaller : ServiceInstaller
	{
		public WindowsServiceInstaller()
		{
			RecoveryOptions = new RecoveryOption[3];
		}

		public TimeSpan FailCountResetTime { get; set; }
		public RecoveryOption[] RecoveryOptions { get; private set; }

		private bool RequiresShutdownPrivileges()
		{
			return RecoveryOptions.Any(ro => ro.Action == RecoverAction.Reboot);
		}
	}
}