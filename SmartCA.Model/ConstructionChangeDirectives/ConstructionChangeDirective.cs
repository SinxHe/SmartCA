using System;
using SmartCA.Model.Transmittals;
using SmartCA.Infrastructure.DomainBase;
using SmartCA.Model.Companies;
using SmartCA.Model.Employees;
using SmartCA.Model.Projects;
using SmartCA.Model.ChangeOrders;
using SmartCA.Model.Description;
using SmartCA.Model.NumberedProjectChildren;

namespace SmartCA.Model.ConstructionChangeDirectives
{
    public class ConstructionChangeDirective 
        : Transmittal, IAggregateRoot, INumberedProjectChild, IDescribable
    {
        private int number;
        private ProjectContact to;
        private Employee from;
        private DateTime? issueDate;
        private Company contractor;
        private string description;
        private string attachment;
        private string reason;
        private string initiator;
        private int cause;
        private int origin;
        private string remarks;
        private PriceChangeType? changeType;
        private ChangeDirection priceChangeDirection;
        private decimal amountChanged;
        private ChangeDirection timeChangeDirection;
        private int timeChanged;
        private DateTime? ownerSignatureDate;
        private DateTime? architectSignatureDate;
        private DateTime? contractorSignatureDate;
        private NumberSpecification<ConstructionChangeDirective> numberSpecification;
        private DescriptionSpecification<ConstructionChangeDirective> descriptionSpecification;
        private object changeOrderKey;
        private BrokenRuleMessages brokenRuleMessages;

        public ConstructionChangeDirective(object projectKey, int number)
            : this(null, projectKey, number)
        {
        }

        public ConstructionChangeDirective(object key, object projectKey, 
            int number) : base(key, projectKey)
        {
            this.number = number;
            this.to = null;
            this.from = null;
            this.issueDate = null;
            this.contractor = null;
            this.description = string.Empty;
            this.attachment = string.Empty;
            this.reason = string.Empty;
            this.initiator = string.Empty;
            this.cause = 0;
            this.origin = 0;
            this.remarks = string.Empty;
            this.changeType = null;
            this.priceChangeDirection = ChangeDirection.Unchanged;
            this.amountChanged = 0;
            this.timeChangeDirection = ChangeDirection.Unchanged;
            this.timeChanged = 0;
            this.ownerSignatureDate = null;
            this.architectSignatureDate = null;
            this.contractorSignatureDate = null;
            this.numberSpecification =
                new NumberSpecification<ConstructionChangeDirective>();
            this.descriptionSpecification = 
                new DescriptionSpecification<ConstructionChangeDirective>();
            this.changeOrderKey = null;
            this.ValidateInitialization();
            this.brokenRuleMessages = 
                new ConstructionChangeDirectiveRuleMessages();
        }

        public int Number
        {
            get { return this.number; }
            set { this.number = value; }
        }

        public ProjectContact To
        {
            get { return this.to; }
            set { this.to = value; }
        }

        public Employee From
        {
            get { return this.from; }
            set { this.from = value; }
        }

        public DateTime? IssueDate
        {
            get { return this.issueDate; }
            set { this.issueDate = value; }
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

        public string Attachment
        {
            get { return this.attachment; }
            set { this.attachment = value; }
        }

        public string Reason
        {
            get { return this.reason; }
            set { this.reason = value; }
        }

        public string Initiator
        {
            get { return this.initiator; }
            set { this.initiator = value; }
        }

        public int Cause
        {
            get { return this.cause; }
            set
            {
                // Cause must be a positive number
                if (value > 0)
                {
                    this.cause = value;
                }
            }
        }

        public int Origin
        {
            get { return this.origin; }
            set
            {
                // Origin must be a positive number
                if (value > 0)
                {
                    this.origin = value;
                }
            }
        }

        public string Remarks
        {
            get { return this.remarks; }
            set { this.remarks = value; }
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

        public object ChangeOrderKey
        {
            get { return this.changeOrderKey; }
            set { this.changeOrderKey = value; }
        }

        public NumberSpecification<ConstructionChangeDirective> NumberSpecification
        {
            get { return this.numberSpecification; }
        }

        public DescriptionSpecification<ConstructionChangeDirective> DescriptionSpecification
        {
            get { return this.descriptionSpecification; }
        }

        public bool HasBeenTransformedToChangeOrder
        {
            get { return (this.changeOrderKey != null); }
        }

        private void ValidateInitialization()
        {
            //if (child.Key == null &&
            //    (child.Number < 1 || child.ProjectKey == null))
            //{
            //    StringBuilder builder = new StringBuilder(100);
            //    builder.Append(string.Format("Invalid {0}.  ",
            //        entityFriendlyName));
            //    builder.Append(string.Format("The {0} must have ",
            //        entityFriendlyName));
            //    builder.Append(string.Format("a valid {0} number ",
            //        entityFriendlyName));
            //    builder.Append("and be associated with a Project.");
            //    throw new InvalidOperationException(builder.ToString());
            //}
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
            if (this.contractor == null)
            {
                this.AddBrokenRule(
                    ConstructionChangeDirectiveRuleMessages.MessageKeys.InvalidContractor);
            }
        }

        protected override BrokenRuleMessages GetBrokenRuleMessages()
        {
            return new ConstructionChangeDirectiveRuleMessages();
        }

        public ChangeOrder TransformToChangeOrder()
        {
            ChangeOrder co = null;

            // See if it has already been transformed into a 
            // Change Order...it can only be changed once!
            if (!this.HasBeenTransformedToChangeOrder)
            {
                Project project = ProjectService.GetProject(this.ProjectKey);
                co = NumberedProjectChildFactory.CreateNumberedProjectChild
                    <ChangeOrder>(project);
                co.AmountChanged = this.amountChanged;
                co.ArchitectSignatureDate = this.architectSignatureDate;
                co.ChangeType = this.changeType;
                co.Contractor = this.contractor;
                co.ContractorSignatureDate = this.contractorSignatureDate;
                co.Description = this.description;
                co.OwnerSignatureDate = this.ownerSignatureDate;
                co.PriceChangeDirection = this.priceChangeDirection;
                co.TimeChanged = this.timeChanged;
                co.TimeChangeDirection = this.timeChangeDirection;
            }
            else
            {
                // It was already changed, so get the Change Order that it was 
                // changed into
                co = ChangeOrderService.GetChangeOrder(this.changeOrderKey);
            }

            // Get the key of the Change Order
            this.changeOrderKey = co.Key;

            // Return the instance
            return co;
        }
    }
}
