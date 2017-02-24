using System;
using System.Collections.Generic;

namespace SmartCA.DataContracts
{
    [Serializable]
    public abstract class TransmittalContract : ContractBase
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

        protected TransmittalContract()
        {
            this.projectKey = null;
            this.transmittalDate = DateTime.Now;
            this.totalPages = 1;
            this.deliveryMethod = DeliveryContract.None;
            this.otherDeliveryMethod = string.Empty;
            this.phaseNumber = string.Empty;
            this.reimbursable = false;
            this.final = false;
            this.copyToList = new List<CopyToContract>();
            this.transmittalRemarks = string.Empty;
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

        public string TransmittalRemarks
        {
            get { return this.transmittalRemarks; }
            set { this.transmittalRemarks = value; }
        }
    }
}
