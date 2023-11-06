using System;

namespace OneSpanSign.Sdk
{
    public class AccountEmailReminderSettings
    {
        private Nullable<int> _startInDaysDelay;
        private Nullable<int> _intervalInDays;
        private Nullable<int> _repetitionsCount;

        public Nullable<int> StartInDaysDelay
        {
            get { return _startInDaysDelay; }
            set { _startInDaysDelay = value; }
        }

        public Nullable<int> IntervalInDays
        {
            get { return _intervalInDays; }
            set { _intervalInDays = value; }
        }
        
        public Nullable<int> RepetitionsCount
        {
            get { return _repetitionsCount; }
            set { _repetitionsCount = value; }
        }
    }
}