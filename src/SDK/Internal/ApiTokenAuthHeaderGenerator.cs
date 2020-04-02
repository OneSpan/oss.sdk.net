using System;
using System.Collections;
using System.Collections.Generic;

namespace OneSpanSign.Sdk
{
	public class ApiTokenAuthHeaderGenerator : AuthHeaderGenerator
    {
		public ApiTokenAuthHeaderGenerator(string apiToken) : base("Authorization", "Basic " + apiToken)
        {
        }
    }
}

