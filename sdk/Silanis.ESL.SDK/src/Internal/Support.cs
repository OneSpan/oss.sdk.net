using System;
using log4net;
using Silanis.ESL.API;

namespace Silanis.ESL.SDK
{
    public class Support
    {
		private static ILog log = LogManager.GetLogger(typeof(Support));

        internal void LogRequest(string httpVerb, string path, string jsonPayload) {
            log.Debug(httpVerb + " on " + path);
            log.Debug("payload: " + jsonPayload);
        }

        internal void LogRequest(string httpVerb, string path) {
            log.Debug(httpVerb + " on " + path);
        }

        internal void LogResponse(string response) {
            log.Debug("response: " + response);
        }

        internal void LogError(Error error) {
            log.Error("message: " + error.Message + ", http code: " + error.Code);
        }

		internal static void LogError( string message ) {
			log.Error(message);
		}

		internal static void LogDebug(string message) {
			log.Debug(message);
		}
    }
}

