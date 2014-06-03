using System;
using NUnit.Framework;
using Silanis.ESL.SDK;

namespace SDK.Tests
{
	[TestFixture]
	public class RequirementStatusConverterTest
    {
		private Silanis.ESL.SDK.RequirementStatus sdkRequirementStatus1;
		private Silanis.ESL.API.RequirementStatus apiRequirementStatus1;

		[Test]
		public void ConvertAPIToSDK()
		{
			apiRequirementStatus1 = CreateTypicalAPIRequirementStatus();
			sdkRequirementStatus1 = new RequirementStatusConverter(apiRequirementStatus1).ToSDKRequirementStatus();

			Assert.AreEqual(sdkRequirementStatus1.ToString(), apiRequirementStatus1.ToString());
		}

		[Test]
		public void ConvertSDKToAPI()
		{
			sdkRequirementStatus1 = CreateTypicalSDKRequirementStatus();
			apiRequirementStatus1 = new RequirementStatusConverter(sdkRequirementStatus1).ToAPIRequirementStatus();

			Assert.AreEqual(apiRequirementStatus1.ToString(), sdkRequirementStatus1.ToString());
		}

		private Silanis.ESL.SDK.RequirementStatus CreateTypicalSDKRequirementStatus()
		{
			return Silanis.ESL.SDK.RequirementStatus.COMPLETE;
		}

		private Silanis.ESL.API.RequirementStatus CreateTypicalAPIRequirementStatus()
		{
			return Silanis.ESL.API.RequirementStatus.INCOMPLETE;
		}
    }
}

