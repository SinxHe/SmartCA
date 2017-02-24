using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartCA.Infrastructure;
using SmartCA.Model.Projects;
using System.Diagnostics;
using SmartCA.Infrastructure.DomainBase;
using SmartCA.Infrastructure.RepositoryFramework;

namespace SmartCA.UnitTests.MockRepositories
{
    public class MockProjectRepository : RepositoryBase<Project>, IProjectRepository
    {
        private List<Project> projects;

        public MockProjectRepository()
            : this(null)
        {
        }

        public MockProjectRepository(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
            this.projects = new List<Project>();
        }

        #region IProjectRepository Members

        public IList<Project> FindBy(IList<MarketSegment> segments, bool completed)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public Project FindBy(string projectNumber)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion

        public override Project FindBy(object key)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public IList<MarketSegment> FindAllMarketSegments()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void PersistNewItem(Project item)
        {
            Debug.WriteLine(string.Format("Saving entity ID {0}", item.Key.ToString()));
        }

        protected override void PersistUpdatedItem(Project item)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void PersistDeletedItem(Project item)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override IList<Project> FindAll()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void SaveContact(ProjectContact contact)
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }
}
