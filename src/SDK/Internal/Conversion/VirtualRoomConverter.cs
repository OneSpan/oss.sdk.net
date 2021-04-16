using System;
namespace OneSpanSign.Sdk
{
    internal class VirtualRoomConverter
    {
        private VirtualRoom sdkVirtualRoom;
        private OneSpanSign.API.VirtualRoom apiVirtualRoom;

        public VirtualRoomConverter(OneSpanSign.API.VirtualRoom apiVirtualRoom)
        {
            this.apiVirtualRoom = apiVirtualRoom;
        }

        public VirtualRoomConverter(VirtualRoom sdkVirtualRoom)
        {
            this.sdkVirtualRoom = sdkVirtualRoom;
        }

        internal OneSpanSign.API.VirtualRoom ToAPIVirtualRoom()
        {
            if (sdkVirtualRoom == null)
            {
                return apiVirtualRoom;
            }
            apiVirtualRoom = new OneSpanSign.API.VirtualRoom();
            apiVirtualRoom.Video = sdkVirtualRoom.Video;
            apiVirtualRoom.VideoRecording = sdkVirtualRoom.VideoRecording;
            apiVirtualRoom.HostUid = sdkVirtualRoom.HostUid;
            apiVirtualRoom.StartDatetime = sdkVirtualRoom.StartDatetime;

            return apiVirtualRoom;
        }

        internal OneSpanSign.Sdk.VirtualRoom ToSDKVirtualRoom()
        {
            if (apiVirtualRoom == null)
            {
                return sdkVirtualRoom;
            }
            sdkVirtualRoom = new VirtualRoom();
            sdkVirtualRoom.Video = apiVirtualRoom.Video;
            sdkVirtualRoom.VideoRecording = apiVirtualRoom.VideoRecording;
            sdkVirtualRoom.HostUid = apiVirtualRoom.HostUid;
            sdkVirtualRoom.StartDatetime = apiVirtualRoom.StartDatetime;

            return sdkVirtualRoom;
        }
    }
}
