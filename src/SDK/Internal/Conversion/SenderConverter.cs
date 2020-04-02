using System;

namespace OneSpanSign.Sdk
{
	internal class SenderConverter
    {
		private OneSpanSign.API.Sender apiSender;
		private OneSpanSign.Sdk.SenderInfo sdkSenderInfo;

		public SenderConverter(OneSpanSign.API.Sender sender)
		{
			if (sender == null) 
				throw new ArgumentNullException("sender");
			this.apiSender = sender;
			this.sdkSenderInfo = null;
		}

		public SenderConverter(OneSpanSign.Sdk.SenderInfo senderInfo)
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
                        .WithTimezoneId(apiSender.TimezoneId)
						.Build();
			}
		}

		internal OneSpanSign.API.Sender ToAPISender()
		{
			if (apiSender != null)
			{
				return apiSender;
			}
			else
			{
				OneSpanSign.API.Sender result = new OneSpanSign.API.Sender();
				result.Email = sdkSenderInfo.Email;

				if (sdkSenderInfo.FirstName != null)
					result.FirstName = sdkSenderInfo.FirstName;
				if (sdkSenderInfo.LastName != null)
					result.LastName = sdkSenderInfo.LastName;
				if (sdkSenderInfo.Company != null)
					result.Company = sdkSenderInfo.Company;
				if (sdkSenderInfo.Title != null)
					result.Title = sdkSenderInfo.Title;
                if (sdkSenderInfo.TimezoneId != null)
                    result.TimezoneId = sdkSenderInfo.TimezoneId;

				return result;
			}
		}

		public Sender ToSDKSender() {
			if (apiSender == null)
			{
				return null;
			}

			Sender result = new Sender();
			result.Email = apiSender.Email;
			result.Id = apiSender.Id;
			result.FirstName = apiSender.FirstName;
			result.LastName = apiSender.LastName;
			result.Company = apiSender.Company;
			result.Created = apiSender.Created;
			result.Language = apiSender.Language;
			result.Name = apiSender.Name;
			result.Phone = apiSender.Phone;
			result.Status = new SenderStatusConverter(apiSender.Status).ToSDKSenderStatus();
			result.Type = new SenderTypeConverter(apiSender.Type).ToSDKSenderType();
			result.Title = apiSender.Title;
			result.Updated = apiSender.Updated;
            result.TimezoneId = apiSender.TimezoneId;
            result.External = new ExternalConverter(apiSender.External).ToSDKExternal();
			
			return result;
		}
    }
}

