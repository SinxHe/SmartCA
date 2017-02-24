using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCA.Infrastructure.Specifications
{
    public class StartsWithSpecification<TCandidate, TValue> : ValueBoundSpecification<TCandidate, TValue>
    {
        public StartsWithSpecification(string attributeName, TValue attributeValue) 
            : base(attributeName, attributeValue)
        {
        }

        public override bool IsSatisfiedBy(TCandidate candidate)
        {
            return this.GetCandidateStringValue(candidate).StartsWith(this.AttributeValue.ToString());
        }
    }
}
