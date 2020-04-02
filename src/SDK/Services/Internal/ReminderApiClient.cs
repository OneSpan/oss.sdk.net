using System;
using OneSpanSign.Sdk.Internal;
using Newtonsoft.Json;
using OneSpanSign.API;

namespace OneSpanSign.Sdk
{
    internal class ReminderApiClient
    {
        private UrlTemplate template;
        private JsonSerializerSettings settings;
        private RestClient restClient;

        public ReminderApiClient(RestClient restClient, string baseUrl, JsonSerializerSettings settings)
        {
            this.restClient = restClient;
            template = new UrlTemplate (baseUrl);
            this.settings = settings;
        }
        
        private string Path( string packageId )
        {
            return template.UrlFor (UrlTemplate.REMINDER_PATH)
                .Replace( "{packageId}", packageId )
                .Build ();
        }
        
        public PackageReminderSchedule GetReminderScheduleForPackage( string packageId )
        {
            try {
                string response = restClient.Get(Path(packageId));
                if (response.Length == 0) {
                    return null;
                }
                PackageReminderSchedule apiResponse = JsonConvert.DeserializeObject<PackageReminderSchedule> (response, settings );
                return apiResponse;
            } 
            catch (OssServerException e) {
                throw new OssServerException ("Failed to retrieve reminder schedule for package with id: " + packageId + ". Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e) {
                throw new OssException ("Failed to retrieve reminder schedule for package with id: " + packageId + ". Exception: " + e.Message, e);
            }
        }

        [Obsolete("Please use CreateReminderScheduleForPackage(ReminderSchedule) instead")]  
        public PackageReminderSchedule SetReminderScheduleForPackage( PackageReminderSchedule apiPayload )
        {
            return CreateReminderScheduleForPackage(apiPayload);
        }

        public PackageReminderSchedule CreateReminderScheduleForPackage( PackageReminderSchedule apiPayload )
        {
            try {
                string response = restClient.Post(Path(apiPayload.PackageId), JsonConvert.SerializeObject (apiPayload, settings));
                PackageReminderSchedule apiResponse = JsonConvert.DeserializeObject<PackageReminderSchedule> (response, settings );
                return apiResponse;
            }
            catch (OssServerException e) {
                throw new OssServerException ("Failed to create reminder schedule for package with id: " + apiPayload.PackageId + ". Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e) {
                throw new OssException ("Failed to create reminder schedule for package with id: " + apiPayload.PackageId + ". Exception: " + e.Message, e);
            }
        }

        public PackageReminderSchedule UpdateReminderScheduleForPackage( PackageReminderSchedule apiPayload )
        {
            try {
                string response = restClient.Put(Path(apiPayload.PackageId), JsonConvert.SerializeObject (apiPayload, settings));
                PackageReminderSchedule apiResponse = JsonConvert.DeserializeObject<PackageReminderSchedule> (response, settings );
                return apiResponse;
            }
            catch (OssServerException e) {
                throw new OssServerException ("Failed to update reminder schedule for package with id: " + apiPayload.PackageId + ". Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e) {
                throw new OssException ("Failed to update reminder schedule for package with id: " + apiPayload.PackageId + ". Exception: " + e.Message, e);
            }
        }

        public void ClearReminderScheduleForPackage( string packageId )
        {
            try {
                restClient.Delete(Path(packageId));
            } 
            catch (OssServerException e) {
                throw new OssServerException ("Failed to remove reminder schedule for package with id: " + packageId + ". Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e) {
                throw new OssException ("Failed to remove reminder schedule for package with id: " + packageId + ". Exception: " + e.Message, e);
            }
        }
    }
}

