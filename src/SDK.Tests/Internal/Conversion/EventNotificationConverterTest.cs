using NUnit.Framework;
using System;
using OneSpanSign.API;
using OneSpanSign.Sdk;
using OneSpanSign.Sdk.src.Internal.Conversion;

namespace SDK.Tests
{
    [TestFixture()]
    public class EventNotificationConverterTest
    {
        private OneSpanSign.Sdk.NotificationEvent sdkNotificationEvent1;
        private string apiNotificationEvent1;

        [Test]
        public void ConvertAPIPACKAGE_ACTIVATEToINCOMPLETENotificationEvent()
        {
            apiNotificationEvent1 = "PACKAGE_ACTIVATE";
            sdkNotificationEvent1 = new EventNotificationConverter(apiNotificationEvent1).ToSDKNotificationEvent();

            Assert.AreEqual(apiNotificationEvent1, sdkNotificationEvent1.getApiValue());
        }

        [Test]
        public void ConvertAPIPACKAGE_COMPLETEToPACKAGE_COMPLETENotificationEvent()
        {
            apiNotificationEvent1 = "PACKAGE_COMPLETE";
            sdkNotificationEvent1 = new EventNotificationConverter(apiNotificationEvent1).ToSDKNotificationEvent();

            Assert.AreEqual(apiNotificationEvent1, sdkNotificationEvent1.getApiValue());
        }

        [Test]
        public void ConvertAPIPACKAGE_EXPIREToPACKAGE_EXPIRENotificationEvent()
        {
            apiNotificationEvent1 = "PACKAGE_EXPIRE";
            sdkNotificationEvent1 = new EventNotificationConverter(apiNotificationEvent1).ToSDKNotificationEvent();

            Assert.AreEqual(apiNotificationEvent1, sdkNotificationEvent1.getApiValue());
        }

        [Test]
        public void ConvertAPIPACKAGE_DECLINEToPACKAGE_DECLINENotificationEvent()
        {
            apiNotificationEvent1 = "PACKAGE_DECLINE";
            sdkNotificationEvent1 = new EventNotificationConverter(apiNotificationEvent1).ToSDKNotificationEvent();

            Assert.AreEqual(apiNotificationEvent1, sdkNotificationEvent1.getApiValue());
        }

        [Test]
        public void ConvertAPISIGNER_COMPLETEToSIGNER_COMPLETENotificationEvent()
        {
            apiNotificationEvent1 = "SIGNER_COMPLETE";
            sdkNotificationEvent1 = new EventNotificationConverter(apiNotificationEvent1).ToSDKNotificationEvent();

            Assert.AreEqual(apiNotificationEvent1, sdkNotificationEvent1.getApiValue());
        }

        [Test]
        public void ConvertAPIDOCUMENT_SIGNEDToDOCUMENT_SIGNEDNotificationEvent()
        {
            apiNotificationEvent1 = "DOCUMENT_SIGNED";
            sdkNotificationEvent1 = new EventNotificationConverter(apiNotificationEvent1).ToSDKNotificationEvent();

            Assert.AreEqual(apiNotificationEvent1, sdkNotificationEvent1.getApiValue());
        }
        
        [Test]
        public void ConvertAPIDOCUMENT_VIEWEDToDOCUMENT_VIEWEDNotificationEvent()
        {
            apiNotificationEvent1 = "DOCUMENT_VIEWED";
            sdkNotificationEvent1 = new EventNotificationConverter(apiNotificationEvent1).ToSDKNotificationEvent();

            Assert.AreEqual(apiNotificationEvent1, sdkNotificationEvent1.getApiValue());
        }

        [Test]
        public void ConvertAPIROLE_REASSIGNToROLE_REASSIGNNotificationEvent()
        {
            apiNotificationEvent1 = "ROLE_REASSIGN";
            sdkNotificationEvent1 = new EventNotificationConverter(apiNotificationEvent1).ToSDKNotificationEvent();

            Assert.AreEqual(apiNotificationEvent1, sdkNotificationEvent1.getApiValue());
        }

        [Test]
        public void ConvertAPIPACKAGE_CREATEToPACKAGE_CREATENotificationEvent()
        {
            apiNotificationEvent1 = "PACKAGE_CREATE";
            sdkNotificationEvent1 = new EventNotificationConverter(apiNotificationEvent1).ToSDKNotificationEvent();

            Assert.AreEqual(apiNotificationEvent1, sdkNotificationEvent1.getApiValue());
        }

