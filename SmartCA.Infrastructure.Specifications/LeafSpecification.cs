using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCA.Infrastructure.Specifications
{
    public abstract class LeafSpecification<TCandidate> : CompositeSpecification<TCandidate>
    {
        public abstract override bool IsSatisfiedBy(TCandidate candidate);
    }
}
