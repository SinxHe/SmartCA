using System;
using SmartCA.Infrastructure.DomainBase;
using SmartCA.Model.Companies;

namespace SmartCA.Model.Projects
{
    public class Contract : EntityBase
    {
        private Company contractor;
        private string scopeOfWork;
        private string bidPackageNumber;
        private DateTime? contractDate;
        private DateTime? noticeToProceedDate;
        private decimal contractAmount;

        public Contract() 
            : this(null)
        {
        }

        public Contract(object key)
            : base(key)
        {
            this.contractor = new Company();
            this.scopeOfWork = string.Empty;
            this.bidPackageNumber = string.Empty;
            this.contractAmount = 0;
        }

        public Company Contractor
        {
            get { return this.contractor; }
            set { this.contractor = value; }
        }

        public string ScopeOfWork
        {
            get { return this.scopeOfWork; }
            set { this.scopeOfWork = value; }
        }

        public string BidPackageNumber
        {
            get { return this.bidPackageNumber; }
            set { this.bidPackageNumber = value; }
        }

        public DateTime? ContractDate
        {
            get { return this.contractDate; }
            set { this.contractDate = value; }
        }

        public DateTime? NoticeToProceedDate
        {
            get { return this.noticeToProceedDate; }
            set { this.noticeToProceedDate = value; }
        }

        public decimal ContractAmount
        {
            get { return this.contractAmount; }
            set { this.contractAmount = value; }
        }

        protected override void Validate()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override BrokenRuleMessages GetBrokenRuleMessages()
        {
            return new ContractRuleMessages();
        }
    }
}
