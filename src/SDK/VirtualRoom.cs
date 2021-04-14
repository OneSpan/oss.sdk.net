using System;

namespace OneSpanSign.Sdk
{
    public class VirtualRoom
    {
        public bool Video
        {
            get; set;
        }

        public bool VideoRecording
        {
            get; set;
        }

        public DateTime StartDatetime
        {
            get; set;
        }

        public string HostUid
        {
            get; set;
        }
    }
}
