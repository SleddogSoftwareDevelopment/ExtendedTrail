namespace Sleddog.ExtendedTrail
{
	public class ServiceValidationResult
	{
		public ServiceValidationResult(string serviceName, bool exists)
		{
			Exists = exists;
			ServiceName = serviceName;
		}

		public bool Exists { get; private set; }
		public string ServiceName { get; private set; }
	}
}