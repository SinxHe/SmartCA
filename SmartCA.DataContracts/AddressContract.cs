using System;

namespace SmartCA.DataContracts
{
    [Serializable]
    public class AddressContract
    {
        private string street;
        private string city;
        private string state;
        private string postalCode;

        public AddressContract()
        {
            this.street = string.Empty;
            this.city = string.Empty;
            this.state = string.Empty;
            this.postalCode = string.Empty;
        }

        public string Street
        {
            get { return this.street; }
            set { this.street = value; }
        }

        public string City
        {
            get { return this.city; }
            set { this.city = value; }
        }

        public string State
        {
            get { return this.state; }
            set { this.state = value; }
        }

        public string PostalCode
        {
            get { return this.postalCode; }
            set { this.postalCode = value; }
        }
    }
}
