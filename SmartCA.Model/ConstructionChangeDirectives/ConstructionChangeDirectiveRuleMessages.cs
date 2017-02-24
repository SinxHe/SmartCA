using System;
using SmartCA.Infrastructure.DomainBase;
using SmartCA.Model.NumberedProjectChildren;
using SmartCA.Model.Description;

namespace SmartCA.Model.ConstructionChangeDirectives
{
    public class ConstructionChangeDirectiveRuleMessages 
        : BrokenRuleMessages
    {
        internal static class MessageKeys
        {
            public const string InvalidContractor = "Must Have Contractor " +
                "Assigned";
        }

        protected override void PopulateMessages()
        {
            // Add the rule messages
            this.Messages.Add(DescriptionRuleMessageKeys.InvalidDescription,
                "The Construction Change Directive must have a description");

            this.Messages.Add(MessageKeys.InvalidContractor,
                "The Construction Change Directive must have a Company assigned " + 
                "to the Contractor property.");
        }
    }
}
