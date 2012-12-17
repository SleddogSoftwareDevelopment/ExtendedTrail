using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;

namespace Sleddog.ExtendedTrail
{
	public class ServiceValidator
	{
		public IEnumerable<ServiceValidationResult> ValidateServices(string[] services)
		{
			var installedServices = ServiceController.GetServices();

			var validationResults = (from s in services
			                         let exists = installedServices.Any(ins => ServiceExists(ins, s))
			                         select new ServiceValidationResult(s, exists));

			return validationResults;
		}

		private bool ServiceExists(ServiceController serviceController, string name)
		{
			var compareCandidates = new[] {serviceController.ServiceName, serviceController.DisplayName};

			return compareCandidates.Contains(name, StringComparer.OrdinalIgnoreCase);
		}
	}
}