using System;

namespace Sleddog.ExtendedTrail
{
	public class ServiceSettings
	{
		public ServiceSettings(string serviceName, string displayName) : this(serviceName, displayName, string.Empty)
		{
		}

		public ServiceSettings(string serviceName, string displayName, string description)
		{
			if (serviceName == null)
			{
				throw new ArgumentNullException("serviceName");
			}

			if (displayName == null)
			{
				throw new ArgumentNullException("displayName");
			}

			if (description == null)
			{
				throw new ArgumentNullException("description");
			}

			DisplayName = displayName;
			ServiceName = serviceName;
			Description = description;
		}

		public string DisplayName { get; set; }
		public string ServiceName { get; set; }
		public string Description { get; set; }
	}
}