//
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace OneSpanSign.API
{
	
	
	internal class Document
	{
		
		// Fields
		private IList<Approval> _approvals = new List<Approval>();
		private IList<Field> _fields = new List<Field>();
		private IList<Page> _pages = new List<Page>();
        private IList<string> _extractionTypes = new List<string>();


		// Accessors
		    

        [JsonProperty ("status")]
        public string Status {
            get; set;
        }


    [JsonProperty("approvals")]
    public IList<Approval> Approvals
    {
                get
        {
            return _approvals;
        }
            set
            {
                _approvals = value;
            }
        }
        public Document AddApproval(Approval value)
    {
        if (value == null)
        {
            throw new ArgumentNullException("Argument cannot be null");
        }
        
        _approvals.Add(value);
        return this;
    }
    
		    
    [JsonProperty("data")]
    public IDictionary<string, object> Data
    {
                get; set;
        }
    
		    
    [JsonProperty("description")]
    public String Description
    {
                get; set;
        }
    
    [JsonProperty("base64Content")]
    public String Base64Content
    {
                get; set;
        }
		    
    [JsonProperty("external")]
    public External External
    {
                get; set;
        }
    
		    
    [JsonProperty("extract")]
    public Nullable<Boolean> Extract
    {
                get; set;
        }

        [JsonProperty("tagged")]
        public Nullable<Boolean> Tagged
        {
            get; set;
        }
    
		    
        [JsonProperty("extractionTypes")]
        public IList<string> ExtractionTypes
        {
            get
            {
                return _extractionTypes;
            }
            set
            {
                _extractionTypes = value;
            }
        }
        public Document AddExtractionType(string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("Argument cannot be null");
            }

            _extractionTypes.Add(value);
            return this;
        }


    [JsonProperty("fields")]
    public IList<Field> Fields
    {
                get
        {
            return _fields;
        }
            set
            {
                _fields = value;
            }
        }
        public Document AddField(Field value)
    {
        if (value == null)
        {
            throw new ArgumentNullException("Argument cannot be null");
        }
        
        _fields.Add(value);
        return this;
    }
    
		    
    [JsonProperty("id")]
    public String Id
    {
                get; set;
        }
    
		    
    [JsonProperty("index")]
    public Nullable<Int32> Index
    {
                get; set;
        }
    
		    
    [JsonProperty("name")]
    public String Name
    {
                get; set;
        }
    
		    
    [JsonProperty("pages")]
    public IList<Page> Pages
    {
                get
        {
            return _pages;
        }
            set
            {
                _pages = value;
            }
        }
        public Document AddPage(Page value)
    {
        if (value == null)
        {
            throw new ArgumentNullException("Argument cannot be null");
        }
        
        _pages.Add(value);
        return this;
    }
    
		    
        [JsonProperty("size")]
        public Nullable<Int32> Size
        {
                get; set;
        }
    
        [JsonProperty ("signedHash")]
        public string SignedHash {
            get; set;
        }
		
        [JsonProperty ("signerVerificationToken")]
        public string SignerVerificationToken {
            get; set;
        }

        [JsonProperty("externalSigned")]
        public Nullable<Boolean> ExternalSigned
        {
            get; set;
        }
	
	}
}