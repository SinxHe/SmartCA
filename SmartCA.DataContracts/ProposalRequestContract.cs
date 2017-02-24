using System;
using System.Collections.Generic;

namespace SmartCA.DataContracts
{
    [Serializable]
    public class ProposalRequestContract : ContractBase
    {
        private object projectKey;
        private DateTime transmittalDate;
        private int totalPages;
        private DeliveryContract deliveryMethod;
        private string otherDeliveryMethod;
        private string phaseNumber;
        private bool reimbursable;
        private bool final;
        private List<CopyToContract> copyToList;
        private string transmittalRemarks;
        private int number;
        private ProjectContactContract to;
        private EmployeeContract from;
        private DateTime? issueDate;
        private CompanyContract contractor;
        private string description;
        private string attachment;
        private string reason;
        private string initiator;
        private int cause;
        private int origin;
        private string remarks;

        public ProposalRequestContract()
        {
            this.projectKey = null;
            this.transmittalDate = DateTime.MinValue;
            this.totalPages = 0;
            this.deliveryMethod = DeliveryContract.Hand;
            this.otherDeliveryMethod = string.Empty;
            this.phaseNumber = string.Empty;
            this.reimbursable = false;
            this.final = false;
            this.copyToList = new List<CopyToContract>();
            this.transmittalRemarks = string.Empty;
            this.number = 0;
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
        }

        public object ProjectKey
        {
            get { return this.projectKey; }
            set { this.projectKey = value; }
        }

        public DateTime TransmittalDate
        {
            get { return this.transmittalDate; }
            set { this.transmittalDate = value; }
        }

        public ProjectContactContract To
        {
            get { return this.to; }
            set { this.to = value; }
        }

        public EmployeeContract From
        {
            get { return this.from; }
            set { this.from = value; }
        }

        public int TotalPages
        {
            get { return this.totalPages; }
            set { this.totalPages = value; }
        }

        public DeliveryContract DeliveryMethod
        {
            get { return this.deliveryMethod; }
            set { this.deliveryMethod = value; }
        }

        public string OtherDeliveryMethod
        {
            get { return this.otherDeliveryMethod; }
            set { this.otherDeliveryMethod = value; }
        }

        public string PhaseNumber
        {
            get { return this.phaseNumber; }
            set { this.phaseNumber = value; }
        }

        public bool Reimbursable
        {
            get { return this.reimbursable; }
            set { this.reimbursable = value; }
        }

        public bool Final
        {
            get { return this.final; }
            set { this.final = value; }
        }

        public IList<CopyToContract> CopyToList
        {
            get { return this.copyToList; }
        }

        public string Remarks
        {
            get { return this.remarks; }
            set { this.remarks = value; }
        }

        public string TransmittalRemarks
        {
            get { return this.transmittalRemarks; }
            set { this.transmittalRemarks = value; }
        }

        public int Number
        {
            get { return this.number; }
            set { this.number = value; }
        }

        public DateTime? IssueDate
        {
            get { return this.issueDate; }
            set { this.issueDate = value; }
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
            set { this.cause = value; }
        }

        public int Origin
        {
            get { return this.origin; }
            set { this.origin = value; }
        }
    }
}
