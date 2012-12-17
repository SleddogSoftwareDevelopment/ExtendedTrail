using System.ServiceProcess;
using System.Linq;

namespace Sleddog.ExtendedTrail
{
	public class ExtendedTrailInstaller
	{
		private readonly ServiceValidator serviceValidator;

		public ExtendedTrailInstaller(ServiceValidator serviceValidator)
		{
			this.serviceValidator = serviceValidator;
		}

		public bool Install(ServiceConfiguration configuration)
		{
			if (configuration.Dependencies != null)
			{
				var validationResults = serviceValidator.ValidateServices(configuration.Dependencies);

				var invalidCount = validationResults.Count(vr => !vr.Exists);

				if (invalidCount > 0)
				{
					//Do something about missing dependencies
				}
			}

			var serviceSettings = configuration.ServiceSettings;

			var installer = new ServiceInstaller
				{
					Description = serviceSettings.Description,
					ServicesDependedOn = configuration.Dependencies,
					DisplayName = serviceSettings.DisplayName,
					ServiceName = serviceSettings.ServiceName,
				};

			SetupServiceStartup(installer, configuration.StartupType);

			return true;
		}

		private void SetupServiceStartup(ServiceInstaller serviceInstaller, StartupType startupType)
		{
			switch (startupType)
			{
				case StartupType.Disabled:
					serviceInstaller.StartType = ServiceStartMode.Disabled;
					break;
				case StartupType.Manual:
					serviceInstaller.StartType = ServiceStartMode.Manual;
					break;
				case StartupType.Automatic:
					serviceInstaller.StartType = ServiceStartMode.Automatic;
					break;
				case StartupType.AutomaticDelayedStart:
					serviceInstaller.DelayedAutoStart = true;
					goto case StartupType.Automatic;
			}
		}

		//private bool RequiresShutdownPrivileges()
		//{
		//	return recoveryOptions.Any(ro => ro.GetType() == typeof (RestartComputerOption));
		//}

		//private bool AnyRecoveryOptions()
		//{
		//	return recoveryOptions.Any(ro => ro.GetType() != typeof (NOOPOption));
		//}
	}
}