using System;

namespace Sleddog.ExtendedTrail.Win32
{
	[Flags]
	public enum ScmAccess : uint
	{
		/// <summary>
		/// Required to connect to the service control manager.
		/// </summary>
		ScManagerConnect = 0x00001,

		/// <summary>
		/// Required to call the CreateService function to create a service
		/// object and add it to the database.
		/// </summary>
		ScManagerCreateService = 0x00002,

		/// <summary>
		/// Required to call the EnumServicesStatusEx function to list the
		/// services that are in the database.
		/// </summary>
		ScManagerEnumerateService = 0x00004,

		/// <summary>
		/// Required to call the LockServiceDatabase function to acquire a
		/// lock on the database.
		/// </summary>
		ScManagerLock = 0x00008,

		/// <summary>
		/// Required to call the QueryServiceLockStatus function to retrieve
		/// the lock status information for the database.
		/// </summary>
		ScManagerQueryLockStatus = 0x00010,

		/// <summary>
		/// Required to call the NotifyBootConfigStatus function.
		/// </summary>
		ScManagerModifyBootConfig = 0x00020,

		/// <summary>
		/// Includes STANDARD_RIGHTS_REQUIRED, in addition to all access
		/// rights in this table.
		/// </summary>
		ScManagerAllAccess = AccessMask.StandardRightsRequired |
		                        ScManagerConnect |
		                        ScManagerCreateService |
		                        ScManagerEnumerateService |
		                        ScManagerLock |
		                        ScManagerQueryLockStatus |
		                        ScManagerModifyBootConfig,

		GenericRead = AccessMask.StandardRightsRead |
		               ScManagerEnumerateService |
		               ScManagerQueryLockStatus,

		GenericWrite = AccessMask.StandardRightsWrite |
		                ScManagerCreateService |
		                ScManagerModifyBootConfig,

		GenericExecute = AccessMask.StandardRightsExecute |
		                  ScManagerConnect | ScManagerLock,

		GenericAll = ScManagerAllAccess,
	}
}