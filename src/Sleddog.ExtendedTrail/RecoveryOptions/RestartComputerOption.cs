using System;

namespace Sleddog.ExtendedTrail.RecoveryOptions
{
	public class RestartComputerOption : RecoveryOption
	{
		public TimeSpan DelayRestart { get; set; }
	}
}