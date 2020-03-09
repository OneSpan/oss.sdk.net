using System;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace Silanis.ESL.API
{
    public class AttachmentFile
    {

       
        private DateTime insertDate;

        [JsonProperty ("id")]
        public Int32 Id 
        {
            get; set;
        }

        [JsonProperty ("name")]
        public String Name 
        {
            get; set;
        }

        [JsonProperty ("preview")]
        public Boolean Preview 
        {
            get; set;
        }

        public DateTime GetInsertDate ()
        {
            return insertDate;
        }


        public void SetInsertDate (long value)
        {
            this.insertDate = new DateTime (value);
        }
    }
}
