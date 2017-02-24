using System;
using System.Collections.Generic;

namespace SmartCA.DataContracts
{
    [Serializable]
    public class RequestForInformationContract : ContractBase
    {
        private object projectKey;
        private int number;
        private DateTime transmittalDate;
        private ProjectContactContract from;
        private int totalPages;
        private DeliveryContract deliveryMethod;
        private string otherDeliveryMethod;
        private string phaseNumber;
        private bool reimbursable;
        private bool final;
        private List<CopyToContract> copyToList;
        private DateTime? dateReceived;
        private DateTime? dateRequestedBy;
        private CompanyContract contractor;
        private SpecificationSectionContract specSection;
        private List<RoutingItemContract> routingItems;
        private string question;
        private string description;
        private string contractorProposedSolution;
        private bool change;
        private int cause;
        private int origin;
        private ItemStatusContract status;
        private DateTime? dateToField;
        private string shortAnswer;
        private string longAnswer;
        private string remarks;

        public RequestForInformationContract()
        {
            this.projectKey = null;
            this.number = 0;
            this.from = null;
            this.totalPages = 0;
            this.deliveryMethod = DeliveryContract.Hand;
            this.phaseNumber = string.Empty;
            this.reimbursable = false;
            this.final = false;
            this.copyToList = new List<CopyToContract>();
            this.dateReceived = null;
            this.dateRequestedBy = null;
            this.contractor = null;
            this.specSection = null;
            this.routingItems = new List<RoutingItemContract>();
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

        public DateTime TransmittalDate
        {
            get { return this.transmittalDate; }
            set { this.transmittalDate = value; }
        }

        public ProjectContactContract From
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

        public CompanyContract Contractor
        {
            get { return this.contractor; }
            set { this.contractor = value; }
        }

        public SpecificationSectionContract SpecSection
        {
            get { return this.specSection; }
            set { this.specSection = value; }
        }

        public IList<RoutingItemContract> RoutingItems
        {
            get { return this.routingItems; }
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

        public ItemStatusContract Status
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
    }
}
