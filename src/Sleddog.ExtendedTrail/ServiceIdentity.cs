using System;
using System.Runtime.InteropServices;
using System.Security;
using System.ServiceProcess;

namespace Sleddog.ExtendedTrail
{
	public class ServiceIdentity : IDisposable
	{
		private SecureString securePassword;

		public ServiceIdentity(string username, string password)
		{
			if (username == null)
			{
				throw new ArgumentNullException("username");
			}

			if (password == null)
			{
				throw new ArgumentNullException("password");
			}

			UserName = username;
			StorePassword(password);

			ServiceAccount = ServiceAccount.User;
		}

		public ServiceIdentity(ServiceAccount serviceAccount)
		{
			if (serviceAccount == ServiceAccount.User)
			{
				throw new ArgumentOutOfRangeException("serviceAccount", "ServiceAccount can't be user without supplying username and password");
			}

			ServiceAccount = serviceAccount;
		}

		public ServiceAccount ServiceAccount { get; set; }

		public string UserName { get; set; }

		public string Password
		{
			get { return RetrievePassword(); }
			set { StorePassword(value); }
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