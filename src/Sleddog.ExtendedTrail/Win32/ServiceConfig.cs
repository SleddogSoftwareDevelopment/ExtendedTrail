namespace Sleddog.ExtendedTrail.Win32
{
	internal enum ServiceConfig : uint
	{
		ServiceConfigDescription = 1,
		ServiceConfigFailureActions = 2,
		ServiceConfigDelayedAutoStartInfo = 3,
		ServiceConfigFailureActionsFlag = 4,
		ServiceConfigServiceSidInfo = 5,
		ServiceConfigRequiredPrivilegesInfo = 6,
		ServiceConfigPreshutdownInfo = 7,
		ServiceConfigTriggerInfo = 8,
		ServiceConfigPreferredNode = 9
	}
}