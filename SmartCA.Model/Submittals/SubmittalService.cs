using System;
using System.Collections.Generic;
using SmartCA.Infrastructure;
using SmartCA.Model.Projects;
using SmartCA.Infrastructure.RepositoryFramework;

namespace SmartCA.Model.Submittals
{
    public static class SubmittalService
    {
        private static ISubmittalRepository repository;
        private static IUnitOfWork unitOfWork;

        static SubmittalService()
        {
            SubmittalService.unitOfWork = new UnitOfWork();
            SubmittalService.repository = 
                RepositoryFactory.GetRepository<ISubmittalRepository, 
                Submittal>(SubmittalService.unitOfWork);
        }

        public static IList<Submittal> GetSubmittals(Project project)
        {
            return SubmittalService.repository.FindBy(project);
        }

        public static IList<SpecificationSection> GetSpecificationSections()
        {
            return SubmittalService.repository.FindAllSpecificationSections();
        }

        public static IList<ItemStatus> GetItemStatuses()
        {
            return SubmittalService.repository.FindAllItemStatuses();
        }

        public static void SaveSubmittal(Submittal submittal)
        {
            SubmittalService.repository[submittal.Key] = submittal;
            SubmittalService.unitOfWork.Commit();
        }

        public static IList<Discipline> GetDisciplines()
        {
            return SubmittalService.repository.FindAllDisciplines();
        }
    }
}
