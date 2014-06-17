using System;
using Silanis.ESL.SDK.Builder;

namespace Silanis.ESL.SDK
{
	internal class DocumentPackageConverter
    {
		private Silanis.ESL.API.Package apiPackage = null;
		private Silanis.ESL.SDK.DocumentPackage sdkPackage = null;

		/// <summary>
		/// Construct with API object involved in conversion.
		/// </summary>
		/// <param name="apiPackage">API Package.</param>
		public DocumentPackageConverter(Silanis.ESL.API.Package apiPackage)
        {
			this.apiPackage = apiPackage;
        }

		/// <summary>
		/// Construct with SDK object involved in conversion.
		/// </summary>
		/// <param name="sdkPackage">SDK DocumentPackage.</param>
		public DocumentPackageConverter(Silanis.ESL.SDK.DocumentPackage sdkPackage)
		{
			this.sdkPackage = sdkPackage;
		}

		internal Silanis.ESL.API.Package ToAPIPackage()
		{
			if (sdkPackage == null)
			{
				return apiPackage;
			}

			Silanis.ESL.API.Package package = new Silanis.ESL.API.Package();

			package.Name = sdkPackage.Name;
			package.Due = sdkPackage.ExpiryDate;
			package.Autocomplete = sdkPackage.Autocomplete;

			if (sdkPackage.Description != null)
			{
				package.Description = sdkPackage.Description;
			}

			if (sdkPackage.EmailMessage != null)
			{
				package.EmailMessage = sdkPackage.EmailMessage;
			}

			if (sdkPackage.Language != null)
			{
				package.Language = sdkPackage.Language.TwoLetterISOLanguageName;
			}

			if (sdkPackage.Settings != null)
			{
				package.Settings = sdkPackage.Settings.toAPIPackageSettings();
			}

			if (sdkPackage.SenderInfo != null)
			{
				package.Sender = new SenderConverter(sdkPackage.SenderInfo).ToAPISender();
			}

			if (sdkPackage.Attributes != null)
			{
				package.Data = sdkPackage.Attributes.Contents;
			}

			int signerCount = 1;
			foreach (Signer signer in sdkPackage.Signers.Values)
			{
				Silanis.ESL.API.Role role = new SignerConverter(signer).ToAPIRole("role" + signerCount);
				package.AddRole(role);
				signerCount++;
			}
			foreach (Signer signer in sdkPackage.Placeholders.Values)
			{
				Silanis.ESL.API.Role role = new SignerConverter(signer).ToAPIRole("role" + signerCount);
				package.AddRole(role);
				signerCount++;
			}

			return package;
		}
    }
}

