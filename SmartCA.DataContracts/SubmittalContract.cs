using System;
using System.Collections.Generic;

namespace SmartCA.DataContracts
{
    [Serializable]
    public class SubmittalContract : ContractBase
    {
        private object projectKey;
        private SpecificationSectionContract specSection;
        private string specSectionPrimaryIndex;
        private string specSectionSecondaryIndex;
        private ProjectContactContract to;
        private DateTime transmittalDate;
        private EmployeeContract from;
        private int totalPages;
        private DeliveryContract deliveryMethod;
        private string otherDeliveryMethod;
        private string phaseNumber;
        private bool reimbursable;
        private bool final;
        private List<CopyToContract> copyToList;
        private DateTime? dateReceived;
        private string contractNumber;
        private List<TrackingItemContract> trackingItems;
        private List<RoutingItemContract> routingItems;
        private string remarks;
        private ActionStatusContract action;
        private ItemStatusContract status;
        private DateTime? dateToField;
        private SubmittalRemainderLocationContract remainderLocation;
        private string remainderUnderSubmittalNumber;
        private string otherRemainderLocation;
        private string transmittalRemarks;

        public SubmittalContract()
        {
            this.projectKey = null;
            this.specSection = null;
            this.specSectionPrimaryIndex = string.Empty;
            this.specSectionSecondaryIndex = string.Empty;
            this.to = null;
            this.from = null;
            this.totalPages = 0;
            this.deliveryMethod = DeliveryContract.Hand;
            this.otherDeliveryMethod = string.Empty;
            this.phaseNumber = string.Empty;
            this.reimbursable = false;
            this.final = false;
            this.copyToList = new List<CopyToContract>();
            this.dateReceived = null;
            this.contractNumber = string.Empty;
            this.trackingItems = new List<TrackingItemContract>();
            this.routingItems = new List<RoutingItemContract>();
            this.remarks = string.Empty;
            this.action = ActionStatusContract.NoExceptionTaken;
            this.status = null;
            this.dateToField = null;
            this.remainderLocation = SubmittalRemainderLocationContract.FilingCabinet;
            this.remainderUnderSubmittalNumber = string.Empty;
            this.otherRemainderLocation = string.Empty;
            this.transmittalRemarks = string.Empty;
        }

        public object ProjectKey
        {
            get { return this.projectKey; }
            set { this.projectKey = value; }
        }

        public SpecificationSectionContract SpecSection
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

        public ProjectContactContract To
        {
            get { return this.to; }
            set { this.to = value; }
        }

        public DateTime TransmittalDate
        {
            get { return this.transmittalDate; }
            set { this.transmittalDate = value; }
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

        public IList<TrackingItemContract> TrackingItems
        {
            get { return this.trackingItems; }
        }

        public IList<RoutingItemContract> RoutingItems
        {
            get { return this.routingItems; }
        }

        public string Remarks
        {
            get { return this.remarks; }
            set { this.remarks = value; }
        }

        public ActionStatusContract Action
        {
            get { return this.action; }
            set { this.action = value; }
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

        public SubmittalRemainderLocationContract RemainderLocation
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

        public string TransmittalRemarks
        {
            get { return this.transmittalRemarks; }
            set { this.transmittalRemarks = value; }
        }
    }
}
