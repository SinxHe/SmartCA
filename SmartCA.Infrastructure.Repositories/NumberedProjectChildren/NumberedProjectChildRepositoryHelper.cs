using System;
using System.Collections.Generic;
using SmartCA.Model.Projects;
using System.Text;
using SmartCA.Infrastructure.DomainBase;
using SmartCA.Model.NumberedProjectChildren;

namespace SmartCA.Infrastructure.Repositories.NumberedProjectChildren
{
    public static class NumberedProjectChildRepositoryHelper
    {
        public static IList<T> FindBy<T>(SqlCeRepositoryBase<T> repository, 
            Project project) where T : IAggregateRoot, INumberedProjectChild
        {
            StringBuilder builder = new StringBuilder(100);
            builder.Append(repository.BaseQuery);
            builder.Append(string.Format(" WHERE ProjectID = '{0}';",
                project.Key));
            return repository.BuildEntitiesFromSql(builder);
        }
    }
}
