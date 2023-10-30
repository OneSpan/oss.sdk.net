using System;

namespace OneSpanSign.Sdk.Builder
{
    public class AccountEmailReminderSettingsBuilder
    {
        
        private Nullable<int> _startInDaysDelay;
        private Nullable<int> _intervalInDays;
        private Nullable<int> _repetitionsCount;
        private AccountEmailReminderSettingsBuilder()
        {
        }

        public static AccountEmailReminderSettingsBuilder NewAccountEmailReminderSettings()
        {
            return new AccountEmailReminderSettingsBuilder();
        }
        
        public AccountEmailReminderSettingsBuilder WithStartInDaysDelay(int startInDaysDelay)
        {
            this._startInDaysDelay = startInDaysDelay;
            return this;
        }
        
        public AccountEmailReminderSettingsBuilder WithIntervalInDays(int intervalInDays)
        {
            this._intervalInDays = intervalInDays;
            return this;
        }
        
        public AccountEmailReminderSettingsBuilder WithRepetitionsCount(int repetitionsCount)
        {
            this._repetitionsCount = repetitionsCount;
            return this;
        }

        
        public AccountEmailReminderSettings Build()
        {
            AccountEmailReminderSettings result = new AccountEmailReminderSettings();
            result.StartInDaysDelay = _startInDaysDelay;
            result.IntervalInDays = _intervalInDays;
            result.RepetitionsCount = _repetitionsCount;
            return result;
        }
    }
}