using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCA.Infrastructure.Specifications
{
    public class EndsWithSpecification<TCandidate, TValue> : ValueBoundSpecification<TCandidate, TValue>
    {
        public EndsWithSpecification(string attributeName, TValue attributeValue) 
            : base(attributeName, attributeValue)
        {
        }

        public override bool IsSatisfiedBy(TCandidate candidate)
        {
            return this.AttributeValue.ToString().EndsWith(this.GetCandidateStringValue(candidate));
        }
    }
}
