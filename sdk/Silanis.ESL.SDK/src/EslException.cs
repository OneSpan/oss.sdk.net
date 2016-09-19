using System;

namespace Silanis.ESL.SDK
{
	/// <summary>
	/// eSignLive exception.
	/// </summary>
	public class EslException : Exception
	{
		public EslException (string message, Exception cause) : base(message, cause)
		{
            String s = message;
		}
	}
}

