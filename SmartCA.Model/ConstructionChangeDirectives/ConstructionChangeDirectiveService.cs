using System;
using SmartCA.Infrastructure;
using SmartCA.Infrastructure.RepositoryFramework;
using System.Collections.Generic;
using SmartCA.Model.Projects;
using System.Linq;

namespace SmartCA.Model.ConstructionChangeDirectives
{
    public class ConstructionChangeDirectiveService
    {
        private static IConstructionChangeDirectiveRepository repository;
        private static IUnitOfWork unitOfWork;

        static ConstructionChangeDirectiveService()
        {
            ConstructionChangeDirectiveService.unitOfWork = new UnitOfWork();
            ConstructionChangeDirectiveService.repository =
                RepositoryFactory.GetRepository
                <IConstructionChangeDirectiveRepository,
                ConstructionChangeDirective>(
                ConstructionChangeDirectiveService.unitOfWork);
        }

        public static ConstructionChangeDirective GetConstructionChangeDirective(object ccdKey)
        {
            return ConstructionChangeDirectiveService.repository.FindBy(ccdKey);
        }

        public static IList<ConstructionChangeDirective>
            GetConstructionChangeDirectives(Project project)
        {
            return 
                ConstructionChangeDirectiveService.repository.FindBy(
                project);
        }

        public static void SaveConstructionChangeDirective(
            ConstructionChangeDirective ccd)
        {
            ConstructionChangeDirectiveService.repository[ccd.Key] = ccd;
            ConstructionChangeDirectiveService.unitOfWork.Commit();
        }
    }
}
