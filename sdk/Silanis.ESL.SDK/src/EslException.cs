using System;

namespace Silanis.ESL.SDK
{
	/// <summary>
	/// E-SignLive exception.
	/// </summary>
	public class EslException : Exception
	{
		public EslException (string message) : base(message)
		{
		}
	}

}