        [Test]
        public void ConvertAPIPACKAGE_DEACTIVATEToPACKAGE_DEACTIVATENotificationEvent()
        {
            apiNotificationEvent1 = "PACKAGE_DEACTIVATE";
            sdkNotificationEvent1 = new EventNotificationConverter(apiNotificationEvent1).ToSDKNotificationEvent();

            Assert.AreEqual(apiNotificationEvent1, sdkNotificationEvent1.getApiValue());
        }

        [Test]
        public void ConvertAPIPACKAGE_READY_FOR_COMPLETEToPACKAGE_READY_FOR_COMPLETENotificationEvent()
        {
            apiNotificationEvent1 = "PACKAGE_READY_FOR_COMPLETE";
            sdkNotificationEvent1 = new EventNotificationConverter(apiNotificationEvent1).ToSDKNotificationEvent();

            Assert.AreEqual(apiNotificationEvent1, sdkNotificationEvent1.getApiValue());
        }

        [Test]
        public void ConvertAPIPACKAGE_TRASHToPACKAGE_TRASHNotificationEvent()
        {
            apiNotificationEvent1 = "PACKAGE_TRASH";
            sdkNotificationEvent1 = new EventNotificationConverter(apiNotificationEvent1).ToSDKNotificationEvent();

            Assert.AreEqual(apiNotificationEvent1, sdkNotificationEvent1.getApiValue());
        }

        [Test]
        public void ConvertAPIPACKAGE_RESTOREToPACKAGE_RESTORENotificationEvent()
        {
            apiNotificationEvent1 = "PACKAGE_RESTORE";
            sdkNotificationEvent1 = new EventNotificationConverter(apiNotificationEvent1).ToSDKNotificationEvent();

            Assert.AreEqual(apiNotificationEvent1, sdkNotificationEvent1.getApiValue());
        }

        [Test]
        public void ConvertAPIPACKAGE_DELETEToPACKAGE_DELETENotificationEvent()
        {
            apiNotificationEvent1 = "PACKAGE_DELETE";
            sdkNotificationEvent1 = new EventNotificationConverter(apiNotificationEvent1).ToSDKNotificationEvent();

            Assert.AreEqual(apiNotificationEvent1, sdkNotificationEvent1.getApiValue());
        }

        [Test]
        public void ConvertAPIUnknonwnValueToUnrecognizedNotificationEvent()
        {
            apiNotificationEvent1 = "NEWLY_ADDED_NOTIFICATION_EVENT";
            sdkNotificationEvent1 = new EventNotificationConverter(apiNotificationEvent1).ToSDKNotificationEvent();

            Assert.AreEqual(sdkNotificationEvent1.getApiValue(), apiNotificationEvent1);
        }

        [Test]
        public void ConvertSDKINCOMPLETEToAPIINCOMPLETE()
        {
            sdkNotificationEvent1 = OneSpanSign.Sdk.NotificationEvent.PACKAGE_ACTIVATE;
            apiNotificationEvent1 = new EventNotificationConverter(sdkNotificationEvent1).ToAPICallbackEvent();

            Assert.AreEqual("PACKAGE_ACTIVATE", apiNotificationEvent1);
        }

        [Test]
        public void ConvertSDKREJECTEDToAPIREJECTED()
        {
            sdkNotificationEvent1 = OneSpanSign.Sdk.NotificationEvent.PACKAGE_COMPLETE;
            apiNotificationEvent1 = new EventNotificationConverter(sdkNotificationEvent1).ToAPICallbackEvent();

            Assert.AreEqual("PACKAGE_COMPLETE", apiNotificationEvent1);
        }

        [Test]
        public void ConvertSDKDELETEToAPIDELETE()
        {
            sdkNotificationEvent1 = OneSpanSign.Sdk.NotificationEvent.PACKAGE_DELETE;
            apiNotificationEvent1 = new EventNotificationConverter(sdkNotificationEvent1).ToAPICallbackEvent();

            Assert.AreEqual("PACKAGE_DELETE", apiNotificationEvent1);
        }

