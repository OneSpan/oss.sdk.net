using System;
using System.Collections.Generic;

namespace OneSpanSign.Sdk
{
	public class FieldValidator
	{
        private List<String> options = new List<String>();

		public string Regex {
			get;
			set;
		}

		public Nullable<Int32> MinLength {
			get;
			set;
		}

		public Nullable<Int32> MaxLength {
			get;
			set;
		}

		public string Message {
			get;
			set;
		}

        public Nullable<Int32> ErrorCode
        {
            get;
            set;
        }

		public bool Required {
			get;
			set;
		}

        public bool Disabled {
            get;
            set;
        }

        public List<string> Options
        {
            get
            {
                return options;
            }
            set
            {
                options = value;
            }
        }

        public void AddOption(string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("Argument cannot be null");
            }

            options.Add(value);

        }

        public void AddOptions (IList<string> options)
        {
            this.options.AddRange (options);
        }
        
        public string Group {
	        get;
	        set;
        }
        
        public Nullable<Int32> MinimumRequired
        {
	        get;
	        set;
        }
        
	}
}