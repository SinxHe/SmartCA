using System;
using System.Collections.Generic;
using SmartCA.Infrastructure.DomainBase;
using SmartCA.Model.Addresses;
using SmartCA.Model.Companies;
using SmartCA.Model.Employees;

namespace SmartCA.Model.Projects
{
    public class Project : EntityBase, IAggregateRoot
    {
        #region Private Fields

        private string number;
        private string name;
        private Address address;
        private Company owner;
        private Employee constructionAdministrator;
        private Employee principalInCharge;
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
        private MarketSegment segment;
        private List<Allowance> allowances;
        private List<Contract> contracts;
        private List<ProjectContact> contacts;

        #endregion

        #region Constructors

        public Project(string number, string name)
            : this(null, number, name)
        {
        }

        public Project(object key, string number, string name) 
            : base(key)
        {
            this.number = number;
            this.name = name;
            this.address = null;
            this.owner = new Company();
            this.constructionAdministrator = null;
            this.principalInCharge = null;
            this.contractDate = null;
            this.estimatedStartDate = null;
            this.estimatedCompletionDate = null;
            this.adjustedCompletionDate = null;
            this.currentCompletionDate = null;
            this.actualCompletionDate = null;
            this.contingencyAllowanceAmount = 0;
            this.testingAllowanceAmount = 0;
            this.utilityAllowanceAmount = 0;
            this.originalConstructionCost = 0;
            this.totalChangeOrderDays = 0;
            this.adjustedConstructionCost = 0;
            this.totalChangeOrdersAmount = 0;
            this.totalSquareFeet = 0;
            this.percentComplete = 0;
            this.remarks = string.Empty;
            this.aeChangeOrderAmount = 0;
            this.contractReason = string.Empty;
            this.agencyApplicationNumber = string.Empty;
            this.agencyFileNumber = string.Empty;
            this.segment = null;
            this.allowances = new List<Allowance>();
            this.contracts = new List<Contract>();
            this.contacts = new List<ProjectContact>();
        }

        #endregion

        #region Public Properties

        public string Number
        {
            get { return this.number; }
        }

        public string Name
        {
            get { return this.name; }
        }

        public Address Address
        {
            get { return this.address; }
            set { this.address = value; }
        }

        public Company Owner
        {
            get { return this.owner; }
            set { this.owner = value; }
        }

        public Employee ConstructionAdministrator
        {
            get { return this.constructionAdministrator; }
            set { this.constructionAdministrator = value; }
        }

        public Employee PrincipalInCharge
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
        }

        public decimal AdjustedConstructionCost
        {
            get { return this.adjustedConstructionCost; }
        }

        public decimal TotalChangeOrdersAmount
        {
            get { return this.totalChangeOrdersAmount; }
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

        public MarketSegment Segment
        {
            get { return this.segment; }
            set { this.segment = value; }
        }

        public IList<Allowance> Allowances
        {
            get { return this.allowances; }
        }

        public IList<Contract> Contracts
        {
            get { return this.contracts; }
        }

        public IList<ProjectContact> Contacts
        {
            get { return this.contacts; }
        }

        #endregion

        protected override void Validate()
        {
        }

        protected override BrokenRuleMessages GetBrokenRuleMessages()
        {
            return new ProjectRuleMessages();
        }
    }
}
