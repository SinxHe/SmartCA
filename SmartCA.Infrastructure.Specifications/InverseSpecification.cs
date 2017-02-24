using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCA.Infrastructure.Specifications
{
    public class InverseSpecification<TCandidate> : CompositeSpecification<TCandidate>
    {
        ISpecification<TCandidate> toBeWrapped;

        public InverseSpecification(ISpecification<TCandidate> toBeWrapped)
        {
            this.toBeWrapped = toBeWrapped;
            this.AddChildComponents(this.toBeWrapped);
        }

        public override bool IsSatisfiedBy(TCandidate candidate)
        {
            return !(this.toBeWrapped.IsSatisfiedBy(candidate));
        }
    }
}
