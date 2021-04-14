using System;
using NUnit.Framework;
using OneSpanSign.Sdk;
namespace SDK.Tests
{
    [TestFixture()]
    public class VirtualRoomConverterTest
    {

        private OneSpanSign.Sdk.VirtualRoom sdkVirtualRoom1 = null;
        private OneSpanSign.Sdk.VirtualRoom sdkVirtualRoom2 = null;
        private OneSpanSign.API.VirtualRoom apiVirtualRoom1 = null;
        private OneSpanSign.API.VirtualRoom apiVirtualRoom2 = null;
        private VirtualRoomConverter converter = null;

        [Test()]
        public void convertNullSDKToAPI()
        {
            sdkVirtualRoom1 = null;
            converter = new VirtualRoomConverter(sdkVirtualRoom1);
            Assert.IsNull(converter.ToAPIVirtualRoom());

        }

        [Test()]
        public void convertNullAPIToSDK()
        {
            apiVirtualRoom1 = null;
            converter = new VirtualRoomConverter(apiVirtualRoom1);
            Assert.IsNull(converter.ToSDKVirtualRoom());

        }

        [Test()]
        public void convertNullSDKToSDK()
        {
            sdkVirtualRoom1 = null;
            converter = new VirtualRoomConverter(sdkVirtualRoom1);
            Assert.IsNull(converter.ToSDKVirtualRoom());

        }

        [Test()]
        public void convertNullAPIToAPI()
        {
            apiVirtualRoom1 = null;
            converter = new VirtualRoomConverter(apiVirtualRoom1);
            Assert.IsNull(converter.ToAPIVirtualRoom());

        }

        [Test()]
        public void convertSDKToSDK()
        {
            sdkVirtualRoom1 = new VirtualRoom();
            sdkVirtualRoom2 = new VirtualRoomConverter(sdkVirtualRoom1).ToSDKVirtualRoom();
            Assert.IsNotNull(sdkVirtualRoom2);
            Assert.AreEqual(sdkVirtualRoom2, sdkVirtualRoom1);
        }

        [Test()]
        public void convertAPIToAPI()
        {
            apiVirtualRoom1 = new OneSpanSign.API.VirtualRoom();
            apiVirtualRoom2 = new VirtualRoomConverter(apiVirtualRoom1).ToAPIVirtualRoom();

            Assert.IsNotNull(apiVirtualRoom2);
            Assert.AreEqual(apiVirtualRoom2, apiVirtualRoom1);

        }

        [Test()]
        public void convertAPIToSDK()
        {
            apiVirtualRoom1 = buildApiVirtualRoom();
            sdkVirtualRoom1 = new VirtualRoomConverter(apiVirtualRoom1).ToSDKVirtualRoom();

            Assert.IsNotNull(sdkVirtualRoom1);
            Assert.AreEqual(sdkVirtualRoom1.Video, apiVirtualRoom1.Video);
            Assert.AreEqual(sdkVirtualRoom1.VideoRecording, apiVirtualRoom1.VideoRecording);
            Assert.AreEqual(sdkVirtualRoom1.StartDatetime, apiVirtualRoom1.StartDatetime);
            Assert.AreEqual(sdkVirtualRoom1.HostUid, apiVirtualRoom1.HostUid);
        }

        [Test()]
        public void convertSDKToAPI()
        {
            sdkVirtualRoom1 = buildSdkVirtualRoom();
            apiVirtualRoom1 = new VirtualRoomConverter(sdkVirtualRoom1).ToAPIVirtualRoom();

            Assert.IsNotNull(apiVirtualRoom1);
            Assert.AreEqual(apiVirtualRoom1.Video, sdkVirtualRoom1.Video);
            Assert.AreEqual(apiVirtualRoom1.VideoRecording, sdkVirtualRoom1.VideoRecording);
            Assert.AreEqual(apiVirtualRoom1.StartDatetime, sdkVirtualRoom1.StartDatetime);
            Assert.AreEqual(apiVirtualRoom1.HostUid, sdkVirtualRoom1.HostUid);
        }

        private VirtualRoom buildSdkVirtualRoom()
        {
            VirtualRoom result = new VirtualRoom();
            result.Video = true;
            result.VideoRecording = true;
            result.StartDatetime = DateTime.Now;
            result.HostUid = "hostUid";
            return result;
        }


        private OneSpanSign.API.VirtualRoom buildApiVirtualRoom()
        {
            OneSpanSign.API.VirtualRoom result = new OneSpanSign.API.VirtualRoom();
            result.Video = true;
            result.VideoRecording = true;
            result.StartDatetime = DateTime.Now;
            result.HostUid = "hostUid";
            return result;
        }
    }
}
