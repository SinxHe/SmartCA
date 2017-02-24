using System;
using System.Collections.Generic;
using SmartCA.Infrastructure.RepositoryFramework;
using SmartCA.Model.Projects;
using SmartCA.Infrastructure.DomainBase;

namespace SmartCA.Model.NumberedProjectChildren
{
    public interface INumberedProjectChildRepository<T>
        : IRepository<T> where T : IAggregateRoot, INumberedProjectChild
    {
        IList<T> FindBy(Project project);
    }
}
