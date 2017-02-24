using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartCA.DataContracts
{
    [Serializable]
    public abstract class PersonContract : ContractBase
    {
        private string firstName;
        private string lastName;
        private string initials;

        protected PersonContract()
        {
            this.firstName = string.Empty;
            this.lastName = string.Empty;
            this.initials = string.Empty;
        }

        public string FirstName
        {
            get { return this.firstName; }
            set { this.firstName = value; }
        }

        public string LastName
        {
            get { return this.lastName; }
            set { this.lastName = value; }
        }

        public string Initials
        {
            get { return this.initials; }
            set { this.initials = value; }
        }
    }
}
