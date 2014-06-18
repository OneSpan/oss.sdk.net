using System;
using Silanis.ESL.SDK.Internal;
using Silanis.ESL.API;
using Newtonsoft.Json;

namespace Silanis.ESL.SDK.Services
{
	/// <summary>
	/// This class is used for registering to the e-SL notification system.
	/// </summary>
	public class EventNotificationService
	{
		private RestClient restClient;
		private UrlTemplate template;
		private JsonSerializerSettings settings;

		public EventNotificationService(RestClient restClient, string apiUrl, JsonSerializerSettings settings)
		{
			this.restClient = restClient;
			template = new UrlTemplate(apiUrl);
			this.settings = settings;
		}

		/// <summary>
		/// Registers to receive notifications sent by e-SL that are described by the config parameter passed to this method.
		/// </summary>
		/// <param name="config">Describes the event notification of interest.</param>
		public void Register(EventNotificationConfig config)
		{
			string path = template.UrlFor(UrlTemplate.CALLBACK_PATH).Build();
			Callback callback = new EventNotificationConfigConverter(config).ToAPICallback();
			string json = JsonConvert.SerializeObject(callback, settings);

			restClient.Post(path, json);
		}

		/// <summary>
		/// <p>Registers to receive notifications sent by e-SL.<p>
		/// <p>The builder parameter of this method is convenient to use when you want to easily add more notification events.</p>
		/// </summary>
		/// <param name="builder">The event notification config builder.</param>
		public void Register(EventNotificationConfigBuilder builder)
		{
			Register(builder.build());
		}

		/// <summary>
		/// Gets the registered event notifications.
		/// </summary>
		/// <returns>Description of registered event notifications.</returns>
		public EventNotificationConfig GetEventNotificationConfig()
		{
			string path = template.UrlFor(UrlTemplate.CALLBACK_PATH).Build();

			try
			{
				string stringResponse = restClient.Get(path);
				Callback apiResponse = JsonConvert.DeserializeObject<Callback>(stringResponse, settings);
				return new EventNotificationConfigConverter(apiResponse).ToSDKEventNotificationConfig();
			}
			catch (EslServerException e)
			{
				throw new EslServerException("Could not retrieve event notification. " + e.Message, e.ServerError, e);
			}
			catch (Exception e)
			{
				throw new EslException("Could not retrieve event notification. " + e.Message, e);
			}
		}
	}
}
