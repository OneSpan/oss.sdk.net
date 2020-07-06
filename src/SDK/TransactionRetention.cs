﻿using System;
namespace OneSpanSign.Sdk
{
    public class TransactionRetention
    {

        public Nullable<Int32> Draft 
        {
            get; set;
        }

        public Nullable<Int32> Sent 
        {
            get; set;
        }

        public Nullable<Int32> Completed 
        {
            get; set;
        }

        public Nullable<Int32> Archived 
        {
            get; set;
        }

        public Nullable<Int32> Declined 
        {
            get; set;
        }

        public Nullable<Int32> OptedOut 
        {
            get; set;
        }

        public Nullable<Int32> Expired 
        {
            get; set;
        }
        public Nullable<Int32> LifetimeTotal 
        {
            get; set;
        }
        public Nullable<Int32> LifetimeUntilCompletion 
        {
            get; set;
        }
    }
}
