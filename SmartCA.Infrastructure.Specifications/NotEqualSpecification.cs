using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCA.Infrastructure.Specifications
{
    public class NotEqualSpecification<TCandidate, TValue> : ValueBoundSpecification<TCandidate, TValue>
    {
        public NotEqualSpecification(string attributeName, TValue attributeValue)
            : base(attributeName, attributeValue)
        {
        }

        public override bool IsSatisfiedBy(TCandidate candidate)
        {
            TValue actual = this.GetCandidateTValue(candidate);
            return !(actual.Equals(this.AttributeValue));
        }
    }
}
