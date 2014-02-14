using System;

namespace Silanis.ESL.SDK.Builder
{
	public class AuthenticationBuilder
	{
		public AuthenticationBuilder() {}

		public virtual Authentication Build()
		{
			Support.LogMethodEntry();
			Authentication result = new Authentication(AuthenticationMethod.EMAIL);
			Support.LogMethodExit(result);
			return result;
		}
	}
}