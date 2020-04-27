using System;
using OneSpanSign.Sdk.Builder;
using OneSpanSign.Sdk;

namespace SDK.Examples
{
    public class ExpiryTimeConfigurationExample : SDKSample
    {

        public ExpiryTimeConfiguration expiryTimeConfigurationAfterUpdate;

        public static void Main (string [] args)
        {
            new ExpiryTimeConfigurationExample ().Run ();
        }

        override public void Execute ()
        {

            ExpiryTimeConfiguration expiryTimeConfiguration = ExpiryTimeConfigurationBuilder.NewExpiryTimeConfiguration ()
                    .WithMaximumRemainingDays (60)
                    .WithRemainingDays (60)
                    .Build ();

            OssClient.DataRetentionSettingsService.SetExpiryTimeConfiguration (expiryTimeConfiguration);
            expiryTimeConfigurationAfterUpdate = OssClient.DataRetentionSettingsService.GetExpiryTimeConfiguration ();
        }
    }
}
