using System;

namespace Sleddog.ExtendedTrail.Internals
{
	public class ServiceDatabaseConnectionException : Exception
	{
		public ServiceDatabaseConnectionException(string message) : base(message)
		{
		}
	}
}