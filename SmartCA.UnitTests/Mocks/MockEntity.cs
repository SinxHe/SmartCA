using System;
using SmartCA.Infrastructure.DomainBase;

namespace SmartCA.UnitTests.Mocks
{
    public class MockEntity : EntityBase
    {
        public MockEntity()
        {
        }

        public MockEntity(Guid id)
            : base(id)
        {
        }

        protected override void Validate()
        {
        }

        protected override BrokenRuleMessages GetBrokenRuleMessages()
        {
            return new MockRuleMessages();
        }
    }
}
