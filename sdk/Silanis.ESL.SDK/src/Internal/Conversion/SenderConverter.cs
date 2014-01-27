using System;

namespace Silanis.ESL.SDK
{
	internal class SenderConverter
    {
		private Silanis.ESL.API.Sender apiSender;
		private Silanis.ESL.SDK.SenderInfo sdkSenderInfo;

		public SenderConverter(Silanis.ESL.API.Sender sender)
		{
			if (sender == null) 
				throw new ArgumentNullException("sender");
			this.apiSender = sender;
			this.sdkSenderInfo = null;
		}

		public SenderConverter(Silanis.ESL.SDK.SenderInfo senderInfo)
		{
			if (senderInfo == null) 
				throw new ArgumentNullException("senderInfo");
			this.apiSender = null;
			this.sdkSenderInfo = senderInfo;
		}

		public SenderInfo ToSDKSenderInfo() {
			if (sdkSenderInfo != null)
			{
				return sdkSenderInfo;
			}
			else
			{
				return SenderInfoBuilder.NewSenderInfo(apiSender.Email)
						.WithName(apiSender.FirstName, apiSender.LastName)
						.WithCompany(apiSender.Company)
						.WithTitle(apiSender.Title)
						.Build();
			}
		}

		internal Silanis.ESL.API.Sender ToAPISender()
		{
			if (apiSender != null)
			{
				return apiSender;
			}
			else
			{
				Silanis.ESL.API.Sender result = new Silanis.ESL.API.Sender();
				result.Email = sdkSenderInfo.Email;

				if (sdkSenderInfo.FirstName != null)
					result.FirstName = sdkSenderInfo.FirstName;
				if (sdkSenderInfo.LastName != null)
					result.LastName = sdkSenderInfo.LastName;
				if (sdkSenderInfo.Company != null)
					result.Company = sdkSenderInfo.Company;
				if (sdkSenderInfo.Title != null)
					result.Title = sdkSenderInfo.Title;

				return result;
			}
		}
    }
}

