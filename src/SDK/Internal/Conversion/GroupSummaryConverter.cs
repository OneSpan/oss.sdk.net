using System;

namespace OneSpanSign.Sdk
{
    internal class GroupSummaryConverter
    {
        private OneSpanSign.API.GroupSummary apiGroupSummary;
        private GroupSummary sdkGroupSummary;

        public GroupSummaryConverter( GroupSummary sdkGroupSummary ) {
            this.sdkGroupSummary = sdkGroupSummary;
            this.apiGroupSummary = null;
        }

        public GroupSummaryConverter( OneSpanSign.API.GroupSummary apiGroupSummary ) {
            this.apiGroupSummary = apiGroupSummary;
            this.sdkGroupSummary = null;
        }
        
        public OneSpanSign.API.GroupSummary ToAPIGroupSummary()
        {
            if (sdkGroupSummary == null)
            {
                return apiGroupSummary;
            }
            OneSpanSign.API.GroupSummary result = new OneSpanSign.API.GroupSummary();

            result.Data = sdkGroupSummary.Data;
            result.Email = sdkGroupSummary.Email;
            result.Id = sdkGroupSummary.Id;
            result.Name = sdkGroupSummary.Name;

            return result;
        }

        public GroupSummary ToSDKGroupSummary()
        {
            if (apiGroupSummary == null)
            {
                return sdkGroupSummary;
            }

            return GroupSummaryBuilder.NewGroupSummary(apiGroupSummary.Email)
                    .WithId(apiGroupSummary.Id)
                    .WithName(apiGroupSummary.Name)
                    .WithData(apiGroupSummary.Data)
                    .Build();
        }
    }
}

