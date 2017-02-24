using System;

namespace SmartCA.DataContracts
{
    [Serializable]
    public class DisciplineContract
    {
        object key;
        private string name;
        private string description;

        public DisciplineContract()
        {
            this.key = null;
            this.name = string.Empty;
            this.description = string.Empty;
        }

        public object Key
        {
            get { return this.key; }
            set { this.key = value; }
        }

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public string Description
        {
            get { return this.description; }
            set { this.description = value; }
        }
    }
}
