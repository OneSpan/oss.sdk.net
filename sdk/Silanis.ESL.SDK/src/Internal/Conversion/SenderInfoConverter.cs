using System;

namespace Silanis.ESL.SDK
{
	internal class SenderInfoConverter
    {
		private Silanis.ESL.API.Signer signer;
		private Silanis.ESL.API.Sender sender;

		public SenderInfoConverter(Silanis.ESL.API.Sender sender)
		{
			this.sender = sender;
		}

		public SenderInfoConverter(Silanis.ESL.API.Signer signer)
        {
			this.signer = signer;
        }

		public SenderInfo ToSDKSenderInfo() {
			if (signer != null)
			{
				return SenderInfoBuilder.NewSenderInfo(signer.Email)
				.WithName(signer.FirstName, signer.LastName)
				.WithCompany(signer.Company)
				.WithTitle(signer.Title)
				.Build();
			}
			else
			{
				return SenderInfoBuilder.NewSenderInfo(sender.Email)
						.WithName(sender.FirstName, sender.LastName)
						.WithCompany(sender.Company)
						.WithTitle(sender.Title)
						.Build();
			}
		}
    }
}

