using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCA.Infrastructure.Specifications
{
    public class EqualSpecification<TCandidate, TValue> : ValueBoundSpecification<TCandidate, TValue>
    {
        public EqualSpecification(string attributeName, TValue attributeValue) 
            : base(attributeName, attributeValue)
        {
        }

        public override bool IsSatisfiedBy(TCandidate candidate)
        {
            TValue actual = this.GetCandidateTValue(candidate);
            if (actual == null && this.AttributeValue == null)
            {
                return true;
            }
            else if (actual == null || this.AttributeValue == null)
            {
                return false;
            }
            return actual.Equals(this.AttributeValue);
        }
    }
}
