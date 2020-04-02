using System;
using System.Collections;
using System.Collections.Generic;

namespace OneSpanSign.Sdk
{
	public class SessionIdAuthHeaderGenerator : AuthHeaderGenerator
    {
		public SessionIdAuthHeaderGenerator(string sessionId) : base("Cookie", "ESIGNLIVE_SESSION_ID" + "=" + sessionId)
        {
        }
    }
}

