using System;

namespace SmartCA.Infrastructure.DomainBase
{
    public class BrokenRule
    {
        private string name;
        private string description;

        public BrokenRule(string name, string description)
        {
            this.name = name;
            this.description = description;
        }

        public string Name
        {
            get { return this.name; }
        }

        public string Description
        {
            get { return this.description; }
        }
    }
}
