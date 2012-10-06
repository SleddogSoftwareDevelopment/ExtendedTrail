using System;

namespace Sleddog.ExtendedTrail
{
	public class RecoveryOption
	{
		public RecoverAction Action { get; set; }
		public TimeSpan RestartDelay { get; set; }
	}
}