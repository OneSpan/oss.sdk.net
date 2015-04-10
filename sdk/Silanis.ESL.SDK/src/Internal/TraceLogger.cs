using System;
using System.Diagnostics;
using System.Security;
using System.Security.Permissions;

namespace Silanis.ESL.SDK.Internal
{
    internal class TraceLogger : ILog {
        TraceSwitch traceSwitch = new TraceSwitch("OpenID", "OpenID Trace Switch");

        /// 

        /// Returns a new logger that uses the  class 
        /// if sufficient CAS permissions are granted to use it, otherwise returns false.
        /// 

        internal static ILog Initialize() {
            return isSufficientPermissionGranted ? new TraceLogger() : null;
        }

        static bool isSufficientPermissionGranted {
            get {
                PermissionSet permissions = new PermissionSet(PermissionState.None);
                permissions.AddPermission(new KeyContainerPermission(PermissionState.Unrestricted));
                permissions.AddPermission(new ReflectionPermission(ReflectionPermissionFlag.MemberAccess));
                permissions.AddPermission(new RegistryPermission(PermissionState.Unrestricted));
                permissions.AddPermission(new SecurityPermission(SecurityPermissionFlag.ControlEvidence | SecurityPermissionFlag.UnmanagedCode | SecurityPermissionFlag.ControlThread));
                var file = new FileIOPermission(PermissionState.None);
                file.AllFiles = FileIOPermissionAccess.PathDiscovery | FileIOPermissionAccess.Read;
                permissions.AddPermission(file);
                try {
                    permissions.Demand();
                    return true;
                } catch (SecurityException) {
                    return false;
                }
            }
        }

        public void Debug(object message) {
            Trace.TraceInformation(message.ToString());
        }

        public void DebugFormat(string format, params object[] args) {
            Trace.TraceInformation(format, args);
        }

        public bool IsDebugEnabled {
            get { return traceSwitch.TraceVerbose; }
        }

        public void Error(object message) {
            Trace.TraceInformation(message.ToString());
        }

        public void ErrorFormat(string format, params object[] args) {
            Trace.TraceInformation(format, args);
        }

        public bool IsErrorEnabled {
            get { return traceSwitch.TraceVerbose; }
        }

        public void Fatal(object message) {
            Trace.TraceInformation(message.ToString());
        }

        public void FatalFormat(string format, params object[] args) {
            Trace.TraceInformation(format, args);
        }

        public bool IsFatalEnabled {
            get { return traceSwitch.TraceVerbose; }
        }

        public void Info(object message) {
            Trace.TraceInformation(message.ToString());
        }

        public void InfoFormat(string format, params object[] args) {
            Trace.TraceInformation(format, args);
        }

        public bool IsInfoEnabled {
            get { return traceSwitch.TraceVerbose; }
        }

        public void Warn(object message) {
            Trace.TraceInformation(message.ToString());
        }

        public void WarnFormat(string format, params object[] args) {
            Trace.TraceInformation(format, args);
        }

        public bool IsWarnEnabled {
            get { return traceSwitch.TraceVerbose; }
        }
    }
}

