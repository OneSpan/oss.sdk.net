using System;
namespace OneSpanSign.Sdk
{
    public class AttachmentFile
    {
        public Int32 Id 
        {
            get; set;
        }

        public DateTime InsertDate 
        {
            get; set;
        }

        public String Name 
        {
            get; set;
        }

        public Boolean Preview 
        {
            get; set;
        }
    }
}
