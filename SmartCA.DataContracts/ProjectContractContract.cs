using System;

namespace SmartCA.DataContracts
{
    [Serializable]
    public class ProjectContractContract : ContractBase
    {
        private CompanyContract contractor;
        private string scopeOfWork;
        private string bidPackageNumber;
        private DateTime? contractDate;
        private DateTime? noticeToProceedDate;
        private decimal contractAmount;

        public ProjectContractContract()
        {
            this.contractor = null;
            this.scopeOfWork = string.Empty;
            this.bidPackageNumber = string.Empty;
            this.contractDate = null;
            this.noticeToProceedDate = null;
            this.contractAmount = 0;
        }

        public CompanyContract Contractor
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
    }
}
