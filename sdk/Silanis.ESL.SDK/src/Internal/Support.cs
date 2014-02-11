using System;
using log4net;
using Silanis.ESL.API;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using System.Reflection;
using Newtonsoft.Json;
using System.IO;

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
			// TODO: Hack for Santander!
			File.AppendAllText("c:\\tmp\\sdk.log.txt", message + "\n");
			log.Debug(message);
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void LogMethodEntry(params object[] values)
		{
			MethodBase methodBase = GetCallingMethod();
			Support.LogDebug("--->" + methodBase.DeclaringType.Name + ": " + methodBase.ToString());
			for (int paramCtr = 0; paramCtr < values.Length; paramCtr++)
			{
				ParameterInfo paramInfo = methodBase.GetParameters()[paramCtr];
				object param = values[paramCtr];
				string json = JsonConvert.SerializeObject (param);
				Support.LogDebug("\t" + paramInfo.ParameterType.ToString() + " " + paramInfo.Name + ": " + json);
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void LogMethodExit(params object[] values)
		{
			foreach ( object value in values ) {
				Support.LogDebug("Returning: " + JsonConvert.SerializeObject(value));
			}
			MethodBase methodBase = GetCallingMethod();
			Support.LogDebug("<---" + methodBase.DeclaringType.Name + ": " + methodBase.Name);
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		public static MethodBase GetCallingMethod() {
			StackTrace st = new StackTrace();
			StackFrame sf = st.GetFrame(2);

			return sf.GetMethod();
		}
    }
}

