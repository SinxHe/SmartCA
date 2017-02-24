using System;

namespace SmartCA.DataContracts
{
    [Serializable]
    public class TrackingItemContract
    {
        private int totalItemsReceived;
        private int totalItemsSent;
        private int deferredApproval;
        private int substitutionNumber;
        private SpecificationSectionContract specSection;
        private string description;
        private ActionStatusContract status;

        public TrackingItemContract()
        {
            this.totalItemsReceived = 0;
            this.totalItemsSent = 0;
            this.deferredApproval = 0;
            this.substitutionNumber = 0;
            this.specSection = null;
            this.description = string.Empty;
            this.status = ActionStatusContract.NoExceptionTaken;
        }

        public int TotalItemsReceived
        {
            get { return this.totalItemsReceived; }
            set { this.totalItemsReceived = value; }
        }

        public int TotalItemsSent
        {
            get { return this.totalItemsSent; }
            set { this.totalItemsSent = value; }
        }

        public int DeferredApproval
        {
            get { return this.deferredApproval; }
            set { this.deferredApproval = value; }
        }

        public int SubstitutionNumber
        {
            get { return this.substitutionNumber; }
            set { this.substitutionNumber = value; }
        }

        public SpecificationSectionContract SpecSection
        {
            get { return this.specSection; }
            set { this.specSection = value; }
        }

        public string Description
        {
            get { return this.description; }
            set { this.description = value; }
        }

        public ActionStatusContract Status
        {
            get { return this.status; }
            set { this.status = value; }
        }
    }
}
