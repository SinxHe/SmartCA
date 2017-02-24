using System;

namespace SmartCA.DataContracts
{
    [Serializable]
    public class CopyToContract
    {
        private ProjectContactContract contact;
        private string notes;

        public CopyToContract()
        {
            this.contact = null;
            this.notes = string.Empty;
        }

        public ProjectContactContract Contact
        {
            get { return this.contact; }
            set { this.contact = value; }
        }

        public string Notes
        {
            get { return this.notes; }
            set { this.notes = value; }
        }
    }
}
