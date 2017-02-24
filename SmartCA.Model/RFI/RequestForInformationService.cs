using System;
using SmartCA.Infrastructure;
using SmartCA.Infrastructure.RepositoryFramework;
using SmartCA.Model.Projects;
using System.Collections.Generic;

namespace SmartCA.Model.RFI
{
    public static class RequestForInformationService
    {
        private static IRequestForInformationRepository repository;
        private static IUnitOfWork unitOfWork;

        static RequestForInformationService()
        {
            RequestForInformationService.unitOfWork = new UnitOfWork();
            RequestForInformationService.repository =
                RepositoryFactory.GetRepository<IRequestForInformationRepository,
                RequestForInformation>(RequestForInformationService.unitOfWork);
        }

        public static IList<RequestForInformation> 
            GetRequestsForInformation(Project project)
        {
            return RequestForInformationService.repository.FindBy(project);
        }

        public static void SaveRequestForInformation(RequestForInformation rfi)
        {
            RequestForInformationService.repository[rfi.Key] = rfi;
            RequestForInformationService.unitOfWork.Commit();
        }
    }
}
