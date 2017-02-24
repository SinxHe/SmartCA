using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCA.Infrastructure.Specifications
{
    public class ContainsSpecification<TCandidate, TValue> : ValueBoundSpecification<TCandidate, TValue>
    {
        public ContainsSpecification(string attributeName, TValue attributeValue) 
            : base(attributeName, attributeValue)
        {
        }

        public override bool IsSatisfiedBy(TCandidate candidate)
        {
            return this.AttributeValue.ToString().Contains(this.GetCandidateStringValue(candidate));
        }
    }
}
