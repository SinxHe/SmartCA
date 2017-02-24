using System;
using SmartCA.Infrastructure.RepositoryFramework;
using System.Collections.Generic;
using SmartCA.Model.Projects;

namespace SmartCA.Model.Submittals
{
    public interface ISubmittalRepository : IRepository<Submittal>
    {
        IList<Submittal> FindBy(Project project);
        IList<SpecificationSection> FindAllSpecificationSections();
        IList<ItemStatus> FindAllItemStatuses();
        IList<Discipline> FindAllDisciplines();
    }
}
