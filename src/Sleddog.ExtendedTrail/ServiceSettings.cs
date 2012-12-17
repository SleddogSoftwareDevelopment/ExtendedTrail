namespace Sleddog.ExtendedTrail
{
	public class ServiceSettings
	{
		public ServiceSettings(string serviceName, string displayName) : this(serviceName, displayName, string.Empty)
		{
		}

		public ServiceSettings(string serviceName, string displayName, string description)
		{
			DisplayName = displayName;
			ServiceName = serviceName;
			Description = description;
		}

		public string DisplayName { get; set; }
		public string ServiceName { get; set; }
		public string Description { get; set; }
	}
}