//
using System;
using System.Collections.Generic;

namespace OneSpanSign.Sdk
{
	
	
	public class License
	{
		
		// Fields
		private IList<Transaction> _transactions = new List<Transaction>();
		
		// Accessors

    public Nullable<DateTime> Created
    {
                get; set;
        }
    
    
    public Nullable<DateTime> PaidUntil
    {
                get; set;
        }
    

    public Plan Plan
    {
                get; set;
        }
    

    public string Status
    {
                get; set;
        }
    
    
    public IList<Transaction> Transactions
    {
                get
        {
            return _transactions;
        }
        }
        public License AddTransaction(Transaction value)
    {
        if (value == null)
        {
            throw new ArgumentNullException("Argument cannot be null");
        }
        
        _transactions.Add(value);
        return this;
    }
    }
}