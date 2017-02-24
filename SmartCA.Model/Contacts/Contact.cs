using System;
using System.Collections.Generic;
using System.Text;
using SmartCA.Model.Companies;
using SmartCA.Infrastructure.DomainBase;
using SmartCA.Model.Addresses;

namespace SmartCA.Model.Contacts
{
    public class Contact : Person, IAggregateRoot, IHasAddresses
    {
        private string jobTitle;
        private string email;
        private string phoneNumber;
        private string mobilePhoneNumber;
        private string faxNumber;
        private string remarks;
        private Company currentCompany;
        private IList<Address> addresses;

        public Contact()
            : this(null)
        {
        }

        public Contact(object key)
            : this(key, null, null)
        {
        }

        public Contact(object key, string firstName, string lastName) 
            : base(key, firstName, lastName)
        {
            this.jobTitle = string.Empty;
            this.email = string.Empty;
            this.phoneNumber = string.Empty;
            this.mobilePhoneNumber = string.Empty;
            this.faxNumber = string.Empty;
            this.remarks = string.Empty;
            this.currentCompany = null;
            this.addresses = new List<Address>();
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

        public Company CurrentCompany
        {
            get { return this.currentCompany; }
            set { this.currentCompany = value; }
        }

        public IList<Address> Addresses
        {
            get { return this.addresses; }
        }

        protected override void Validate()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override BrokenRuleMessages GetBrokenRuleMessages()
        {
            return new ContactRuleMessages();
        }
    }
}
