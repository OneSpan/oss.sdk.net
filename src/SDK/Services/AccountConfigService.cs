using System.Collections.Generic;
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
        
        public IList<string> CreateDeclineReasons(IList<string> declineReasons, string language)
        {
           return apiClient.CreateDeclineReasons(declineReasons, language);
           
        }
        
        public IList<string> UpdateDeclineReasons(IList<string> declineReasons, string language)
        {
            return apiClient.UpdateDeclineReasons(declineReasons, language);
        }

        public IList<string> GetDeclineReasons(string language)
        {
            return apiClient.GetDeclineReasons(language);
        }
        
        public void DeleteDeclineReasons(string language)
        {
            apiClient.DeleteDeclineReasons(language);
        }
    }
}