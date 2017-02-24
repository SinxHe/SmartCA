using System;
using SmartCA.Infrastructure.DomainBase;
using SmartCA.Model.Transmittals;
using SmartCA.Model.Projects;
using System.Collections.Generic;
using SmartCA.Model.Companies;
using SmartCA.Model.Submittals;
using SmartCA.Model.Employees;
using System.Text;
using System.Collections.ObjectModel;
using SmartCA.Model.Description;
using SmartCA.Model.NumberedProjectChildren;

namespace SmartCA.Model.ProposalRequests
{
    public class ProposalRequest 
        : Transmittal, IAggregateRoot, INumberedProjectChild, IDescribable
    {
        private int number;
        private ProjectContact to;
        private Employee from;
        private DateTime? issueDate;
        private DateTime expectedContractorReturnDate;
        private Company contractor;
        private string description;
        private string attachment;
        private string reason;
        private string initiator;
        private int cause;
        private int origin;
        private string remarks;
        private NumberSpecification<ProposalRequest> numberSpecification;
        private DescriptionSpecification<ProposalRequest> descriptionSpecification;
        private int expectedContractorReturnDays;
        private BrokenRuleMessages brokenRuleMessages;

        private const int DefaultExpectedContractorReturnDays = 7;
        
        public ProposalRequest(object projectKey, int number)
            : this(null, projectKey, number)
        {
        }

        public ProposalRequest(object key, object projectKey, 
            int number) : base(key, projectKey)
        {
            this.number = number;
            this.to = null;
            this.from = null;
            this.issueDate = null;
            this.GetExpectedContractorReturnDays();
            this.expectedContractorReturnDate = 
                this.TransmittalDate.AddDays(this.expectedContractorReturnDays);
            this.contractor = null;
            this.description = string.Empty;
            this.attachment = string.Empty;
            this.reason = string.Empty;
            this.initiator = string.Empty;
            this.cause = 0;
            this.origin = 0;
            this.remarks = string.Empty;
            this.numberSpecification = 
                new NumberSpecification<ProposalRequest>();
            this.descriptionSpecification =
                new DescriptionSpecification<ProposalRequest>();
            this.ValidateInitialization();
            this.brokenRuleMessages = new ProposalRequestRuleMessages();
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

        public DateTime ExpectedContractorReturnDate
        {
            get { return this.expectedContractorReturnDate; }
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

        public NumberSpecification<ProposalRequest> NumberSpecification
        {
            get { return this.numberSpecification; }
        }

        public DescriptionSpecification<ProposalRequest> DescriptionSpecification
        {
            get { return this.descriptionSpecification; }
        }

        private void ValidateInitialization()
        {
            NumberedProjectChildValidator.ValidateInitialState(this, "Proposal Request");
        }

        private void GetExpectedContractorReturnDays()
        {
            // First go with the default value
            this.expectedContractorReturnDays = 
                ProposalRequest.DefaultExpectedContractorReturnDays;

            // Now try to get the real value from the service
            int expectedContractorReturnDays = 
                ProposalRequestService.GetExpectedContractorReturnDays();

            // If the service returned a valid value, then use it instead 
            // of the default value
            if (expectedContractorReturnDays > 0)
            {
                this.expectedContractorReturnDays = 
                    expectedContractorReturnDays;
            }
        }

        protected override void Validate()
        {
            if (!this.numberSpecification.IsSatisfiedBy(this))
            {
                this.AddBrokenRule(
                    NumberedProjectChildrenRuleMessageKeys.InvalidNumber);
            }
            if (!this.descriptionSpecification.IsSatisfiedBy(this))
            {
                this.AddBrokenRule(
                    DescriptionRuleMessageKeys.InvalidDescription);
            }
            if (this.to == null)
            {
                this.AddBrokenRule(
                    ProposalRequestRuleMessages.MessageKeys.InvalidProjectContact);
            }
            if (this.from == null)
            {
                this.AddBrokenRule(
                    ProposalRequestRuleMessages.MessageKeys.InvalidEmployee);
            }
            if (this.contractor == null)
            {
                this.AddBrokenRule(
                    ProposalRequestRuleMessages.MessageKeys.InvalidContractor);
            }
        }

        protected override BrokenRuleMessages GetBrokenRuleMessages()
        {
            return new ProposalRequestRuleMessages();
        }
    }
}
