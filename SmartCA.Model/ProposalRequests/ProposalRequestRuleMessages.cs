using System;
using System.Collections.Generic;
using SmartCA.Infrastructure.DomainBase;
using SmartCA.Model.NumberedProjectChildren;
using SmartCA.Model.Description;

namespace SmartCA.Model.ProposalRequests
{
    internal class ProposalRequestRuleMessages : BrokenRuleMessages
    {
        internal static class MessageKeys
        {
            public const string InvalidProjectContact = "Must Have " + 
                "ProjectContact Assigned";
            public const string InvalidEmployee = "Must Have Employee Assigned";
            public const string InvalidContractor = "Must Have Contractor " + 
                "Assigned";
        }

        protected override void PopulateMessages()
        {
            // Add the rule messages
            this.Messages.Add(NumberedProjectChildrenRuleMessageKeys.InvalidNumber,
                "The same Proposal Request number cannot be used for the " + 
                "current project, and there cannot be any gaps between " + 
                "Proposal Request numbers.");

            this.Messages.Add(DescriptionRuleMessageKeys.InvalidDescription,
                "The Proposal Request must have a description");

            this.Messages.Add(MessageKeys.InvalidProjectContact,
                "The Proposal Request must have a ProjectContact assigned " + 
                "to the To property.");

            this.Messages.Add(MessageKeys.InvalidEmployee,
                "The Proposal Request must have an Employee assigned to the " + 
                "From property.");

            this.Messages.Add(MessageKeys.InvalidContractor,
                "The Proposal Request must have a Company assigned to the " + 
                "Contractor property.");
        }
    }
}
