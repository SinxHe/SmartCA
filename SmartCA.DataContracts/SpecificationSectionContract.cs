using System;

namespace SmartCA.DataContracts
{
    [Serializable]
    public class SpecificationSectionContract
    {
        private int key;
        private string number;
        private string title;
        private string description;

        public int Key
        {
            get { return this.key; }
            set { this.key = value; }
        }

        public string Number
        {
            get { return this.number; }
            set { this.number = value; }
        }

        public string Title
        {
            get { return this.title; }
            set { this.title = value; }
        }

        public string Description
        {
            get { return this.description; }
            set { this.description = value; }
        }
    }
}
