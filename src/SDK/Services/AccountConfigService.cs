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
            OneSpanSign.API.Handover apiHandover = apiClient.GetHandoverUrl(language);
            return new HandoverConverter(apiHandover).ToSDKHandover(language);
        }

        public Handover CreateHandoverUrl(Handover handover)
        {
            OneSpanSign.API.Handover apiHandover = new HandoverConverter(handover).ToAPIHandover();
            apiHandover = apiClient.CreateHandoverUrl(handover.Language, apiHandover);
            return new HandoverConverter(apiHandover).ToSDKHandover(handover.Language);
        }

        public Handover UpdateHandoverUrl(Handover handover)
        {
            OneSpanSign.API.Handover apiHandover = new HandoverConverter(handover).ToAPIHandover();
            apiHandover = apiClient.UpdateHandoverUrl(handover.Language, apiHandover);
            return new HandoverConverter(apiHandover).ToSDKHandover(handover.Language);
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