using System;
using System.Collections.Generic;
using Silanis.ESL.SDK;

namespace SDK.Examples
{
    public class GetGroupSummariesExample : SDKSample
    {
        public List<GroupSummary> retrievedGroupSummaries;
        public static void Main(string[] args)
        {
            new GetGroupSummariesExample(Props.GetInstance()).Run();
        }

        public GetGroupSummariesExample(Props props) : this(props.Get("api.url"), props.Get("api.key"))
        {
        }

        public GetGroupSummariesExample(string apiKey, string apiUrl) : base(apiKey, apiUrl)
        {
        }

        override public void Execute()
        {
            retrievedGroupSummaries = eslClient.GroupService.GetGroupSummaries();

            foreach(GroupSummary groupSummary in retrievedGroupSummaries) {
                Console.WriteLine ("GroupSummary id : {0}, email : {1}, name : {2}", groupSummary.Id, groupSummary.Email, groupSummary.Name);
            }
            Console.WriteLine ("Total : {0}" + retrievedGroupSummaries.Count);
        }
    }
}

