using System;
using Silanis.ESL.SDK.Internal;
using Newtonsoft.Json;
using Silanis.ESL.API;

namespace Silanis.ESL.SDK.Services
{
    public class ReminderService
    {
		private UrlTemplate template;
		private JsonSerializerSettings settings;
		private RestClient restClient;

		public ReminderService(RestClient restClient, string baseUrl, JsonSerializerSettings settings)
		{
			this.restClient = restClient;
			template = new UrlTemplate (baseUrl);

            this.settings = settings;
		}

		private string Path( PackageId packageId )
		{
			return template.UrlFor (UrlTemplate.REMINDER_PATH)
				.Replace( "{packageId}", packageId.Id )
				.Build ();
		}

		public ReminderSchedule GetReminderScheduleForPackage( PackageId packageId )
		{
			try {
				string response = restClient.Get(Path(packageId));
				if (response.Length == 0) {
					return null;
				}
				PackageReminderSchedule apiResponse = JsonConvert.DeserializeObject<PackageReminderSchedule> (response, settings );
				return new ReminderScheduleConverter( apiResponse ).ToSDKReminderSchedule();
			} 
            catch (EslServerException e) {
                throw new EslServerException ("Failed to retrieve reminder schedule for package with id: " + packageId.Id + ". Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e) {
				throw new EslException ("Failed to retrieve reminder schedule for package with id: " + packageId.Id + ". Exception: " + e.Message, e);
			}
		}

		public ReminderSchedule SetReminderScheduleForPackage( ReminderSchedule reminderSchedule )
		{
			try {
				PackageReminderSchedule apiPayload = new ReminderScheduleConverter(reminderSchedule).ToAPIPackageReminderSchedule();
				string payload = JsonConvert.SerializeObject (apiPayload, settings);
				string response = restClient.Post(Path(reminderSchedule.PackageId), payload);
				PackageReminderSchedule apiResponse = JsonConvert.DeserializeObject<PackageReminderSchedule> (response, settings );
				return new ReminderScheduleConverter( apiResponse ).ToSDKReminderSchedule();
			}
            catch (EslServerException e) {
                throw new EslServerException ("Failed to set reminder schedule for package with id: " + reminderSchedule.PackageId.Id + ". Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e) {
				throw new EslException ("Failed to set reminder schedule for package with id: " + reminderSchedule.PackageId.Id + ". Exception: " + e.Message, e);
			}
		}

		public void ClearReminderScheduleForPackage( PackageId packageId )
		{
			try {
				restClient.Delete(Path(packageId));
			} 
            catch (EslServerException e) {
                throw new EslServerException ("Failed to remove reminder schedule for package with id: " + packageId.Id + ". Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e) {
				throw new EslException ("Failed to remove reminder schedule for package with id: " + packageId.Id + ". Exception: " + e.Message, e);
			}
		}
    }
}

