using System;

namespace Sleddog.ExtendedTrail.RecoveryOptions
{
	public class RestartServiceOption : RecoveryOption
	{
		public TimeSpan RestartDelay { get; set; }
	}
}