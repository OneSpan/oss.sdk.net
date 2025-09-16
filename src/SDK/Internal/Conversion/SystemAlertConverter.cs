namespace OneSpanSign.Sdk
{
    internal class SystemAlertConverter
    {
        private SystemAlert sdkSystemAlert;
        private OneSpanSign.API.SystemAlert apiSystemAlert;

        public SystemAlertConverter (SystemAlert sdkSystemAlert)
        {
            this.sdkSystemAlert = sdkSystemAlert;
        }

        public SystemAlertConverter (OneSpanSign.API.SystemAlert apiSystemAlert)
        {
            this.apiSystemAlert = apiSystemAlert;
        }

        public OneSpanSign.API.SystemAlert ToAPISystemAlert ()
        {
            if (sdkSystemAlert == null) 
            {
                return apiSystemAlert;
            }

            OneSpanSign.API.SystemAlert result = new OneSpanSign.API.SystemAlert ();
            result.SeverityLevel = new SeverityLevelConverter(sdkSystemAlert.SeverityLevel).ToAPISeverityLevel();
            result.Code = sdkSystemAlert.Code;
            result.DefaultMessage = sdkSystemAlert.DefaultMessage;
            result.Parameters = sdkSystemAlert.Parameters;

            return result;
        }

        public SystemAlert ToSDKSystemAlert ()
        {
            if (apiSystemAlert == null) 
            {
                return sdkSystemAlert;
            }

            SystemAlert result = new SystemAlert ();

            result.SeverityLevel = new SeverityLevelConverter(apiSystemAlert.SeverityLevel).ToSDKSeverityLevel();
            result.Code = apiSystemAlert.Code;
            result.DefaultMessage = apiSystemAlert.DefaultMessage;
            result.Parameters = apiSystemAlert.Parameters;

            return result;
        }
    }
}
