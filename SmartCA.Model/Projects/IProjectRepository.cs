using System;
using System.Collections.Generic;
using SmartCA.Infrastructure.RepositoryFramework;

namespace SmartCA.Model.Projects
{
    public interface IProjectRepository : IRepository<Project>
    {
        IList<Project> FindBy(IList<MarketSegment> segments, bool completed);
        Project FindBy(string projectNumber);
        IList<MarketSegment> FindAllMarketSegments();
        void SaveContact(ProjectContact contact);
    }
}
