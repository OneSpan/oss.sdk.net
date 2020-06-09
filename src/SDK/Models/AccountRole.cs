//

using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace OneSpanSign.API
{
    internal class AccountRole
    {
        public AccountRole()
        {
        }

        public AccountRole(string id, string name, List<string> permissions, string description, bool enabled)
        {
            this.Id = id;
            this.Name = name;
            this.Permissions = permissions;
            this.Description = description;
            this.Enabled = enabled;
        }

        [JsonProperty("id")] public string Id { get; set; }

        [JsonProperty("name")] public string Name { get; set; }
        [JsonProperty("enabled")] public bool Enabled { get; set; }
        [JsonProperty("description")] public string Description { get; set; }
        [JsonProperty("permissions")] public List<string> Permissions { get; set; }
    }
}