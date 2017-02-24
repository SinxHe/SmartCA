using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCA.Infrastructure.Specifications
{
    public class AndSpecification<TCandidate> : CompositeSpecification<TCandidate>
    {
        ISpecification<TCandidate> one;
        ISpecification<TCandidate> other;

        public AndSpecification(ISpecification<TCandidate> one, ISpecification<TCandidate> other)
        {
            this.one = one;
            this.other = other;
            this.AddChildComponents(this.one);
            this.AddChildComponents(this.other);
        }

        public override bool IsSatisfiedBy(TCandidate candidate)
        {
            return (one.IsSatisfiedBy(candidate) && other.IsSatisfiedBy(candidate));
        }
    }
}
