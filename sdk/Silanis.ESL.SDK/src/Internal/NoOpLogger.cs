using System;
using Silanis.ESL.SDK.Internal;

namespace Silanis.ESL.SDK
{
    internal class NoOpLogger : ILog
    {
        
        /// <summary>
        /// Returns a new logger that does nothing when invoked.
        /// </summary>
        internal static ILog Initialize() {
            return new NoOpLogger();
        }

        public void Debug(object message) {
            return;
        }

        public void DebugFormat(string format, params object[] args) {
            return;
        }

        public bool IsDebugEnabled {
            get { return false; }
        }

        public void Error(object message) {
            return;
        }

        public void ErrorFormat(string format, params object[] args) {
            return;
        }

        public bool IsErrorEnabled {
            get { return false; }
        }

        public void Fatal(object message) {
            return;
        }

        public void FatalFormat(string format, params object[] args) {
            return;
        }

        public bool IsFatalEnabled {
            get { return false; }
        }

        public void Info(object message) {
            return;
        }

        public void InfoFormat(string format, params object[] args) {
            return;
        }

        public bool IsInfoEnabled {
            get { return false; }
        }

        public void Warn(object message) {
            return;
        }

        public void WarnFormat(string format, params object[] args) {
            return;
        }

        public bool IsWarnEnabled {
            get { return false; }
        }
    }
}

