using System;

namespace Sleddog.ExtendedTrail.Internals
{
	public class MissingServiceManagerConnectionException : Exception
	{
		public MissingServiceManagerConnectionException(string message) : base(message)
		{
		}
	}
}