using System;

namespace SmartCA.DataContracts
{
    [Serializable]
    public class AllowanceContract
    {
        private string title;
        private decimal amount;

        public AllowanceContract()
        {
            this.title = string.Empty;
            this.amount = 0;
        }

        public string Title
        {
            get { return this.title; }
            set { this.title = value; }
        }

        public decimal Amount
        {
            get { return this.amount; }
            set { this.amount = value; }
        }
    }
}
