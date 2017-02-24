using System;
using System.Collections.Generic;

namespace SmartCA.DataContracts
{
    [Serializable]
    public class ChangeOrderContract : ContractBase
    {
        private object projectKey;
        private int number;
        private DateTime effectiveDate;
        private CompanyContract contractor;
        private string description;
        private decimal originalConstructionCost;
        private decimal newConstructionCost;
        private ProjectContract currentProject;
        private PriceChangeTypeContract? changeType;
        private ChangeDirectionContract changeTypeDirection;
        private decimal amountChanged;
        private ChangeDirectionContract timeChangeDirection;
        private int? previousTimeChangedTotal;
        private decimal? previousAuthorizedChangeOrderAmount;
        private int timeChanged;
        private DateTime? dateOfSubstantialCompletion;
        private List<RoutingItemContract> routingItems;
        private ItemStatusContract status;
        private DateTime? agencyApprovedDate;
        private DateTime? dateToField;
        private DateTime? ownerSignatureDate;
        private DateTime? architectSignatureDate;
        private DateTime? contractorSignatureDate;
        
        public ChangeOrderContract()
        {
            this.projectKey = null;
            this.number = 0;
            this.effectiveDate = DateTime.Now;
            this.contractor = null;
            this.description = string.Empty;
            this.originalConstructionCost = 0;
            this.newConstructionCost = 0;
            this.currentProject = null;
            this.changeType = null;
            this.changeTypeDirection = ChangeDirectionContract.Unchanged;
            this.previousTimeChangedTotal = null;
            this.previousAuthorizedChangeOrderAmount = null;
            this.amountChanged = 0;
            this.timeChangeDirection = ChangeDirectionContract.Unchanged;
            this.timeChanged = 0;
            this.dateOfSubstantialCompletion = null;
            this.routingItems = new List<RoutingItemContract>();
            this.status = null;
            this.agencyApprovedDate = null;
            this.dateToField = null;
            this.ownerSignatureDate = null;
            this.architectSignatureDate = null;
            this.contractorSignatureDate = null;
        }

        public object ProjectKey
        {
            get { return this.projectKey; }
            set { this.projectKey = value; }
        }

        public int Number
        {
            get { return this.number; }
            set { this.number = value; }
        }

        public DateTime EffectiveDate
        {
            get { return this.effectiveDate; }
            set { this.effectiveDate = value; }
        }

        public CompanyContract Contractor
        {
            get { return this.contractor; }
            set { this.contractor = value; }
        }

        public string Description
        {
            get { return this.description; }
            set { this.description = value; }
        }

        public decimal OriginalConstructionCost
        {
            get { return this.originalConstructionCost; }
            set { this.originalConstructionCost = value; }
        }

        public decimal NewConstructionCost
        {
            get { return this.newConstructionCost; }
            set { this.newConstructionCost = value; }
        }

        public ProjectContract CurrentProject
        {
            get { return this.currentProject; }
            set { this.currentProject = value; }
        }

        public PriceChangeTypeContract? ChangeType
        {
            get { return this.changeType; }
            set { this.changeType = value; }
        }

        public ChangeDirectionContract ChangeTypeDirection
        {
            get { return this.changeTypeDirection; }
            set { this.changeTypeDirection = value; }
        }

        public int? PreviousTimeChangedTotal
        {
            get { return this.previousTimeChangedTotal; }
            set { this.previousTimeChangedTotal = value; }
        }

        public decimal? PreviousAuthorizedChangeOrderAmount
        {
            get { return this.previousAuthorizedChangeOrderAmount; }
            set { this.previousAuthorizedChangeOrderAmount = value; }
        }

        public decimal AmountChanged
        {
            get { return this.amountChanged; }
            set { this.amountChanged = value; }
        }

        public ChangeDirectionContract TimeChangeDirection
        {
            get { return this.timeChangeDirection; }
            set { this.timeChangeDirection = value; }
        }

        public int TimeChanged
        {
            get { return this.timeChanged; }
            set { this.timeChanged = value; }
        }

        public DateTime? DateOfSubstantialCompletion
        {
            get { return this.dateOfSubstantialCompletion; }
            set { this.dateOfSubstantialCompletion = value; }
        }

        public IList<RoutingItemContract> RoutingItems
        {
            get { return this.routingItems; }
        }

        public ItemStatusContract Status
        {
            get { return this.status; }
            set { this.status = value; }
        }

        public DateTime? AgencyApprovedDate
        {
            get { return this.agencyApprovedDate; }
            set { this.agencyApprovedDate = value; }
        }

        public DateTime? DateToField
        {
            get { return this.dateToField; }
            set { this.dateToField = value; }
        }

        public DateTime? OwnerSignatureDate
        {
            get { return this.ownerSignatureDate; }
            set { this.ownerSignatureDate = value; }
        }

        public DateTime? ArchitectSignatureDate
        {
            get { return this.architectSignatureDate; }
            set { this.architectSignatureDate = value; }
        }

        public DateTime? ContractorSignatureDate
        {
            get { return this.contractorSignatureDate; }
            set { this.contractorSignatureDate = value; }
        }
    }
}
