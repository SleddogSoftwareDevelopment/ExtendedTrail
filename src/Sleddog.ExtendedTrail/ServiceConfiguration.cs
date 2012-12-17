using System;
using System.Linq;
using Sleddog.ExtendedTrail.RecoveryOptions;

namespace Sleddog.ExtendedTrail
{
	public class ServiceConfiguration
	{
		private readonly RecoveryOption[] recoveryOptions;

		public ServiceConfiguration()
		{
			recoveryOptions = Enumerable.Range(0, 3).Select(_ => new NOOPOption()).ToArray();
		}

		public ServiceSettings ServiceSettings { get; set; }

		public bool StartOnInstall { get; set; }

		public StartupType StartupType { get; set; }

		public TimeSpan FailCountResetTime { get; set; }

		public ServiceIdentity ServiceAccount { get; set; }

		public string[] Dependencies { get; set; }

		public RecoveryOption FirstFailure
		{
			get { return recoveryOptions[0]; }
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("FirstFailure");
				}

				recoveryOptions[0] = value;
			}
		}

		public RecoveryOption SecondFailure
		{
			get { return recoveryOptions[1]; }
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("SecondFailure");
				}

				recoveryOptions[1] = value;
			}
		}

		public RecoveryOption SubsequentFailure
		{
			get { return recoveryOptions[2]; }
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("SubsequentFailure");
				}

				recoveryOptions[2] = value;
			}
		}
	}
}