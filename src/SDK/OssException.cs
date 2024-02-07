using System;

namespace OneSpanSign.Sdk
{
	/// <summary>
	/// OneSpan Sign exception.
	/// </summary>
	public class OssException : Exception
	{
		public OssException (string message, Exception cause) : base(message, cause)
		{
            String s = message;
		}
	}
}

