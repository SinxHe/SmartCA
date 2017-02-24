using System;
using SmartCA.Infrastructure.DomainBase;

namespace SmartCA.Model.Employees
{
    public class Employee : Person, IAggregateRoot
    {
        private string jobTitle;

        public Employee(object key) 
            : this(key, string.Empty, string.Empty)
        {
        }

        public Employee(object key, string firstName, string lastName) 
            : base(key, firstName, lastName)
        {
            this.jobTitle = string.Empty;
        }

        public string JobTitle
        {
            get { return this.jobTitle; }
            set { this.jobTitle = value; }
        }

        protected override void Validate()
        {
            base.Validate();
        }

        protected override BrokenRuleMessages GetBrokenRuleMessages()
        {
            return new EmployeeRuleMessages();
        }
    }
}
