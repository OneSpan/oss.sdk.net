using System;
namespace OneSpanSign.Sdk
{
    public class ExpiryTimeConfiguration
    {
        public Nullable<Int32> MaximumRemainingDays 
        {
            get; set;
        }

        public Nullable<Int32> RemainingDays 
        {
            get; set;
        }
    }
}
