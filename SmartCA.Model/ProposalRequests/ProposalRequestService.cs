using System;
using System.Collections.Generic;
using SmartCA.Infrastructure;
using SmartCA.Infrastructure.RepositoryFramework;
using SmartCA.Model.Projects;

namespace SmartCA.Model.ProposalRequests
{
    public static class ProposalRequestService
    {
        private static IProposalRequestRepository repository;
        private static IUnitOfWork unitOfWork;
        
        static ProposalRequestService()
        {
            ProposalRequestService.unitOfWork = new UnitOfWork();
            ProposalRequestService.repository =
                RepositoryFactory.GetRepository<IProposalRequestRepository,
                ProposalRequest>(ProposalRequestService.unitOfWork);
        }

        public static IList<ProposalRequest>
            GetProposalRequests(Project project)
        {
            return ProposalRequestService.repository.FindBy(project);
        }

        public static void SaveProposalRequest(ProposalRequest proposalRequest)
        {
            ProposalRequestService.repository[proposalRequest.Key] = 
                proposalRequest;
            ProposalRequestService.unitOfWork.Commit();
        }

        public static int GetExpectedContractorReturnDays()
        {
            return ProposalRequestService.repository.GetExpectedContractorReturnDays();
        }
    }
}
