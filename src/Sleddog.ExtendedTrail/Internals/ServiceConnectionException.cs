using System;

namespace Sleddog.ExtendedTrail.Internals
{
	public class ServiceConnectionException : Exception
	{
		public ServiceConnectionException(string message) : base(message)
		{
		}
	}
}