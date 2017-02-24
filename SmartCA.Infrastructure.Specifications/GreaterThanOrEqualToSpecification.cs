using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCA.Infrastructure.Specifications
{
    public class GreaterThanOrEqualToSpecification<TCandidate, TValue> : ValueBoundSpecification<TCandidate, TValue> where TValue : IComparable
    {
        public GreaterThanOrEqualToSpecification(string attributeName, TValue attributeValue) 
            : base(attributeName, attributeValue)
        {
        }

        public override bool IsSatisfiedBy(TCandidate candidate)
        {
            IComparable actual = this.GetCandidateTValue(candidate) as IComparable;
            return (actual.CompareTo(this.AttributeValue) >= 0);
        }
    }
}
