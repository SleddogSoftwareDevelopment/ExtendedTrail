using System;
using System.Diagnostics.Contracts;
using System.Linq;
using System.ServiceProcess;

namespace Sleddog.ExtendedTrail
{
	public class WindowsServiceInstaller : ServiceInstaller
	{
		private readonly RecoveryOption[] recoveryOptions;

		public WindowsServiceInstaller()
		{
			recoveryOptions = Enumerable.Range(0, 3).Select(_ => new RecoveryOption()).ToArray();
		}

		public TimeSpan FailCountResetTime { get; set; }

		public RecoveryOption FirstFailure
		{
			get { return recoveryOptions[0]; }
			set
			{
				Contract.Requires(value != null);

				recoveryOptions[0] = value;
			}
		}

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