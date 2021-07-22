using System;
using OneSpanSign.Sdk.Builder;
using OneSpanSign.Sdk;

namespace SDK.Examples
{
    public class ExpiryTimeConfigurationExample : SDKSample
    {
        public ExpiryTimeConfiguration expiryTimeConfigurationAfterUpdate;
        public static void Main(string[] args)
        {
            new ExpiryTimeConfigurationExample().Run();
        }
        override public void Execute()
        {
            ExpiryTimeConfiguration expiryTimeConfiguration = ExpiryTimeConfigurationBuilder.NewExpiryTimeConfiguration()
                    .WithMaximumRemainingDays(120)
                    .WithRemainingDays(60)
                    .Build();
            OssClient.DataRetentionSettingsService.SetExpiryTimeConfiguration(expiryTimeConfiguration);
            expiryTimeConfigurationAfterUpdate = OssClient.DataRetentionSettingsService.GetExpiryTimeConfiguration();
            ExpiryTimeConfiguration resetTimeConfiguration = ExpiryTimeConfigurationBuilder.NewExpiryTimeConfiguration()
                .WithMaximumRemainingDays(0)
                .WithRemainingDays(0)
                .Build();
            OssClient.DataRetentionSettingsService.SetExpiryTimeConfiguration(resetTimeConfiguration);
        }
    }
}
