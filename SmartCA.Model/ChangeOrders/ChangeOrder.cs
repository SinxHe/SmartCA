using System;
using SmartCA.Model.Companies;
using SmartCA.Infrastructure.DomainBase;
using System.Collections.Generic;
using SmartCA.Model.Transmittals;
using System.Text;
using SmartCA.Model.Projects;
using SmartCA.Model.Description;
using SmartCA.Model.NumberedProjectChildren;

namespace SmartCA.Model.ChangeOrders
{
    public class ChangeOrder
        : EntityBase, IAggregateRoot, INumberedProjectChild, 
        IDescribable, IHasRoutingItems
    {
        #region Private Fields

        private object projectKey;
        private int number;
        private DateTime effectiveDate;
        private Company contractor;
        private string description;
        private Project currentProject;
        private PriceChangeType? changeType;
        private ChangeDirection priceChangeDirection;
        private decimal? previousAuthorizedAmount;
        private decimal amountChanged;
        private ChangeDirection timeChangeDirection;
        private int? previousTimeChangedTotal;
        private int timeChanged;
        private List<RoutingItem> routingItems;
        private ItemStatus status;
        private DateTime? agencyApprovedDate;
        private DateTime? dateToField;
        private DateTime? ownerSignatureDate;
        private DateTime? architectSignatureDate;
        private DateTime? contractorSignatureDate;
        private NumberSpecification<ChangeOrder> numberSpecification;
        private DescriptionSpecification<ChangeOrder> descriptionSpecification;
        
        #endregion

        #region Constructors

        public ChangeOrder(object projectKey, int number)
            : this(null, projectKey, number)
        {
        }

        public ChangeOrder(object key, object projectKey, 
            int number) : base(key)
        {
            this.projectKey = projectKey;
            this.number = number;
            this.effectiveDate = DateTime.Now;
            this.contractor = null;
            this.description = string.Empty;
            this.changeType = null;
            this.priceChangeDirection = ChangeDirection.Unchanged;
            this.previousAuthorizedAmount = null;
            this.previousTimeChangedTotal = null;
            this.amountChanged = 0;
            this.timeChangeDirection = ChangeDirection.Unchanged;
            this.timeChanged = 0;
            this.routingItems = new List<RoutingItem>();
            this.status = null;
            this.agencyApprovedDate = null;
            this.dateToField = null;
            this.ownerSignatureDate = null;
            this.architectSignatureDate = null;
            this.contractorSignatureDate = null;
            this.numberSpecification =
                new NumberSpecification<ChangeOrder>();
            this.descriptionSpecification = new DescriptionSpecification<ChangeOrder>();
            this.ValidateInitialization();
        }

        #endregion

        #region Public Properties

        public object ProjectKey
        {
            get { return this.projectKey; }
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

        public Company Contractor
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
            get
            {
                this.GetCurrentProject();
                return this.currentProject.OriginalConstructionCost;
            }
        }

        public decimal NewConstructionCost
        {
            get 
            {
                this.GetPreviousAuthorizedAmount();
                return this.OriginalConstructionCost + 
                    this.PreviousAuthorizedAmount + 
                    this.amountChanged;
            }
        }

        public PriceChangeType? ChangeType
        {
            get { return this.changeType; }
            set { this.changeType = value; }
        }

        public ChangeDirection PriceChangeDirection
        {
            get { return this.priceChangeDirection; }
            set { this.priceChangeDirection = value; }
        }

        public int PreviousTimeChangedTotal
        {
            get 
            {
                this.GetPreviousTimeChangedTotal();
                return this.previousTimeChangedTotal.HasValue ? 
                    this.previousTimeChangedTotal.Value : 0;
            }
        }

        public decimal PreviousAuthorizedAmount
        {
            get 
            {
                this.GetPreviousAuthorizedAmount();
                return this.previousAuthorizedAmount.HasValue ? 
                    this.previousAuthorizedAmount.Value : 0;
            }
        }

        public decimal AmountChanged
        {
            get { return this.amountChanged; }
            set { this.amountChanged = value; }
        }

        public ChangeDirection TimeChangeDirection
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
            get
            {
                DateTime? completionDate = null;
                this.GetCurrentProject();
                if (this.currentProject.EstimatedCompletionDate.HasValue)
                {
                    this.GetPreviousTimeChangedTotal();
                    completionDate = 
                        this.currentProject.EstimatedCompletionDate.Value.AddDays(
                        this.PreviousTimeChangedTotal + this.timeChanged);
                }
                return completionDate;
            }
        }

        public IList<RoutingItem> RoutingItems
        {
            get { return this.routingItems; }
        }

        public ItemStatus Status
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

        public NumberSpecification<ChangeOrder> NumberSpecification
        {
            get { return this.numberSpecification; }
        }

        public DescriptionSpecification<ChangeOrder> DescriptionSpecification
        {
            get { return this.descriptionSpecification; }
        }

        #endregion

        #region Methods

        private void ValidateInitialization()
        {
            NumberedProjectChildValidator.ValidateInitialState(this, 
                "Change Order");
        }

        private void GetCurrentProject()
        {
            if (this.currentProject == null)
            {
                this.currentProject = ProjectService.GetProject(this.projectKey);
            }
        }

        private void GetPreviousAuthorizedAmount()
        {
            if (!this.previousAuthorizedAmount.HasValue)
            {
                this.previousAuthorizedAmount = 
                    ChangeOrderService.GetPreviousAuthorizedAmountFrom(this);
            }
        }

        private void GetPreviousTimeChangedTotal()
        {
            if (!this.previousTimeChangedTotal.HasValue)
            {
                this.previousTimeChangedTotal = 
                    ChangeOrderService.GetPreviousTimeChangedTotalFrom(this);
            }
        }

        protected override void Validate()
        {
            if (!this.numberSpecification.IsSatisfiedBy(this))
            {
                this.AddBrokenRule(
                    NumberedProjectChildrenRuleMessageKeys.InvalidNumber);
            }
            if (!(this.descriptionSpecification.IsSatisfiedBy(this)))
            {
                this.AddBrokenRule(
                    DescriptionRuleMessageKeys.InvalidDescription);
            }
            if (this.status == null)
            {
                this.AddBrokenRule(
                    ChangeOrderRuleMessages.MessageKeys.InvalidStatus);
            }
            if (this.contractor == null)
            {
                this.AddBrokenRule(
                    ChangeOrderRuleMessages.MessageKeys.InvalidContractor);
            }
        }

        protected override BrokenRuleMessages GetBrokenRuleMessages()
        {
            return new ChangeOrderRuleMessages();
        }

        #endregion
    }
}
