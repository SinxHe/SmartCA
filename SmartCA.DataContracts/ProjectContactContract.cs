using System;

namespace SmartCA.DataContracts
{
    [Serializable]
    public class ProjectContactContract : ContractBase
    {
        private object projectKey;
        private bool onFinalDistributionList;
        private ContactContract contact;

        public ProjectContactContract()
        {
            this.projectKey = null;
            this.onFinalDistributionList = false;
            this.contact = null;
        }

        public object ProjectKey
        {
            get { return this.projectKey; }
            set { this.projectKey = value; }
        }

        public bool OnFinalDistributionList
        {
            get { return this.onFinalDistributionList; }
            set { this.onFinalDistributionList = value; }
        }

        public ContactContract Contact
        {
            get { return this.contact; }
            set { this.contact = value; }
        }
    }
}
