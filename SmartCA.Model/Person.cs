using System;
using SmartCA.Infrastructure.DomainBase;

namespace SmartCA.Model
{
    public abstract class Person : EntityBase
    {
        private string firstName;
        private string lastName;
        private string initials;

        protected Person() 
            : this(null)
        {
        }

        protected Person(object key) 
            : this(key, string.Empty, string.Empty)
        {
        }

        protected Person(object key, string firstName, string lastName) 
            : base(key)
        {
            this.firstName = (firstName == null) ? string.Empty : firstName;
            this.lastName = (lastName == null) ? string.Empty : lastName;
            this.initials = string.Empty;
        }

        public string FirstName
        {
            get { return this.firstName; }
            set { this.firstName = value; }
        }

        public string LastName
        {
            get { return this.lastName; }
            set { this.lastName = value; }
        }

        public string Initials
        {
            get { return this.initials; }
            set { this.initials = value; }
        }

        protected override void Validate()
        {
            if (!string.IsNullOrEmpty(this.firstName))
            {
                this.BrokenRules.Add(new BrokenRule("", ""));
            }
            if (!string.IsNullOrEmpty(this.lastName))
            {
                this.BrokenRules.Add(new BrokenRule("", ""));
            }
        }
    }
}
