using System;
namespace OneSpanSign.Sdk
{
    public class ExpiryTimeConfigurationBuilder
    {
        private int maximumRemainingDays;
        private int remainingDays;

        private ExpiryTimeConfigurationBuilder () { }

        public static ExpiryTimeConfigurationBuilder NewExpiryTimeConfiguration ()
        {
            return new ExpiryTimeConfigurationBuilder ();
        }

        public ExpiryTimeConfigurationBuilder WithMaximumRemainingDays (int maximumRemainingDays)
        {
            this.maximumRemainingDays = maximumRemainingDays;
            return this;
        }

        public ExpiryTimeConfigurationBuilder WithRemainingDays (int remainingDays)
        {
            this.remainingDays = remainingDays;
            return this;
        }

        public ExpiryTimeConfiguration Build ()
        {
            ExpiryTimeConfiguration result = new ExpiryTimeConfiguration ();
            result.MaximumRemainingDays = maximumRemainingDays;
            result.RemainingDays = remainingDays;
            return result;
        }
    }
}
