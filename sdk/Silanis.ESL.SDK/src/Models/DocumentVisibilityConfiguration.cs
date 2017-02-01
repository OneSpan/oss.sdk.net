using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Silanis.ESL.API
{
    internal class DocumentVisibilityConfiguration
    {
        // Fields
        protected List<string> _roleUids = new List<string>();

        // Accessors
        [JsonProperty("documentUid")]
        public string DocumentUid
        {
            get; set;
        }

        [JsonProperty("roleUids")]
        public List<string> RoleUids
        {
            get
            {
                return _roleUids;
            }
            set {
                _roleUids = value;
            }
        }
        public DocumentVisibilityConfiguration AddRoleUid(string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("Argument cannot be null");
            }

            _roleUids.Add(value);
            return this;
        }
    }
}

