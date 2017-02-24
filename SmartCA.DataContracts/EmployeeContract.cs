using System;

namespace SmartCA.DataContracts
{
    [Serializable]
    public class EmployeeContract : PersonContract
    {
        private string jobTitle;

        public EmployeeContract()
        {
            this.jobTitle = string.Empty;
        }

        public string JobTitle
        {
            get { return this.jobTitle; }
            set { this.jobTitle = value; }
        }
    }
}
