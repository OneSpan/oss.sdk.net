//

using System;
using System.Collections.Generic;

namespace OneSpanSign.Sdk
{
    public class Plan
    {
        private IList<Quota> _quotas = new List<Quota>();

        public string Contract { get; set; }

        public string Cycle { get; set; }


        public IDictionary<string, object> Data { get; set; }


        public String Description { get; set; }

        public IDictionary<string, object> Features { get; set; }


        public CycleCount FreeCycles { get; set; }

        public String Group { get; set; }

        public String Id { get; set; }

        public String Name { get; set; }

        public String Original { get; set; }


        public Price Price { get; set; }


        public IList<Quota> Quotas
        {
            get { return _quotas; }
        }

        public Plan AddQuota(Quota quota)
        {
            if (quota == null)
            {
                throw new ArgumentNullException("Argument cannot be null");
            }

            _quotas.Add(quota);
            return this;
        }
    }
}