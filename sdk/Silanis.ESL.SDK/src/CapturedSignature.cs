using System;
namespace Silanis.ESL.SDK
{
    public class CapturedSignature
    {
        public CapturedSignature(string handdrawn)
        {
            Handdrawn = handdrawn;
        }

        public string Handdrawn 
        { 
                get;
                set;
        }
    }
}
