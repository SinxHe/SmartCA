using System;
using SmartCA.Infrastructure.DomainBase;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using SmartCA.Model.Addresses;

namespace SmartCA.Model.Companies
{
    public class Company : EntityBase, IAggregateRoot, IHasAddresses
    {
        private string name;
        private string abbreviation;
        private Address headquartersAddress;
        private List<Address> addresses;
        private string phoneNumber;
        private string faxNumber;
        private string url;
        private string remarks;

        public Company()
            : this(null)
        {
        }

        public Company(object key)
            : base(key)
        {
            this.name = string.Empty;
            this.abbreviation = string.Empty;
            this.headquartersAddress = null;
            this.addresses = new List<Address>();
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

        public Address HeadquartersAddress
        {
            get { return this.headquartersAddress; }
            set 
            {
                if (this.headquartersAddress != value)
                {
                    this.headquartersAddress = value;
                    if (!this.addresses.Contains(value))
                    {
                        this.addresses.Add(value);
                    }
                }
            }
        }

        public IList<Address> Addresses
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

        protected override void Validate()
        {
            // The Company must have a name
            if (!string.IsNullOrEmpty(this.name))
            {
                this.BrokenRules.Add(new BrokenRule("", ""));
            }
        }

        protected override BrokenRuleMessages GetBrokenRuleMessages()
        {
            return new CompanyRuleMessages();
        }
    }
}
