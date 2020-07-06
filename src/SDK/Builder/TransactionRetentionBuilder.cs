using System;
namespace OneSpanSign.Sdk
{
    public class TransactionRetentionBuilder
    {
        private int draft;
        private int sent;
        private int completed;
        private int archived;
        private int declined;
        private int optedOut;
        private int expired;
        private int lifetimeTotal;
        private int lifetimeUntilCompletion;

        private TransactionRetentionBuilder () { }

        public static TransactionRetentionBuilder NewTransactionRetention ()
        {
            return new TransactionRetentionBuilder ();
        }

        public TransactionRetentionBuilder WithDraft (int draft)
        {
            this.draft = draft;
            return this;
        }

        public TransactionRetentionBuilder WithSent (int sent)
        {
            this.sent = sent;
            return this;
        }
        public TransactionRetentionBuilder WithCompleted (int completed)
        {
            this.completed = completed;
            return this;
        }
        public TransactionRetentionBuilder WithArchived (int archived)
        {
            this.archived = archived;
            return this;
        }
        public TransactionRetentionBuilder WithDeclined (int declined)
        {
            this.declined = declined;
            return this;
        }
        public TransactionRetentionBuilder WithOptedOut (int optedOut)
        {
            this.optedOut = optedOut;
            return this;
        }
        public TransactionRetentionBuilder WithExpired (int expired)
        {
            this.expired = expired;
            return this;
        }
        
        public TransactionRetentionBuilder WithLifetimeTotal(int lifetimeTotal)
        {
            this.lifetimeTotal = lifetimeTotal;
            return this;
        }
    
        public TransactionRetentionBuilder WithLifetimeUntilCompletion(int lifetimeUntilCompletion)
        {
            this.lifetimeUntilCompletion = lifetimeUntilCompletion;
            return this;
        }

        public TransactionRetention Build ()
        {
            TransactionRetention result = new TransactionRetention ();
            result.Draft = draft;
            result.Sent = sent;
            result.Completed = completed;
            result.OptedOut = optedOut;
            result.Expired = expired;
            result.Declined = declined;
            result.Archived = archived;
            if (lifetimeTotal != null)
            {
                result.LifetimeTotal = lifetimeTotal;
            }
            if (lifetimeUntilCompletion != null)
            {
                result.LifetimeUntilCompletion = lifetimeUntilCompletion;
            }

            return result;
        }
    }
}
