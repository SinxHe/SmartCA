using System;
using System.Collections.Generic;

namespace SmartCA.DataContracts
{
    [Serializable]
    public class CompanyContract : ContractBase
    {
        private string name;
        private string abbreviation;
        private AddressContract headquartersAddress;
        private List<AddressContract> addresses;
        private string phoneNumber;
        private string faxNumber;
        private string url;
        private string remarks;

        public CompanyContract()
        {
            this.name = string.Empty;
            this.abbreviation = string.Empty;
            this.headquartersAddress = null;
            this.addresses = new List<AddressContract>();
            this.phoneNumber = string.Empty;
            this.faxNumber = string.Empty;
            this.url = string.Empty;
            this.remarks = string.Empty;
        }

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public string Abbreviation
        {
            get { return this.abbreviation; }
            set { this.abbreviation = value; }
        }

        public AddressContract HeadquartersAddress
        {
            get { return this.headquartersAddress; }
            set { this.headquartersAddress = value; }
        }

        public IList<AddressContract> Addresses
        {
            get { return this.addresses; }
        }

        public string PhoneNumber
        {
            get { return this.phoneNumber; }
            set { this.phoneNumber = value; }
        }

        public string FaxNumber
        {
            get { return this.faxNumber; }
            set { this.faxNumber = value; }
        }

        public string Url
        {
            get { return this.url; }
            set { this.url = value; }
        }

        public string Remarks
        {
            get { return this.remarks; }
            set { this.remarks = value; }
        }
    }
}
