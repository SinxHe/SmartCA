using System;
using SmartCA.Infrastructure.DomainBase;
using SmartCA.Model.Projects;
using SmartCA.Model.Employees;
using System.Collections.Generic;
using System.Text;
using SmartCA.Model.Transmittals;

namespace SmartCA.Model.Submittals
{
    public class Submittal : RoutableTransmittal, IAggregateRoot
    {
        private SpecificationSection specSection;
        private string specSectionPrimaryIndex;
        private string specSectionSecondaryIndex;
        private ProjectContact to;
        private Employee from;
        private DateTime? dateReceived;
        private string contractNumber;
        private List<TrackingItem> trackingItems;
        private string remarks;
        private ActionStatus action;
        private ItemStatus status;
        private DateTime? dateToField;
        private SubmittalRemainderLocation remainderLocation;
        private string remainderUnderSubmittalNumber;
        private string otherRemainderLocation;

        public Submittal(SpecificationSection specSection, object projectKey)
            : this(null, specSection, projectKey)
        {
        }

        public Submittal(object key, SpecificationSection specSection, 
            object projectKey) : base(key, projectKey)
        {
            this.specSection = specSection;
            this.specSectionPrimaryIndex = "01";
            this.specSectionSecondaryIndex = "00";
            this.to = null;
            this.from = null;
            this.dateReceived = null;
            this.contractNumber = string.Empty;
            this.trackingItems = new List<TrackingItem>();
            this.remarks = string.Empty;
            this.action = ActionStatus.NoExceptionTaken;
            this.status = null;
            this.dateToField = null;
            this.remainderLocation = SubmittalRemainderLocation.None;
            this.remainderUnderSubmittalNumber = string.Empty;
            this.otherRemainderLocation = string.Empty;
            this.ValidateInitialization();
        }

        public SpecificationSection SpecSection
        {
            get { return this.specSection; }
            set { this.specSection = value; }
        }

        public string SpecSectionPrimaryIndex
        {
            get { return this.specSectionPrimaryIndex; }
            set { this.specSectionPrimaryIndex = value; }
        }

        public string SpecSectionSecondaryIndex
        {
            get { return this.specSectionSecondaryIndex; }
            set { this.specSectionSecondaryIndex = value; }
        }

        public string Number
        {
            get
            {
                return string.Format("{0}.{1}.{2}",
                    this.specSection.Number, this.specSectionPrimaryIndex,
                    this.specSectionSecondaryIndex);
            }
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

        public DateTime? DateReceived
        {
            get { return this.dateReceived; }
            set { this.dateReceived = value; }
        }

        public string ContractNumber
        {
            get { return this.contractNumber; }
            set { this.contractNumber = value; }
        }

        public IList<TrackingItem> TrackingItems
        {
            get { return this.trackingItems; }
        }

        public string Remarks
        {
            get { return this.remarks; }
            set { this.remarks = value; }
        }

        public ActionStatus Action
        {
            get { return this.action; }
            set { this.action = value; }
        }

        public ItemStatus Status
        {
            get { return this.status; }
            set { this.status = value; }
        }

        public DateTime? DateToField
        {
            get { return this.dateToField; }
            set { this.dateToField = value; }
        }

        public SubmittalRemainderLocation RemainderLocation
        {
            get { return this.remainderLocation; }
            set { this.remainderLocation = value; }
        }

        public string RemainderUnderSubmittalNumber
        {
            get { return this.remainderUnderSubmittalNumber; }
            set { this.remainderUnderSubmittalNumber = value; }
        }

        public string OtherRemainderLocation
        {
            get { return this.otherRemainderLocation; }
            set { this.otherRemainderLocation = value; }
        }

        public override string ToString()
        {
            return this.Number;
        }

        private void ValidateInitialization()
        {
            if (this.Key == null && 
                (this.specSection == null || this.ProjectKey == null))
            {
                StringBuilder builder = new StringBuilder(100);
                builder.Append("Invalid Submittal.  ");
                builder.Append("The Submittal must have a valid Specification Section ");
                builder.Append("and be associated with a Project.");
                throw new InvalidOperationException(builder.ToString());
            }
        }

        protected override void Validate()
        {
            //throw new Exception("The method or operation is not implemented.");
        }

        protected override BrokenRuleMessages GetBrokenRuleMessages()
        {
            return new SubmittalRuleMessages();
        }
    }
}