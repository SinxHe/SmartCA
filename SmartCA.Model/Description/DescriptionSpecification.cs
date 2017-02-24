using System;
using SmartCA.Infrastructure.Specifications;
using SmartCA.Infrastructure.DomainBase;
using SmartCA.Model.Description;

namespace SmartCA.Model.Description
{
    public class DescriptionSpecification<TCandidate>
        : Specification<TCandidate> where TCandidate : IDescribable
    {
        public override bool IsSatisfiedBy(TCandidate candidate)
        {
            // The candidate must have a description
            return (!string.IsNullOrEmpty(candidate.Description));
        }
    }
}
