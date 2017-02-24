using System;
using System.Collections.Generic;
using SmartCA.Infrastructure.RepositoryFramework;
using SmartCA.Model.Projects;
using SmartCA.Model.NumberedProjectChildren;

namespace SmartCA.Model.ProposalRequests
{
    public interface IProposalRequestRepository
        : INumberedProjectChildRepository<ProposalRequest>
    {
        int GetExpectedContractorReturnDays();
    }
}
