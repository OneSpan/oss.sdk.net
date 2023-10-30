namespace OneSpanSign.Sdk
{
    internal class AccountDesignerSettingsConverter
    {
        private OneSpanSign.Sdk.AccountDesignerSettings sdkAccountDesignerSettings;
        private OneSpanSign.API.AccountDesignerSettings apiAccountDesignerSettings;
        
        public AccountDesignerSettingsConverter(OneSpanSign.API.AccountPackageSettings apiAccountPackageSettings)
        {
            this.apiAccountDesignerSettings = apiAccountDesignerSettings;
        }

        public AccountDesignerSettingsConverter(OneSpanSign.Sdk.AccountPackageSettings sdkAccountPackageSettings)
        {
            this.sdkAccountDesignerSettings = sdkAccountDesignerSettings;
        }
        
        public OneSpanSign.API.AccountDesignerSettings ToAPIAccountDesignerSettings()
		{
			if (sdkAccountDesignerSettings == null)
			{
				return apiAccountDesignerSettings;
			}

			OneSpanSign.API.AccountDesignerSettings result = new OneSpanSign.API.AccountDesignerSettings();
			result.Send = sdkAccountDesignerSettings.Send;
			result.Done = sdkAccountDesignerSettings.Done;
			result.Settings = sdkAccountDesignerSettings.Settings;
			result.DocumentVisibility = sdkAccountDesignerSettings.DocumentVisibility;
			result.AddDocument = sdkAccountDesignerSettings.AddDocument;
			result.EditDocument = sdkAccountDesignerSettings.EditDocument;
			result.DeleteDocument = sdkAccountDesignerSettings.DeleteDocument;
			result.AddSigner = sdkAccountDesignerSettings.AddSigner;
			result.EditRecipient = sdkAccountDesignerSettings.EditRecipient;
			result.RolePickerSender = sdkAccountDesignerSettings.RolePickerSender;
			result.SaveLayout = sdkAccountDesignerSettings.SaveLayout;
			result.ApplyLayout = sdkAccountDesignerSettings.ApplyLayout;
			result.ShowSharedLayouts = sdkAccountDesignerSettings.ShowSharedLayouts;
			result.DefaultSignatureType = sdkAccountDesignerSettings.DefaultSignatureType;
			
            return result;
		}

		public OneSpanSign.Sdk.AccountDesignerSettings ToSDKAccountDesignerSettings()
		{
			if (apiAccountDesignerSettings == null)
			{
				return sdkAccountDesignerSettings;
			}

			OneSpanSign.Sdk.AccountDesignerSettings result = new OneSpanSign.Sdk.AccountDesignerSettings();
			result.Send = apiAccountDesignerSettings.Send;
			result.Done = apiAccountDesignerSettings.Done;
			result.Settings = apiAccountDesignerSettings.Settings;
			result.DocumentVisibility = apiAccountDesignerSettings.DocumentVisibility;
			result.AddDocument = apiAccountDesignerSettings.AddDocument;
			result.EditDocument = apiAccountDesignerSettings.EditDocument;
			result.DeleteDocument = apiAccountDesignerSettings.DeleteDocument;
			result.AddSigner = apiAccountDesignerSettings.AddSigner;
			result.EditRecipient = apiAccountDesignerSettings.EditRecipient;
			result.RolePickerSender = apiAccountDesignerSettings.RolePickerSender;
			result.SaveLayout = apiAccountDesignerSettings.SaveLayout;
			result.ApplyLayout = apiAccountDesignerSettings.ApplyLayout;
			result.ShowSharedLayouts = apiAccountDesignerSettings.ShowSharedLayouts;
			result.DefaultSignatureType = apiAccountDesignerSettings.DefaultSignatureType;

			return result;
		}
		
    }
}