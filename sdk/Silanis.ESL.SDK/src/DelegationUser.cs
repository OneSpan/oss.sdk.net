using System;

namespace Silanis.ESL.SDK
{
    public class DelegationUser
    {
        private string email;
        private string firstName;
        private string id;
        private string lastName;
        private string name;

        public DelegationUser()
        {
        }

        public string Email { 
            get;
            set;
        }

        public string FirstName {
            get;
            set;
        }

        public string Id {
            get;
            set;
        }

        public string LastName{
            get;
            set;
        }

        public string Name {
            get;
            set;
        }
    }
}

