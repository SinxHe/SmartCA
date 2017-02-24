using System;
using SmartCA.Infrastructure.DomainBase;
using SmartCA.Model.NumberedProjectChildren;
using SmartCA.Model.Description;

namespace SmartCA.Model.ChangeOrders
{
    public class ChangeOrderRuleMessages : BrokenRuleMessages
    {
        internal static class MessageKeys
        {
            public const string InvalidStatus = "Must Have " +
                "Status Assigned";
            public const string InvalidContractor = "Must Have Contractor " +
                "Assigned";
        }

        protected override void PopulateMessages()
        {
            // Add the rule messages
            this.Messages.Add(NumberedProjectChildrenRuleMessageKeys.InvalidNumber,
                "The same Change Order number cannot be used for the " +
                "current project, and there cannot be any gaps between " +
                "Change Order numbers.");

            this.Messages.Add(DescriptionRuleMessageKeys.InvalidDescription,
                "The Change Order must have a description");

            this.Messages.Add(MessageKeys.InvalidStatus,
                "The Change Order must have a status");

            this.Messages.Add(MessageKeys.InvalidContractor,
                "The Change Order must have a Company assigned to the " +
                "Contractor property.");
        }
    }
}
