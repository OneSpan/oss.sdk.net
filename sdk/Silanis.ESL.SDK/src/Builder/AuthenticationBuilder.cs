using System;

namespace Silanis.ESL.SDK.Builder
{
	public class AuthenticationBuilder
	{
		public AuthenticationBuilder() {}

		public virtual Authentication Build()
		{
			return new Authentication(AuthenticationMethod.EMAIL);
		}
	}
}