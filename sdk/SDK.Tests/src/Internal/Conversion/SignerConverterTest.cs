using NUnit.Framework;
using System;
using Silanis.ESL.SDK;

namespace SDK.Tests
{
    [TestFixture()]
    public class SignerConverterTest
    {
		private Silanis.ESL.SDK.Signer sdkSigner1 = null;
		private Silanis.ESL.SDK.Signer sdkSigner2 = null;
		private Silanis.ESL.API.Signer apiSigner1 = null;
		private Silanis.ESL.API.Signer apiSigner2 = null;
		private Silanis.ESL.API.Role apiRole = null;
		private SignerConverter converter = null;

        [Test()]
		public void ConvertNullSDKToAPI()
        {
			sdkSigner1 = null;
			converter = new SignerConverter(sdkSigner1);
//			Assert.IsNull(converter.ToAPIRole("roleId"));
        }
    }
}

