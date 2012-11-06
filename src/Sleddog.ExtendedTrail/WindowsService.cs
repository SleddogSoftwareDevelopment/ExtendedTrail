using System;
using System.Linq;
using System.ServiceProcess;

namespace Sleddog.ExtendedTrail
{
	public class WindowsServiceInstaller : ServiceInstaller
	{
		private readonly RecoveryOption[] recoveryOptions = new RecoveryOption[3];

		public TimeSpan FailCountResetTime { get; set; }

		public RecoveryOption FirstFailure { get; set; }
		public RecoveryOption SecondFailure { get; set; }
		public RecoveryOption SubsequentFailure { get; set; }

		private bool RequiresShutdownPrivileges()
		{
			return recoveryOptions.Any(ro => ro.Action == RecoverAction.Reboot);
		}

		private bool AnyRecoveryOptions()
		{
			return recoveryOptions.Any(ro => ro.Action != RecoverAction.None);
		}
	}
}