        [Test]
        public void ConvertSDKPACKAGE_DECLINEToAPIPACKAGE_DECLINE()
        {
            sdkNotificationEvent1 = OneSpanSign.Sdk.NotificationEvent.PACKAGE_DECLINE;
            apiNotificationEvent1 = new EventNotificationConverter(sdkNotificationEvent1).ToAPICallbackEvent();

            Assert.AreEqual("PACKAGE_DECLINE", apiNotificationEvent1);
        }

        [Test]
        public void ConvertSDKSIGNER_COMPLETEToAPISIGNER_COMPLETE()
        {
            sdkNotificationEvent1 = OneSpanSign.Sdk.NotificationEvent.SIGNER_COMPLETE;
            apiNotificationEvent1 = new EventNotificationConverter(sdkNotificationEvent1).ToAPICallbackEvent();

            Assert.AreEqual("SIGNER_COMPLETE", apiNotificationEvent1);
        }

        [Test]
        public void ConvertSDKDOCUMENT_SIGNEDToAPIDOCUMENT_SIGNED()
        {
            sdkNotificationEvent1 = OneSpanSign.Sdk.NotificationEvent.DOCUMENT_SIGNED;
            apiNotificationEvent1 = new EventNotificationConverter(sdkNotificationEvent1).ToAPICallbackEvent();

            Assert.AreEqual("DOCUMENT_SIGNED", apiNotificationEvent1);
        }
        
        [Test]
        public void ConvertSDKDOCUMENT_VIEWEDToAPIDOCUMENT_VIEWED()
        {
            sdkNotificationEvent1 = OneSpanSign.Sdk.NotificationEvent.DOCUMENT_VIEWED;
            apiNotificationEvent1 = new EventNotificationConverter(sdkNotificationEvent1).ToAPICallbackEvent();

            Assert.AreEqual("DOCUMENT_VIEWED", apiNotificationEvent1);
        }


        [Test]
        public void ConvertSDKROLE_REASSIGNToAPIROLE_REASSIGN()
        {
            sdkNotificationEvent1 = OneSpanSign.Sdk.NotificationEvent.ROLE_REASSIGN;
            apiNotificationEvent1 = new EventNotificationConverter(sdkNotificationEvent1).ToAPICallbackEvent();

            Assert.AreEqual("ROLE_REASSIGN", apiNotificationEvent1);
        }


        [Test]
        public void ConvertSDKPACKAGE_CREATEToAPIPACKAGE_CREATE()
        {
            sdkNotificationEvent1 = OneSpanSign.Sdk.NotificationEvent.PACKAGE_CREATE;
            apiNotificationEvent1 = new EventNotificationConverter(sdkNotificationEvent1).ToAPICallbackEvent();

            Assert.AreEqual("PACKAGE_CREATE", apiNotificationEvent1);
        }


        [Test]
        public void ConvertSDKPACKAGE_DEACTIVATEToAPIPACKAGE_DEACTIVATE()
        {
            sdkNotificationEvent1 = OneSpanSign.Sdk.NotificationEvent.PACKAGE_DEACTIVATE;
            apiNotificationEvent1 = new EventNotificationConverter(sdkNotificationEvent1).ToAPICallbackEvent();

            Assert.AreEqual("PACKAGE_DEACTIVATE", apiNotificationEvent1);
        }

        [Test]
        public void ConvertSDKPACKAGE_READY_FOR_COMPLETEToAPIPACKAGE_READY_FOR_COMPLETE()
        {
            sdkNotificationEvent1 = OneSpanSign.Sdk.NotificationEvent.PACKAGE_READY_FOR_COMPLETION;
            apiNotificationEvent1 = new EventNotificationConverter(sdkNotificationEvent1).ToAPICallbackEvent();

            Assert.AreEqual("PACKAGE_READY_FOR_COMPLETE", apiNotificationEvent1);
        }

        [Test]
        public void ConvertSDKPACKAGE_TRASHToAPIPACKAGE_TRASH()
        {
            sdkNotificationEvent1 = OneSpanSign.Sdk.NotificationEvent.PACKAGE_TRASH;
            apiNotificationEvent1 = new EventNotificationConverter(sdkNotificationEvent1).ToAPICallbackEvent();

            Assert.AreEqual("PACKAGE_TRASH", apiNotificationEvent1);
        }


