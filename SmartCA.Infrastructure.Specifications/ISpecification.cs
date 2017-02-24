using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCA.Infrastructure.Specifications
{
    public interface ISpecification<TCandidate>
    {
        bool IsSatisfiedBy(TCandidate candidate);
        bool HasBeenAddedToComposite { get; set; }
    }

    public interface ISpecification
    {
        bool IsSatisfiedBy(object candidate);
        bool HasBeenAddedToComposite { get; set; }
    }
}
