using System;
using System.Collections.Generic;

namespace OneSpanSign.Sdk
{
    public class AccountRole
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
        public string Id { get; set; }

        public string Name { get; set; }

        public List<string> Permissions { get; set; }

        public string Description { get; set; }

        public bool Enabled { get; set; }
    }
}