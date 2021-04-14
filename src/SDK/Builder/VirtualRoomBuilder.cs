using System;
namespace OneSpanSign.Sdk.Builder
{
    public class VirtualRoomBuilder
    {
        private bool video;
        private bool videoRecording;
        private DateTime startDatetime;
        private string hostUid;

        private VirtualRoomBuilder() { }

        public static VirtualRoomBuilder NewVirtualRoom()
        {
            return new VirtualRoomBuilder();
        }

        public VirtualRoomBuilder WithVideo(bool video)
        {
            this.video = video;
            return this;
        }

        public VirtualRoomBuilder WithVideoRecording(bool videoRecording)
        {
            this.videoRecording = videoRecording;
            return this;
        }
        public VirtualRoomBuilder WithStartDateTime(DateTime startDatetime)
        {
            this.startDatetime = startDatetime;
            return this;
        }
        public VirtualRoomBuilder WithHostUid(string hostUid)
        {
            this.hostUid = hostUid;
            return this;
        }

        public VirtualRoom Build()
        {
            VirtualRoom result = new VirtualRoom();
            result.Video = video;
            result.VideoRecording = videoRecording;
            result.StartDatetime = startDatetime;
            result.HostUid = hostUid;
            return result;
        }
    }
}
