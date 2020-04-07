using OneSpanSign.API;
using OneSpanSign.Sdk.Internal.Conversion;

namespace OneSpanSign.Sdk.Services
{
    public class AccountConfigService
    {
        private AccountConfigClient apiClient;

        internal AccountConfigService(AccountConfigClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public Handover GetHandoverUrl(string language)
        {
            Link link = apiClient.GetHandoverUrl(language);
            return new HandoverConverter(link).ToSDKHandover(language);
        }

        public Handover CreateHandoverUrl(Handover handover)
        {
            Link link = new HandoverConverter(handover).ToAPILink();
            link = apiClient.CreateHandoverUrl(handover.Language, link);
            return new HandoverConverter(link).ToSDKHandover(handover.Language);
        }

        public Handover UpdateHandoverUrl(Handover handover)
        {
            Link link = new HandoverConverter(handover).ToAPILink();
            link = apiClient.UpdateHandoverUrl(handover.Language, link);
            return new HandoverConverter(link).ToSDKHandover(handover.Language);
        }

        public void DeleteHandoverUrl(string language)
        {
            apiClient.DeleteHandoverUrl(language);
        }
    }
}