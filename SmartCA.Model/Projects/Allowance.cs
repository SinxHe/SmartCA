using System;

namespace SmartCA.Model.Projects
{
    public class Allowance
    {
        private string title;
        private decimal amount;

        public Allowance(string title, decimal amount)
        {
            this.title = title;
            this.amount = amount;
        }

        public string Title
        {
            get { return this.title; }
        }

        public decimal Amount
        {
            get { return this.amount; }
        }
    }
}
