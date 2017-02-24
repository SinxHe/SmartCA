using System;
using SmartCA.Model.Contacts;
using SmartCA.Infrastructure.DomainBase;
using SmartCA.Model.Addresses;
using System.Collections.Generic;

namespace SmartCA.Model.Projects
{
    public class ProjectContact : EntityBase
    {
        private Project project;
        private object projectKey;
        private bool onFinalDistributionList;
        private Contact contact;

        public ProjectContact(Project project, object key, 
            Contact contact) : base(key)
        {
            this.project = project;
            this.projectKey = this.project.Key;
            this.contact = contact;
            this.onFinalDistributionList = false;
        }

        public ProjectContact(object projectKey, object key,
            Contact contact)
            : base(key)
        {
            this.projectKey = projectKey;
            this.project = null;
            this.contact = contact;
            this.onFinalDistributionList = false;
        }

        public Project Project
        {
            get 
            {
                if (this.project == null)
                {
                    this.project = ProjectService.GetProject(this.projectKey);
                }
                return this.project; 
            }
        }

        public object ProjectKey
        {
            get { return this.projectKey; }
        }

        public Contact Contact
        {
            get { return this.contact; }
        }

        public bool OnFinalDistributionList
        {
            get { return this.onFinalDistributionList; }
            set { this.onFinalDistributionList = value; }
        }

        protected override void Validate()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override BrokenRuleMessages GetBrokenRuleMessages()
        {
            return new ProjectContactRuleMessages();
        }
    }
}
