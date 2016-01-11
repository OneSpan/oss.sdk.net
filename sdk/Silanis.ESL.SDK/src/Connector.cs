using System;
using System.Collections.Generic;

namespace Silanis.ESL.SDK
{
    public class Connector
    {
        public static readonly Connector DYNAMICS = new Connector("dynamics");
        public static readonly Connector SHAREPOINT = new Connector("sharepoint");
        public static readonly Connector DYNAMICS_2011 = new Connector("dynamics2011");
        public static readonly Connector DYNAMICS_2013 = new Connector("dynamics2013");
        public static readonly Connector DYNAMICS_2015 = new Connector("dynamics2015");
        public static readonly Connector SHAREPOINT_2010 = new Connector("sharepoint2010");
        public static readonly Connector SHAREPOINT_2013 = new Connector("sharepoint2013");

        public static IEnumerable<Connector> Values
        {
            get
            {
                yield return DYNAMICS;
                yield return SHAREPOINT;
                yield return DYNAMICS_2011;
                yield return DYNAMICS_2013;
                yield return DYNAMICS_2015;
                yield return SHAREPOINT_2010;
                yield return SHAREPOINT_2013;
            }
        }

        private readonly string name;

        Connector(string name)
        {
            this.name = name;
        }

        public string Name 
        { 
            get 
            { 
                return name; 
            } 
        }

        public override string ToString()
        {
            return name;
        }
    }
}