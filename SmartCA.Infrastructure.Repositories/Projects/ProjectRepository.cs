using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using SmartCA.Infrastructure.DomainBase;
using SmartCA.Infrastructure.EntityFactoryFramework;
using SmartCA.Infrastructure.Helpers;
using SmartCA.Model.Companies;
using SmartCA.Model.Employees;
using SmartCA.Model.Projects;

namespace SmartCA.Infrastructure.Repositories
{
    public class ProjectRepository : SqlCeRepositoryBase<Project>, 
        IProjectRepository
    {
        #region Private Fields
        
        #endregion

        #region Public Constructors

        public ProjectRepository()
            : this(null)
        {
        }

        public ProjectRepository(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }

        #endregion

        #region IProjectRepository Members

        public IList<Project> FindBy(IList<MarketSegment> segments, bool completed)
        {
            StringBuilder builder = this.GetBaseQueryBuilder();
            if (completed)
            {
                builder.Append(" WHERE p.ActualCompletionDate IS NOT NULL AND p.PercentComplete > 99");
            }
            else
            {
                builder.Append(" WHERE p.ActualCompletionDate IS NULL AND p.PercentComplete < 100");
            }
            if (segments != null || segments.Count > 0)
            {
                builder.Append(string.Format(" AND p.MarketSegmentID IN ({0})", 
                    DataHelper.EntityListToDelimited(segments).ToString()));
            }
            builder.Append(";");
            return this.BuildEntitiesFromSql(builder.ToString());
        }

        public Project FindBy(string projectNumber)
        {
            StringBuilder builder = this.GetBaseQueryBuilder();
            return this.BuildEntityFromSql(builder.Append(string.Format(" WHERE p.ProjectNumber = N'{0}';", 
                projectNumber)).ToString());
        }

        public IList<MarketSegment> FindAllMarketSegments()
        {
            List<MarketSegment> segments = new List<MarketSegment>();
            string query = "SELECT * FROM MarketSegment mst INNER JOIN MarketSector msr ON mst.MarketSectorID = msr.MarketSectorID;";
            IEntityFactory<MarketSegment> factory = EntityFactoryBuilder.BuildFactory<MarketSegment>();
            using (IDataReader reader = this.ExecuteReader(query))
            {
                while (reader.Read())
                {
                    segments.Add(factory.BuildEntity(reader));
                }
            }
            return segments;
        }

        public void SaveContact(ProjectContact contact)
        {
            
            // Get the list of contacts
            List<ProjectContact> contacts = 
                new List<ProjectContact>(
                    this.FindBy(contact.Project.Key).Contacts);

            if (contacts.Where(c => c.Key.Equals(contact.Key)).Count() > 0)
            {
                // The contact exists, so update it
                this.UnitOfWork.RegisterChanged(contact, this);
            }
            else
            {
                // The contact is new, so add it
                this.UnitOfWork.RegisterAdded(contact, this);
            }
        }

        #endregion

        #region BuildChildCallbacks

        protected override void BuildChildCallbacks()
        {
            this.ChildCallbacks.Add(ProjectFactory.FieldNames.OwnerCompanyId, 
                this.AppendOwner);
            this.ChildCallbacks.Add(
                ProjectFactory.FieldNames.ConstructionAdministratorEmployeeId, 
                this.AppendConstructionAdministrator);
            this.ChildCallbacks.Add(ProjectFactory.FieldNames.PrincipalEmployeeId, 
                this.AppendPrincipal);
            this.ChildCallbacks.Add("allowances", 
                delegate(Project project, object childKeyName) 
                { 
                    this.AppendProjectAllowances(project); 
                });
            this.ChildCallbacks.Add("contracts",
                delegate(Project project, object childKeyName)
                {
                    this.AppendContracts(project);
                });
            this.ChildCallbacks.Add("contacts",
                delegate(Project project, object childKeyName)
                {
                    this.AppendContacts(project);
                });
        }

        #endregion

        #region GetBaseQuery

        protected override string GetBaseQuery()
        {
            return "SELECT * FROM Project p INNER JOIN MarketSegment ms ON p.MarketSegmentID = ms.MarketSegmentID";
        }

        #endregion

        #region GetBaseWhereClause

        protected override string GetBaseWhereClause()
        {
            return " WHERE ProjectID = '{0}';";
        }

        #endregion

        #region GetEntityName

        protected override string GetEntityName()
        {
            return "Project";
        }

        #endregion

        #region GetKeyFieldName

        protected override string GetKeyFieldName()
        {
            return ProjectFactory.FieldNames.ProjectId;
        }

        #endregion

        #region Unit of Work Implementation

        public override void PersistNewItem(IEntity item)
        {
            Project project = item as Project;
            if (project != null)
            {
                this.PersistNewItem(project);
            }
            else
            {
                ProjectContact contact = item as ProjectContact;
                this.PersistNewItem(contact);
            }
        }

        protected void PersistNewItem(ProjectContact contact)
        {
            StringBuilder builder = new StringBuilder(100);
            builder.Append(string.Format(
                "INSERT INTO ProjectContact ({0},{1},{2}) ",
            ProjectFactory.FieldNames.ProjectId,
            ContactFactory.FieldNames.ContactId,
            ProjectFactory.FieldNames.OnFinalDistributionList));
            builder.Append(string.Format("VALUES ({0},{1},{2});",
                DataHelper.GetSqlValue(contact.Project.Key),
                DataHelper.GetSqlValue(contact.Contact.Key),
                DataHelper.GetSqlValue(contact.OnFinalDistributionList)));
            this.Database.ExecuteNonQuery(
                this.Database.GetSqlStringCommand(builder.ToString()));
        }

        protected override void PersistNewItem(Project item)
        {
            StringBuilder builder = new StringBuilder(100);
            builder.Append(string.Format("INSERT INTO Project ({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23},{24},{25},{26}) ",
                ProjectFactory.FieldNames.ProjectId, 
                ProjectFactory.FieldNames.ProjectNumber, 
                ProjectFactory.FieldNames.ProjectName, 
                ProjectFactory.FieldNames.ConstructionAdministratorEmployeeId,
                ProjectFactory.FieldNames.PrincipalEmployeeId, 
                ProjectFactory.FieldNames.ProjectAddress,
                ProjectFactory.FieldNames.ProjectCity, 
                ProjectFactory.FieldNames.ProjectState,
                ProjectFactory.FieldNames.ProjectPostalCode, 
                ProjectFactory.FieldNames.OwnerCompanyId,
                ProjectFactory.FieldNames.ContractDate, 
                ProjectFactory.FieldNames.EstimatedStartDate,
                ProjectFactory.FieldNames.EstimatedCompletionDate, 
                ProjectFactory.FieldNames.CurrentCompletionDate,
                ProjectFactory.FieldNames.ActualCompletionDate, 
                ProjectFactory.FieldNames.ContingencyAllowanceAmount,
                ProjectFactory.FieldNames.TestingAllowanceAmount, 
                ProjectFactory.FieldNames.UtilityAllowanceAmount,
                ProjectFactory.FieldNames.OriginalConstructionCost, 
                ProjectFactory.FieldNames.AeChangeOrderAmount,
                ProjectFactory.FieldNames.TotalSquareFeet, 
                ProjectFactory.FieldNames.PercentComplete,
                ProjectFactory.FieldNames.Remarks, 
                ProjectFactory.FieldNames.ContractReason,
                ProjectFactory.FieldNames.AgencyApplicationNumber, 
                ProjectFactory.FieldNames.AgencyFileNumber,
                ProjectFactory.FieldNames.MarketSegmentId));
            builder.Append(string.Format("VALUES ({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23},{24},{25},{26});",
                DataHelper.GetSqlValue(item.Key), 
                DataHelper.GetSqlValue(item.Number),
                DataHelper.GetSqlValue(item.Name), 
                item.ConstructionAdministrator.Key,
                item.PrincipalInCharge.Key, 
                DataHelper.GetSqlValue(item.Address.Street),
                DataHelper.GetSqlValue(item.Address.City), 
                DataHelper.GetSqlValue(item.Address.State),
                DataHelper.GetSqlValue(item.Address.PostalCode), 
                DataHelper.GetSqlValue(item.Owner.Key),
                DataHelper.GetSqlValue(item.ContractDate), 
                DataHelper.GetSqlValue(item.EstimatedStartDate),
                DataHelper.GetSqlValue(item.EstimatedCompletionDate), 
                DataHelper.GetSqlValue(item.CurrentCompletionDate),
                DataHelper.GetSqlValue(item.ActualCompletionDate), 
                item.ContingencyAllowanceAmount, 
                item.TestingAllowanceAmount, 
                item.UtilityAllowanceAmount, 
                item.OriginalConstructionCost,
                item.AeChangeOrderAmount, 
                item.TotalSquareFeet, 
                item.PercentComplete,
                DataHelper.GetSqlValue(item.Remarks), 
                DataHelper.GetSqlValue(item.ContractReason),
                DataHelper.GetSqlValue(item.AgencyApplicationNumber), 
                DataHelper.GetSqlValue(item.AgencyFileNumber),
                item.Segment.Key));
            this.Database.ExecuteNonQuery(this.Database.GetSqlStringCommand(builder.ToString()));
        }

        public override void PersistUpdatedItem(IEntity item)
        {
            Project project = item as Project;
            if (project != null)
            {
                this.PersistUpdatedItem(project);
            }
            else
            {
                ProjectContact contact = item as ProjectContact;
                this.PersistUpdatedItem(contact);
            }
        }

        protected void PersistUpdatedItem(ProjectContact contact)
        {
            StringBuilder builder = new StringBuilder(100);
            builder.Append("UPDATE ProjectContact SET ");
            builder.Append(string.Format("{0} = {1}",
                ProjectFactory.FieldNames.OnFinalDistributionList,
                DataHelper.GetSqlValue(contact.OnFinalDistributionList)));
            builder.Append(" ");
            builder.Append(string.Format("WHERE ProjectContactID = '{0}'", contact.Key));
            this.Database.ExecuteNonQuery(this.Database.GetSqlStringCommand(builder.ToString()));
        }

        protected override void PersistUpdatedItem(Project item)
        {
            StringBuilder builder = new StringBuilder(100);
            builder.Append("UPDATE Project SET ");

            builder.Append(string.Format("{0} = {1}", 
                ProjectFactory.FieldNames.ConstructionAdministratorEmployeeId, 
                item.ConstructionAdministrator.Key));

            builder.Append(string.Format(",{0} = {1}",
                ProjectFactory.FieldNames.PrincipalEmployeeId,
                item.PrincipalInCharge.Key));

            builder.Append(string.Format(",{0} = {1}",
                ProjectFactory.FieldNames.ProjectAddress,
                DataHelper.GetSqlValue(item.Address.Street)));

            builder.Append(string.Format(",{0} = {1}",
                ProjectFactory.FieldNames.ProjectCity,
                DataHelper.GetSqlValue(item.Address.City)));

            builder.Append(string.Format(",{0} = {1}",
                ProjectFactory.FieldNames.ProjectState,
                DataHelper.GetSqlValue(item.Address.State)));

            builder.Append(string.Format(",{0} = {1}",
                ProjectFactory.FieldNames.ProjectPostalCode,
                DataHelper.GetSqlValue(item.Address.PostalCode)));

            builder.Append(string.Format(",{0} = {1}",
                ProjectFactory.FieldNames.OwnerCompanyId,
                DataHelper.GetSqlValue(item.Owner.Key)));

            builder.Append(string.Format(",{0} = {1}",
                ProjectFactory.FieldNames.ContractDate,
                DataHelper.GetSqlValue(item.ContractDate)));

            builder.Append(string.Format(",{0} = {1}",
                ProjectFactory.FieldNames.EstimatedStartDate,
                DataHelper.GetSqlValue(item.EstimatedStartDate)));

            builder.Append(string.Format(",{0} = {1}",
                ProjectFactory.FieldNames.EstimatedCompletionDate,
                DataHelper.GetSqlValue(item.EstimatedCompletionDate)));

            builder.Append(string.Format(",{0} = {1}",
                ProjectFactory.FieldNames.CurrentCompletionDate,
                DataHelper.GetSqlValue(item.CurrentCompletionDate)));

            builder.Append(string.Format(",{0} = {1}",
                ProjectFactory.FieldNames.ActualCompletionDate,
                DataHelper.GetSqlValue(item.ActualCompletionDate)));

            builder.Append(string.Format(",{0} = {1}",
                ProjectFactory.FieldNames.ContingencyAllowanceAmount,
                item.ContingencyAllowanceAmount));

            builder.Append(string.Format(",{0} = {1}",
                ProjectFactory.FieldNames.TestingAllowanceAmount,
                item.TestingAllowanceAmount));

            builder.Append(string.Format(",{0} = {1}",
                ProjectFactory.FieldNames.UtilityAllowanceAmount,
                item.UtilityAllowanceAmount));

            builder.Append(string.Format(",{0} = {1}",
                ProjectFactory.FieldNames.OriginalConstructionCost,
                item.OriginalConstructionCost));

            builder.Append(string.Format(",{0} = {1}",
                ProjectFactory.FieldNames.AeChangeOrderAmount,
                item.AeChangeOrderAmount));

            builder.Append(string.Format(",{0} = {1}",
                ProjectFactory.FieldNames.TotalSquareFeet,
                item.TotalSquareFeet));

            builder.Append(string.Format(",{0} = {1}",
                ProjectFactory.FieldNames.PercentComplete,
                item.PercentComplete));

            builder.Append(string.Format(",{0} = {1}",
                ProjectFactory.FieldNames.Remarks,
                DataHelper.GetSqlValue(item.Remarks)));

            builder.Append(string.Format(",{0} = {1}",
                ProjectFactory.FieldNames.ContractReason,
                DataHelper.GetSqlValue(item.ContractReason)));

            builder.Append(string.Format(",{0} = {1}",
                ProjectFactory.FieldNames.AgencyApplicationNumber,
                DataHelper.GetSqlValue(item.AgencyApplicationNumber)));

            builder.Append(string.Format(",{0} = {1}",
                ProjectFactory.FieldNames.AgencyFileNumber,
                DataHelper.GetSqlValue(item.AgencyFileNumber)));

            builder.Append(string.Format(",{0} = {1}",
                ProjectFactory.FieldNames.MarketSegmentId,
                item.Segment.Key));

            builder.Append(" ");
            builder.Append(this.BuildBaseWhereClause(item.Key));

            this.Database.ExecuteNonQuery(this.Database.GetSqlStringCommand(builder.ToString()));
        }

        public override void PersistDeletedItem(IEntity item)
        {
            Project project = item as Project;
            if (project != null)
            {
                this.PersistDeletedItem(project);
            }
            else
            {
                ProjectContact contact = item as ProjectContact;
                this.PersistDeletedItem(contact);
            }
        }

        protected void PersistDeletedItem(ProjectContact contact)
        {
            StringBuilder builder = new StringBuilder(100);
            builder.Append("UPDATE ProjectContact SET ");
            builder.Append(string.Format("{0} = {1}",
                ProjectFactory.FieldNames.OnFinalDistributionList,
                DataHelper.GetSqlValue(contact.OnFinalDistributionList)));
            builder.Append(" ");
            builder.Append(string.Format("WHERE ProjectContactID = '{0}'", contact.Key));
            this.Database.ExecuteNonQuery(this.Database.GetSqlStringCommand(builder.ToString()));
        }

        protected override void PersistDeletedItem(Project item)
        {
            // Delete the child objects
            string query = string.Format("DELETE FROM ProjectContact {0}",
                this.BuildBaseWhereClause(item.Key));
            query = string.Format("DELETE FROM ProjectAllowance {0}", 
                this.BuildBaseWhereClause(item.Key));
            this.Database.ExecuteNonQuery(this.Database.GetSqlStringCommand(query));

            // Delete the Project
            base.PersistDeletedItem(item);
        }

        #endregion

        #region Private Callback and Helper Methods

        private void AppendProjectAllowances(Project project)
        {
            string sql = 
                string.Format("SELECT * FROM ProjectAllowance WHERE ProjectID = '{0}'", project.Key);
            using (IDataReader reader = this.ExecuteReader(sql))
            {
                while (reader.Read())
                {
                    project.Allowances.Add(ProjectFactory.BuildAllowance(reader));
                }
            }
        }

        private void AppendOwner(Project project, object ownerCompanyId)
        {
            project.Owner = CompanyService.GetCompany(ownerCompanyId);
        }

        private void AppendConstructionAdministrator(Project project, 
            object constructionAdministratorId)
        {
            project.ConstructionAdministrator =
                EmployeeService.GetEmployee(constructionAdministratorId);
        }

        private void AppendPrincipal(Project project, object principalId)
        {
            project.PrincipalInCharge = EmployeeService.GetEmployee(principalId);
        }

        private void AppendContacts(Project project)
        {
            StringBuilder builder = new StringBuilder(100);
            builder.Append("SELECT * FROM ProjectContact");
            builder.Append(string.Format(" WHERE ProjectID = '{0}'", project.Key));
            using (IDataReader reader = this.ExecuteReader(builder.ToString()))
            {
                while (reader.Read())
                {
                    project.Contacts.Add(ProjectFactory.BuildProjectContact(project, reader));
                }
            }
        }

        private void AppendContracts(Project project)
        {
            StringBuilder builder = new StringBuilder(100);
            builder.Append("SELECT * FROM Contract");
            builder.Append(string.Format(" WHERE ProjectID = '{0}'", project.Key));
            using (IDataReader reader = this.ExecuteReader(builder.ToString()))
            {
                while (reader.Read())
                {
                    project.Contracts.Add(ProjectFactory.BuildContract(reader));
                }
            }
        }

        #endregion
    }
}
