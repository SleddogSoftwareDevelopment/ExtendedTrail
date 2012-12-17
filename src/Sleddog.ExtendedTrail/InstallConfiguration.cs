using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.ServiceProcess;
using Sleddog.ExtendedTrail.RecoveryOptions;

namespace Sleddog.ExtendedTrail
{
	public class InstallConfiguration : IDisposable
	{
		private readonly RecoveryOption[] recoveryOptions;
		private SecureString securePassword;

		public InstallConfiguration()
		{
			recoveryOptions = Enumerable.Range(0, 3).Select(_ => new NOOPOption()).ToArray();
		}

		public bool StartOnInstall { get; set; }

		public StartupType StartupType { get; set; }

		public ServiceAccount ServiceAccount { get; set; }

		public string UserName { get; set; }

		public string Password
		{
			get { return RetrievePassword(); }
			set { StorePassword(value); }
		}

		public TimeSpan FailCountResetTime { get; set; }

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

		public void Dispose()
		{
			if (securePassword != null)
			{
				securePassword.Dispose();
			}
		}

		private void StorePassword(string password)
		{
			unsafe
			{
				fixed (char* passwordPointer = password)
				{
					securePassword = new SecureString(passwordPointer, password.Length);
					securePassword.MakeReadOnly();
				}
			}
		}

		private string RetrievePassword()
		{
			var unmanagedPassword = IntPtr.Zero;

			try
			{
				unmanagedPassword = Marshal.SecureStringToGlobalAllocUnicode(securePassword);

				return Marshal.PtrToStringUni(unmanagedPassword);
			}
			finally
			{
				Marshal.ZeroFreeGlobalAllocUnicode(unmanagedPassword);
			}
		}
	}
}