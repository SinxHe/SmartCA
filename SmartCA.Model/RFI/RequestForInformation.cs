using System;
using SmartCA.Infrastructure.DomainBase;
using SmartCA.Model.Submittals;
using SmartCA.Model.Employees;
using SmartCA.Model.Projects;
using System.Collections.Generic;
using System.Text;
using SmartCA.Model.Companies;
using SmartCA.Model.Transmittals;
using SmartCA.Model.Description;
using SmartCA.Model.NumberedProjectChildren;

namespace SmartCA.Model.RFI
{
    public class RequestForInformation 
        : RoutableTransmittal, IAggregateRoot, INumberedProjectChild, IDescribable
    {
        private int number;
        private ProjectContact from;
        private DateTime? dateReceived;
        private DateTime? dateRequestedBy;
        private Company contractor;
        private SpecificationSection specSection;
        private string question;
        private string description;
        private string contractorProposedSolution;
        private bool change;
        private int cause;
        private int origin;
        private ItemStatus status;
        private DateTime? dateToField;
        private string shortAnswer;
        private string longAnswer;
        private string remarks;
        private NumberSpecification<RequestForInformation> numberSpecification;
        private RequestForInformationDateSpecification dateToFieldSpecification;
        private RequestForInformationQuestionAnswerSpecification questionAnswerSpecification;

        public RequestForInformation(object projectKey, int number)
            : this(null, projectKey, number)
        {
        }

        public RequestForInformation(object key, object projectKey, 
            int number) : base(key, projectKey)
        {
            this.number = number;
            this.from = null;
            this.dateReceived = null;
            this.dateRequestedBy = null;
            this.contractor = null;
            this.specSection = null;
            this.question = string.Empty;
            this.description = string.Empty;
            this.contractorProposedSolution = string.Empty;
            this.change = false;
            this.cause = 0;
            this.origin = 0;
            this.status = null;
            this.dateToField = null;
            this.shortAnswer = string.Empty;
            this.longAnswer = string.Empty;
            this.remarks = string.Empty;
            this.numberSpecification = new NumberSpecification<RequestForInformation>();
            this.dateToFieldSpecification = new RequestForInformationDateSpecification();
            this.questionAnswerSpecification = new RequestForInformationQuestionAnswerSpecification();
            this.ValidateInitialization();
        }

        public int Number
        {
            get { return this.number; }
            set { this.number = value; }
        }

        public ProjectContact From
        {
            get { return this.from; }
            set { this.from = value; }
        }

        public DateTime? DateReceived
        {
            get { return this.dateReceived; }
            set { this.dateReceived = value; }
        }

        public DateTime? DateRequestedBy
        {
            get { return this.dateRequestedBy; }
            set { this.dateRequestedBy = value; }
        }

        public int DaysLapsed
        {
            get
            {
                int daysLapsed = 0;
                if (this.dateReceived.HasValue && 
                    this.dateToField.HasValue)
                {
                    daysLapsed = this.dateToField.Value.Subtract(this.dateRequestedBy.Value).Days;
                }
                return daysLapsed;
            }
        }

        public Company Contractor
        {
            get { return this.contractor; }
            set { this.contractor = value; }
        }

        public SpecificationSection SpecSection
        {
            get { return this.specSection; }
            set { this.specSection = value; }
        }

        public string Question
        {
            get { return this.question; }
            set { this.question = value; }
        }

        public string Description
        {
            get { return this.description; }
            set { this.description = value; }
        }

        public string ContractorProposedSolution
        {
            get { return this.contractorProposedSolution; }
            set { this.contractorProposedSolution = value; }
        }

        public bool Change
        {
            get { return this.change; }
            set { this.change = value; }
        }

        public int Cause
        {
            get { return this.cause; }
            set { this.cause = value; }
        }

        public int Origin
        {
            get { return this.origin; }
            set { this.origin = value; }
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

        public string ShortAnswer
        {
            get { return this.shortAnswer; }
            set { this.shortAnswer = value; }
        }

        public string LongAnswer
        {
            get { return this.longAnswer; }
            set { this.longAnswer = value; }
        }

        public string Remarks
        {
            get { return this.remarks; }
            set { this.remarks = value; }
        }

        public NumberSpecification<RequestForInformation> NumberSpecification
        {
            get { return this.numberSpecification; }
        }

        public RequestForInformationDateSpecification DateToFieldSpecification
        {
            get { return this.dateToFieldSpecification; }
        }

        public RequestForInformationQuestionAnswerSpecification QuestionAnswerSpecification
        {
            get { return this.questionAnswerSpecification; }
        }

        public override string ToString()
        {
            return this.number.ToString();
        }

        private void ValidateInitialization()
        {
            NumberedProjectChildValidator.ValidateInitialState(this, "RFI");
        }

        protected override void Validate()
        {
            if (!this.dateToFieldSpecification.IsSatisfiedBy(this))
            {
                this.BrokenRules.Add(new BrokenRule("", ""));
            }
            if (!this.numberSpecification.IsSatisfiedBy(this))
            {
                this.BrokenRules.Add(new BrokenRule("", ""));
            }
            if (!this.questionAnswerSpecification.IsSatisfiedBy(this))
            {
                this.BrokenRules.Add(new BrokenRule("", ""));
            }
        }

        protected override BrokenRuleMessages GetBrokenRuleMessages()
        {
            return new RequestForInformationRuleMessages();
        }
    }
}