        [Test]
        public void ConvertSDKPACKAGE_RESTOREToAPIPACKAGE_RESTORE()
        {
            sdkNotificationEvent1 = OneSpanSign.Sdk.NotificationEvent.PACKAGE_RESTORE;
            apiNotificationEvent1 = new EventNotificationConverter(sdkNotificationEvent1).ToAPICallbackEvent();

            Assert.AreEqual("PACKAGE_RESTORE", apiNotificationEvent1);
        }

        [Test]
        public void ConvertSDKPACKAGE_DELETEToAPIPACKAGE_DELETE()
        {
            sdkNotificationEvent1 = OneSpanSign.Sdk.NotificationEvent.PACKAGE_DELETE;
            apiNotificationEvent1 = new EventNotificationConverter(sdkNotificationEvent1).ToAPICallbackEvent();

            Assert.AreEqual("PACKAGE_DELETE", apiNotificationEvent1);
        }

        [Test]
        public void ConvertSDKKBA_FAILUREToAPIKBA_FAILURE()
        {
            sdkNotificationEvent1 = OneSpanSign.Sdk.NotificationEvent.KBA_FAILURE;
            apiNotificationEvent1 = new EventNotificationConverter(sdkNotificationEvent1).ToAPICallbackEvent();

            Assert.AreEqual("KBA_FAILURE", apiNotificationEvent1);
        }

        [Test]
        public void ConvertSDKEMAIL_BOUNCEToAPIEMAIL_BOUNCE()
        {
            sdkNotificationEvent1 = OneSpanSign.Sdk.NotificationEvent.EMAIL_BOUNCE;
            apiNotificationEvent1 = new EventNotificationConverter(sdkNotificationEvent1).ToAPICallbackEvent();

            Assert.AreEqual("EMAIL_BOUNCE", apiNotificationEvent1);
        }

        [Test]
        public void ConvertSDKPACKAGE_ATTACHMENTToAPIPACKAGE_ATTACHMENT()
        {
            sdkNotificationEvent1 = OneSpanSign.Sdk.NotificationEvent.PACKAGE_ATTACHMENT;
            apiNotificationEvent1 = new EventNotificationConverter(sdkNotificationEvent1).ToAPICallbackEvent();

            Assert.AreEqual("PACKAGE_ATTACHMENT", apiNotificationEvent1);
        }

        [Test]
        public void ConvertSDKSIGNER_LOCKEDToAPISIGNER_LOCKED()
        {
            sdkNotificationEvent1 = OneSpanSign.Sdk.NotificationEvent.SIGNER_LOCKED;
            apiNotificationEvent1 = new EventNotificationConverter(sdkNotificationEvent1).ToAPICallbackEvent();

            Assert.AreEqual("SIGNER_LOCKED", apiNotificationEvent1);
        }

        [Test]
        public void ConvertSDKPACKAGE_ARCHIVEToAPIPACKAGE_ARCHIVE()
        {
            sdkNotificationEvent1 = OneSpanSign.Sdk.NotificationEvent.PACKAGE_ARCHIVE;
            apiNotificationEvent1 = new EventNotificationConverter(sdkNotificationEvent1).ToAPICallbackEvent();

            Assert.AreEqual("PACKAGE_ARCHIVE", apiNotificationEvent1);
        }

        [Test]
        public void ConvertSDKTEMPLATE_CREATEToAPITEMPLATE_CREATE()
        {
            sdkNotificationEvent1 = OneSpanSign.Sdk.NotificationEvent.TEMPLATE_CREATE;
            apiNotificationEvent1 = new EventNotificationConverter(sdkNotificationEvent1).ToAPICallbackEvent();

            Assert.AreEqual("TEMPLATE_CREATE", apiNotificationEvent1);
        }

        [Test]
        public void ConvertSDKUnrecognizedNotificationEventToAPIUnknownValue()
        {
            apiNotificationEvent1 = "NEWLY_ADDED_REQUIREMENT_STATUS";
            NotificationEvent unrecognizedNotificationEvent = NotificationEvent.valueOf(apiNotificationEvent1);
            string acutalApiValue = new EventNotificationConverter(unrecognizedNotificationEvent).ToAPICallbackEvent();

            Assert.AreEqual(apiNotificationEvent1, acutalApiValue);
        }

    }
}

