using System;
using SmartCA.Infrastructure.DomainBase;
using System.Collections.Generic;

namespace SmartCA.Model.Transmittals
{
    public abstract class Transmittal : EntityBase, ITransmittal
    {
        #region Private Fields

        private object projectKey;
        private DateTime transmittalDate;
        private int totalPages;
        private Delivery deliveryMethod;
        private string otherDeliveryMethod;
        private string phaseNumber;
        private bool reimbursable;
        private bool final;
        private List<CopyTo> copyToList;
        private string transmittalRemarks;

        #endregion

        #region Constructors

        protected Transmittal(object projectKey) 
            : this(null, projectKey)
        {
        }

        protected Transmittal(object key, object projectKey) 
            : base(key)
        {
            this.projectKey = projectKey;
            this.transmittalDate = DateTime.Now;
            this.totalPages = 1;
            this.deliveryMethod = Delivery.None;
            this.otherDeliveryMethod = string.Empty;
            this.phaseNumber = string.Empty;
            this.reimbursable = false;
            this.final = false;
            this.copyToList = new List<CopyTo>();
            this.transmittalRemarks = string.Empty;
        }

        #endregion

        #region ITransmittal Members

        public object ProjectKey
        {
            get { return this.projectKey; }
        }

        public DateTime TransmittalDate
        {
            get { return this.transmittalDate; }
            set { this.transmittalDate = value; }
        }

        public int TotalPages
        {
            get { return this.totalPages; }
            set 
            {
                // Total Pages must be a positive number
                if (value > 0)
                {
                    this.totalPages = value;
                }
            }
        }

        public Delivery DeliveryMethod
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

        public IList<CopyTo> CopyToList
        {
            get { return this.copyToList; }
        }

        public string TransmittalRemarks
        {
            get { return this.transmittalRemarks; }
            set { this.transmittalRemarks = value; }
        }

        #endregion
    }
}
