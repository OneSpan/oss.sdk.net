using System;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using System.Globalization;

namespace Silanis.ESL.SDK.Internal
{
    class Log4NetLogger : ILog 
    {
        private log4net.ILog log4netLogger;

        private Log4NetLogger(log4net.ILog logger) {
            log4netLogger = logger;
        }

        /// <summary>
        /// Returns a new log4net logger if it exists, or returns null if the assembly cannot be found.
        /// </summary>
        internal static ILog Initialize() {
            return isLog4NetPresent ? CreateLogger() : null;
        }

        static bool isLog4NetPresent {
            get {
                try {
                    Assembly.Load("log4net");
                    return true;
                } catch (FileNotFoundException) {
                    return false;
                }
            }
        }

        /// 
        /// Creates the log4net.LogManager.  Call ONLY once log4net.dll is known to be present.
        /// 

        static ILog CreateLogger() {
            return new Log4NetLogger(log4net.LogManager.GetLogger("Log4NetLogger"));
        }

        public void Debug(object message) {
            log4netLogger.Debug(message);
        }

        public void DebugFormat(string format, params object[] args) {
            log4netLogger.DebugFormat(CultureInfo.InvariantCulture, format, args);
        }

        public bool IsDebugEnabled {
            get { return log4netLogger.IsDebugEnabled; }
        }

        public void Error(object message) {
            log4netLogger.Error(message);
        }

        public void ErrorFormat(string format, params object[] args) {
            log4netLogger.ErrorFormat(CultureInfo.InvariantCulture, format, args);
        }

        public bool IsErrorEnabled {
            get { return log4netLogger.IsErrorEnabled; }
        }

        public void Fatal(object message) {
            log4netLogger.Fatal(message);
        }

        public void FatalFormat(string format, params object[] args) {
            log4netLogger.FatalFormat(CultureInfo.InvariantCulture, format, args);
        }

        public bool IsFatalEnabled {
            get { return log4netLogger.IsFatalEnabled; }
        }

        public void Info(object message) {
            log4netLogger.Info(message);
        }

        public void InfoFormat(string format, params object[] args) {
            log4netLogger.InfoFormat(CultureInfo.InvariantCulture, format, args);
        }

        public bool IsInfoEnabled {
            get { return log4netLogger.IsInfoEnabled; }
        }

        public void Warn(object message) {
            log4netLogger.Warn(message);
        }

        public void WarnFormat(string format, params object[] args) {
            log4netLogger.WarnFormat(CultureInfo.InvariantCulture, format, args);
        }

        public bool IsWarnEnabled {
            get { return log4netLogger.IsWarnEnabled; }
        }


    }
}