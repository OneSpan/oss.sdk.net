using System.Collections.Generic;
using OneSpanSign.Sdk.Services;

namespace SDK.Examples
{
    public class DeclineReasonsExample : SDKSample
    {
        public static void Main(string [] args)
        {
            new DeclineReasonsExample ().Run();
        }
        
        public IList<string> CreatedDeclineReasons, RetrievedDeclineReasons, UpdatedDeclineReasons, DeclineReasonsAfterDelete;
        public readonly IList<string> DeclineReasonsCreateList = new List<string>
        {"Decline Reason 1", "Decline Reason 2"};

        public readonly IList<string> DeclineReasonsUpateList = new List<string>
            {"Decline Reason 1", "Decline Reason 3"};

        public override void Execute()
        {
            AccountConfigService accountConfigService = ossClient.AccountConfigService;
            // create decline reasons
            CreatedDeclineReasons = accountConfigService.CreateDeclineReasons(DeclineReasonsCreateList, "en");

            // get decline reasons
            RetrievedDeclineReasons = accountConfigService.GetDeclineReasons("en");
            
            // update decline reasons
            UpdatedDeclineReasons =
                accountConfigService.UpdateDeclineReasons(DeclineReasonsUpateList, "en");
            
            // delete decline reasons
            accountConfigService.DeleteDeclineReasons("en");

            DeclineReasonsAfterDelete = accountConfigService.GetDeclineReasons("en");
        }
    }
}