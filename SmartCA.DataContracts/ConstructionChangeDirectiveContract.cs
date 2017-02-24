using System;

namespace SmartCA.DataContracts
{
    [Serializable]
    public class ConstructionChangeDirectiveContract : TransmittalContract
    {
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
        private PriceChangeTypeContract? changeType;
        private ChangeDirectionContract priceChangeDirection;
        private decimal amountChanged;
        private ChangeDirectionContract timeChangeDirection;
        private int timeChanged;
        private DateTime? ownerSignatureDate;
        private DateTime? architectSignatureDate;
        private DateTime? contractorSignatureDate;

        public ConstructionChangeDirectiveContract()
        {
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
            this.changeType = null;
            this.priceChangeDirection = ChangeDirectionContract.Unchanged;
            this.amountChanged = 0;
            this.timeChangeDirection = ChangeDirectionContract.Unchanged;
            this.timeChanged = 0;
            this.ownerSignatureDate = null;
            this.architectSignatureDate = null;
            this.contractorSignatureDate = null;
        }

        public int Number
        {
            get { return this.number; }
            set { this.number = value; }
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

        public string Remarks
        {
            get { return this.remarks; }
            set { this.remarks = value; }
        }

        public PriceChangeTypeContract? ChangeType
        {
            get { return this.changeType; }
            set { this.changeType = value; }
        }

        public ChangeDirectionContract PriceChangeDirection
        {
            get { return this.priceChangeDirection; }
            set { this.priceChangeDirection = value; }
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
