using System;

namespace SmartCA.DataContracts
{
    [Serializable]
    public class ItemStatusContract
    {
        private int id;
        private string status;

        public ItemStatusContract()
        {
            this.id = 0;
            this.status = string.Empty;
        }

        public int Id
        {
            get { return this.id; }
            set { this.id = value; }
        }

        public string Status
        {
            get { return this.status; }
            set { this.status = value; }
        }
    }
}
