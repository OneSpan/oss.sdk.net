//
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace OneSpanSign.API
{


    internal class AttachmentRequirement
    {

        // Fields
        private IList<AttachmentFile> files = new List<AttachmentFile> ();
        // Accessors

        [JsonProperty ("comment")]
        public String Comment 
        {
            get; set;
        }


        [JsonProperty ("data")]
        public IDictionary<string, object> Data 
        {
            get; set;
        }


        [JsonProperty ("description")]
        public String Description 
        {
            get; set;
        }


        [JsonProperty ("id")]
        public String Id 
        {
            get; set;
        }


        [JsonProperty ("name")]
        public String Name 
        {
            get; set;
        }


        [JsonProperty ("required")]
        public Nullable<Boolean> Required 
        {
            get; set;
        }


        [JsonProperty ("status")]
        public string Status 
        {
            get; set;
        }

        [JsonProperty ("files")]
        public IList<AttachmentFile> Files 
        {
            get 
            {
                return files;
            }
            set 
            {
             this.files = value;
            }
        }

    }


}