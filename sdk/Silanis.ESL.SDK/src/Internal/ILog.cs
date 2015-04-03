using System;
using System.Collections.Generic;
using System.Reflection;

namespace Silanis.ESL.SDK.Internal
{
	interface ILog
	{
        bool IsDebugEnabled
        {
            get;
        }
        bool IsErrorEnabled
        {
            get;
        }
        bool IsFatalEnabled
        {
            get;
        }
        bool IsInfoEnabled
        {
            get;
        }
        bool IsWarnEnabled
        {
            get;
        }

        void Debug(object message);
        void DebugFormat(string format, params object[] args);
        void Error(object message);
        void ErrorFormat(string format, params object[] args);
        void Fatal(object message);
        void FatalFormat(string format, params object[] args);
        void Info(object message);
        void InfoFormat(string format, params object[] args);
        void Warn(object message);
        void WarnFormat(string format, params object[] args);
	}
}