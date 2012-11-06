using System;
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
				if (value == null)
				{
					throw new ArgumentNullException("FirstFailure");
				}

				recoveryOptions[0] = value;
			}
		}

		public RecoveryOption SecondFailure
		{
			get { return recoveryOptions[1]; }
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("SecondFailure");
				}

				recoveryOptions[1] = value;
			}
		}

		public RecoveryOption SubsequentFailure
		{
			get { return recoveryOptions[2]; }
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("SubsequentFailure");
				}

				recoveryOptions[2] = value;
			}
		}

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