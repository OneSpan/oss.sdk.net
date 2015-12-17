using System;
using System.Globalization;

namespace Silanis.ESL.SDK.Internal
{
    internal static class Logger {
        internal static ILog facade = initializeFacade();

        internal static ILog initializeFacade() {
            ILog result = TraceLogger.Initialize() ?? NoOpLogger.Initialize();
            return result;
        }

        public static void Debug(object message) {
            facade.Debug(message);
        }

        public static void DebugFormat(string format, params object[] args) {
            facade.DebugFormat(format, args);
        }

        public static bool IsDebugEnabled {
            get { return facade.IsDebugEnabled; }
        }

        public static void Error(object message) {
            facade.Error(message);
        }

        public static void ErrorFormat(string format, params object[] args) {
            facade.ErrorFormat(format, args);
        }

        public static bool IsErrorEnabled {
            get { return facade.IsErrorEnabled; }
        }

        public static void Fatal(object message) {
            facade.Fatal(message);
        }

        public static void FatalFormat(string format, params object[] args) {
            facade.FatalFormat(format, args);
        }

        public static bool IsFatalEnabled {
            get { return facade.IsFatalEnabled; }
        }

        public static void Info(object message) {
            facade.Info(message);
        }

        public static void InfoFormat(string format, params object[] args) {
            facade.InfoFormat(format, args);
        }

        public static bool IsInfoEnabled {
            get { return facade.IsInfoEnabled; }
        }

        public static void Warn(object message) {
            facade.Warn(message);
        }

        public static void WarnFormat(string format, params object[] args) {
            facade.WarnFormat(format, args);
        }

        public static bool IsWarnEnabled {
            get { return facade.IsWarnEnabled; }
        }
    }
}

