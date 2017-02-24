using System;
using System.Collections.Generic;

namespace SmartCA.DataContracts
{
    [Serializable]
    public class ProjectContract : ContractBase
    {
        private string number;
        private string name;
        private AddressContract address;
        private CompanyContract owner;
        private EmployeeContract constructionAdministrator;
        private EmployeeContract principalInCharge;
        private DateTime? contractDate;
        private DateTime? estimatedStartDate;
        private DateTime? estimatedCompletionDate;
        private DateTime? adjustedCompletionDate;
        private DateTime? currentCompletionDate;
        private DateTime? actualCompletionDate;
        private decimal contingencyAllowanceAmount;
        private decimal testingAllowanceAmount;
        private decimal utilityAllowanceAmount;
        private decimal originalConstructionCost;
        private int totalChangeOrderDays;
        private decimal adjustedConstructionCost;
        private decimal totalChangeOrdersAmount;
        private int totalSquareFeet;
        private int percentComplete;
        private string remarks;
        private decimal aeChangeOrderAmount;
        private string contractReason;
        private string agencyApplicationNumber;
        private string agencyFileNumber;
        private MarketSegmentContract segment;
        private List<AllowanceContract> allowances;
        private List<ProjectContractContract> contracts;
        private List<ProjectContactContract> contacts;

        public ProjectContract()
        {
            this.allowances = new List<AllowanceContract>();
            this.contracts = new List<ProjectContractContract>();
            this.contacts = new List<ProjectContactContract>();
        }

        public string Number
        {
            get { return this.number; }
            set { this.number = value; }
        }

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public AddressContract Address
        {
            get { return this.address; }
            set { this.address = value; }
        }

        public CompanyContract Owner
        {
            get { return this.owner; }
            set { this.owner = value; }
        }

        public EmployeeContract ConstructionAdministrator
        {
            get { return this.constructionAdministrator; }
            set { this.constructionAdministrator = value; }
        }

        public EmployeeContract PrincipalInCharge
        {
            get { return this.principalInCharge; }
            set { this.principalInCharge = value; }
        }

        public DateTime? ContractDate
        {
            get { return this.contractDate; }
            set { this.contractDate = value; }
        }

        public DateTime? EstimatedStartDate
        {
            get { return this.estimatedStartDate; }
            set { this.estimatedStartDate = value; }
        }

        public DateTime? EstimatedCompletionDate
        {
            get { return this.estimatedCompletionDate; }
            set { this.estimatedCompletionDate = value; }
        }

        public DateTime? AdjustedCompletionDate
        {
            get { return this.adjustedCompletionDate; }
            set { this.adjustedCompletionDate = value; }
        }

        public DateTime? CurrentCompletionDate
        {
            get { return this.currentCompletionDate; }
            set { this.currentCompletionDate = value; }
        }

        public DateTime? ActualCompletionDate
        {
            get { return this.actualCompletionDate; }
            set { this.actualCompletionDate = value; }
        }

        public decimal ContingencyAllowanceAmount
        {
            get { return this.contingencyAllowanceAmount; }
            set { this.contingencyAllowanceAmount = value; }
        }

        public decimal TestingAllowanceAmount
        {
            get { return this.testingAllowanceAmount; }
            set { this.testingAllowanceAmount = value; }
        }

        public decimal UtilityAllowanceAmount
        {
            get { return this.utilityAllowanceAmount; }
            set { this.utilityAllowanceAmount = value; }
        }

        public decimal OriginalConstructionCost
        {
            get { return this.originalConstructionCost; }
            set { this.originalConstructionCost = value; }
        }

        public int TotalChangeOrderDays
        {
            get { return this.totalChangeOrderDays; }
            set { this.totalChangeOrderDays = value; }
        }

        public decimal AdjustedConstructionCost
        {
            get { return this.adjustedConstructionCost; }
            set { this.adjustedConstructionCost = value; }
        }

        public decimal TotalChangeOrdersAmount
        {
            get { return this.totalChangeOrdersAmount; }
            set { this.totalChangeOrdersAmount = value; }
        }

        public int TotalSquareFeet
        {
            get { return this.totalSquareFeet; }
            set { this.totalSquareFeet = value; }
        }

        public int PercentComplete
        {
            get { return this.percentComplete; }
            set { this.percentComplete = value; }
        }

        public string Remarks
        {
            get { return this.remarks; }
            set { this.remarks = value; }
        }

        public decimal AeChangeOrderAmount
        {
            get { return this.aeChangeOrderAmount; }
            set { this.aeChangeOrderAmount = value; }
        }

        public string ContractReason
        {
            get { return this.contractReason; }
            set { this.contractReason = value; }
        }

        public string AgencyApplicationNumber
        {
            get { return this.agencyApplicationNumber; }
            set { this.agencyApplicationNumber = value; }
        }

        public string AgencyFileNumber
        {
            get { return this.agencyFileNumber; }
            set { this.agencyFileNumber = value; }
        }

        public MarketSegmentContract Segment
        {
            get { return this.segment; }
            set { this.segment = value; }
        }

        public IList<AllowanceContract> Allowances
        {
            get { return this.allowances; }
        }

        public IList<ProjectContractContract> Contracts
        {
            get { return this.contracts; }
        }

        public IList<ProjectContactContract> Contacts
        {
            get { return this.contacts; }
        }
    }
}
