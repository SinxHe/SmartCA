using System;
using System.Collections.Generic;

namespace SmartCA.DataContracts
{
    [Serializable]
    public class ContactContract : PersonContract
    {
        private string jobTitle;
        private string email;
        private string phoneNumber;
        private string mobilePhoneNumber;
        private string faxNumber;
        private string remarks;
        private CompanyContract currentCompany;
        private IList<AddressContract> addresses;

        public ContactContract()
        {
            this.jobTitle = string.Empty;
            this.email = string.Empty;
            this.phoneNumber = string.Empty;
            this.mobilePhoneNumber = string.Empty;
            this.faxNumber = string.Empty;
            this.remarks = string.Empty;
            this.currentCompany = null;
            this.addresses = new List<AddressContract>();
        }

        public string JobTitle
        {
            get { return this.jobTitle; }
            set { this.jobTitle = value; }
        }

        public string Email
        {
            get { return this.email; }
            set { this.email = value; }
        }

        public string PhoneNumber
        {
            get { return this.phoneNumber; }
            set { this.phoneNumber = value; }
        }

        public string MobilePhoneNumber
        {
            get { return this.mobilePhoneNumber; }
            set { this.mobilePhoneNumber = value; }
        }

        public string FaxNumber
        {
            get { return this.faxNumber; }
            set { this.faxNumber = value; }
        }

        public string Remarks
        {
            get { return this.remarks; }
            set { this.remarks = value; }
        }

        public CompanyContract CurrentCompany
        {
            get { return this.currentCompany; }
            set { this.currentCompany = value; }
        }

        public IList<AddressContract> Addresses
        {
            get { return this.addresses; }
        }
    }
}
