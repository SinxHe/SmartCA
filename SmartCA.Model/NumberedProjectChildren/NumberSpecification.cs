using System;
using System.Collections.Generic;
using System.Linq;
using SmartCA.Infrastructure.Specifications;
using SmartCA.Model.Projects;
using SmartCA.Infrastructure.RepositoryFramework;
using SmartCA.Infrastructure.DomainBase;

namespace SmartCA.Model.NumberedProjectChildren
{
    public class NumberSpecification<TCandidate>
        : Specification<TCandidate> where TCandidate : IAggregateRoot, 
        INumberedProjectChild
    {
        public override bool IsSatisfiedBy(TCandidate candidate)
        {
            bool isSatisfiedBy = true;

            // Make sure that the same entity number has not 
            // been used for the current project, and that there are no 
            // gaps between entity numbers

            // First get the project associated with the entity
            Project project = ProjectService.GetProject(candidate.ProjectKey);

            // Next get the list of items for the project

            // First get the correct Repository
            INumberedProjectChildRepository<TCandidate> repository = 
                RepositoryFactory.GetRepository
                <INumberedProjectChildRepository<TCandidate>, TCandidate>();

            // Now use the Repository to find all of the items by the Project
            IList<TCandidate> items = repository.FindBy(project);

            // Find out if this is a new Entity or an existing one
            // No need to proceed if it already exists
            if (items.Where(item => item.Key.Equals(candidate.Key)).Count() == 0)
            {
                // Use a LINQ query to determine if the entity number has been 
                // used before
                isSatisfiedBy =
                    (items.Where(item => item.Number.Equals(candidate.Number)).Count()
                        < 1);

                // See if the candidate passed the first test
                if (isSatisfiedBy)
                {
                    // First test passed, now use another LINQ query to make sure that 
                    // there are no gaps
                    isSatisfiedBy =
                        (candidate.Number - items.Max(item => item.Number) == 1);
                }
            }

            return isSatisfiedBy;
        }
    }
}
