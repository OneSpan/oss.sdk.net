using System;
using Newtonsoft.Json;

namespace OneSpanSign.Sdk
{
    public class AuthenticationToken
    {
		public string Token { get; private set; }

        public AuthenticationToken(string token)
        {
            this.Token = token;
        }

		public override string ToString ()
		{
			return Token;
		}
    }
}