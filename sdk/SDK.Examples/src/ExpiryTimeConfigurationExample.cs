using System;
using Silanis.ESL.SDK.Builder;
using Silanis.ESL.SDK;

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

            eslClient.DataRetentionSettingsService.SetExpiryTimeConfiguration (expiryTimeConfiguration);
            expiryTimeConfigurationAfterUpdate = eslClient.DataRetentionSettingsService.GetExpiryTimeConfiguration ();
        }
    }
}
