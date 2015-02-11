using System;
using System.Collections.Generic;
using System.Reflection;
using log4net;

namespace Silanis.ESL.SDK
{
	public class DocumentPackageStatus : EslEnumeration
	{
        private static ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static DocumentPackageStatus DRAFT = new DocumentPackageStatus("DRAFT", "DRAFT");
        public static DocumentPackageStatus SENT = new DocumentPackageStatus("SENT", "SENT");
        public static DocumentPackageStatus COMPLETED = new DocumentPackageStatus("COMPLETED", "COMPLETED");
        public static DocumentPackageStatus ARCHIVED = new DocumentPackageStatus("ARCHIVED", "ARCHIVED");
        public static DocumentPackageStatus DECLINED = new DocumentPackageStatus("DECLINED", "DECLINED");
        public static DocumentPackageStatus OPTED_OUT = new DocumentPackageStatus("OPTED_OUT", "OPTED_OUT");
        public static DocumentPackageStatus EXPIRED = new DocumentPackageStatus("EXPIRED", "EXPIRED");
        private static Dictionary<string,DocumentPackageStatus> allDocumentPackageStatus = new Dictionary<string,DocumentPackageStatus>();

        static DocumentPackageStatus(){
            allDocumentPackageStatus.Add(DRAFT.getApiValue(), DocumentPackageStatus.DRAFT);
            allDocumentPackageStatus.Add(SENT.getApiValue(), DocumentPackageStatus.SENT);
            allDocumentPackageStatus.Add(COMPLETED.getApiValue(), DocumentPackageStatus.COMPLETED);
            allDocumentPackageStatus.Add(ARCHIVED.getApiValue(), DocumentPackageStatus.ARCHIVED);
            allDocumentPackageStatus.Add(DECLINED.getApiValue(), DocumentPackageStatus.DECLINED);
            allDocumentPackageStatus.Add(OPTED_OUT.getApiValue(), DocumentPackageStatus.OPTED_OUT);
            allDocumentPackageStatus.Add(EXPIRED.getApiValue(), DocumentPackageStatus.EXPIRED);
        }

        private DocumentPackageStatus(string apiValue, string sdkValue):base(apiValue, sdkValue) {           
        }

        internal static DocumentPackageStatus valueOf (String apiValue){

            if (!String.IsNullOrEmpty(apiValue) && allDocumentPackageStatus.ContainsKey(apiValue))
            {
                return allDocumentPackageStatus[apiValue];
            }
            log.WarnFormat("Unknown API DocumentPackageStatus {0}. The upgrade is required.", apiValue);
            return new DocumentPackageStatus(apiValue, "UNRECOGNIZED");
        }

        public static string[] GetNames(){
            string[] names = new string[allDocumentPackageStatus.Count];
            int i = 0;
            foreach(DocumentPackageStatus documentPackageStatus in allDocumentPackageStatus.Values){
                names[i] = documentPackageStatus.GetName();
                i++;
            }
            return names;
        }

        public static DocumentPackageStatus parse(string value){

            if (null == value)
            {
                throw new ArgumentNullException("value is null");
            }

            if (value.Length == 0 || value.Trim().Length==0)
            {
                throw new ArgumentException("value is either an empty string or only contains white space");
            }
            foreach(DocumentPackageStatus documentPackageStatus in allDocumentPackageStatus.Values){
                if (String.Equals(documentPackageStatus.GetName(), value))
                {
                    return documentPackageStatus;
                }
            }
            throw new ArgumentException("value is a name, but not one of the named constants defined for the DocumentPackageStatus");
        }

	}
}