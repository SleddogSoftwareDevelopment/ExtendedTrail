namespace Sleddog.ExtendedTrail.RecoveryOptions
{
	public class RunCommandOption : RecoveryOption
	{
		public string CommandPath { get; set; }
		public string CommandLineParameters { get; set; }
		public bool AppendFailCount { get; set; }
	}
}