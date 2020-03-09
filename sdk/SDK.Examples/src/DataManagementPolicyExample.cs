using System;
using Silanis.ESL.SDK.Builder;
using Silanis.ESL.SDK;
namespace SDK.Examples
{

    public class DataManagementPolicyExample : SDKSample
    {

        public DataManagementPolicy dataManagementPolicyAfterUpdate;

        public static void Main (string [] args)
        {
            new DataManagementPolicyExample ().Run ();
        }

        override public void Execute ()
        {

            DataManagementPolicy dataManagementPolicy = DataManagementPolicyBuilder.NewDataManagementPolicy ()
                    .WithTransactionRetention (TransactionRetentionBuilder.NewTransactionRetention ()
                            .WithArchived (60)
                            .WithCompleted (60)
                            .WithDeclined (60)
                            .WithDraft (60)
                            .WithExpired (60)
                            .WithOptedOut (60)
                            .WithSent (0)
                            .Build ())
                    .Build ();

            eslClient.DataRetentionSettingsService.SetDataManagementPolicy (dataManagementPolicy);
            dataManagementPolicyAfterUpdate = eslClient.DataRetentionSettingsService.GetDataManagementPolicy ();
        }
    }
}
