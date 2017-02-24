using System.Collections.Generic;
using System.Data;
using System.Text;
using SmartCA.Infrastructure.Helpers;
using SmartCA.Infrastructure.Repositories.Transmittals;
using SmartCA.Model;
using SmartCA.Model.Employees;
using SmartCA.Model.Projects;
using SmartCA.Model.Submittals;

namespace SmartCA.Infrastructure.Repositories
{
    public class SubmittalRepository : SqlCeRoutableTransmittalRepository<Submittal>, 
        ISubmittalRepository
    {
        #region Private Fields

        #endregion

        #region Public Constructors

        public SubmittalRepository()
            : this(null)
        {
        }

        public SubmittalRepository(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }

        #endregion

        #region GetBaseQuery

        protected override string GetBaseQuery()
        {
            StringBuilder builder = new StringBuilder(200);
            builder.Append("SELECT * FROM Submittal s ");
            builder.Append("INNER JOIN SpecificationSection ss ");
            builder.Append("ON s.SpecificationSectionID = ss.SpecificationSectionID ");
            builder.Append("INNER JOIN ItemStatus ist ");
            builder.Append("ON s.ItemStatusID = ist.ItemStatusID");
            return builder.ToString();
        }

        #endregion

        #region GetBaseWhereClause

        protected override string GetBaseWhereClause()
        {
            return " WHERE SubmittalID = '{0}';";
        }

        #endregion

        #region GetEntityName

        protected override string GetEntityName()
        {
            return "Submittal";
        }

        #endregion

        #region GetKeyFieldName

        protected override string GetKeyFieldName()
        {
            return SubmittalFactory.FieldNames.SubmittalId;
        }

        #endregion

        #region BuildChildCallbacks

        protected override void BuildChildCallbacks()
        {
            this.ChildCallbacks.Add(SubmittalFactory.FieldNames.EmployeeId,
                this.AppendFrom);
            this.ChildCallbacks.Add(SubmittalFactory.FieldNames.ProjectContactId,
                this.AppendTo);
           this.ChildCallbacks.Add("TrackingItems",
                delegate(Submittal submittal, object childKeyName)
                {
                    this.AppendTrackingItems(submittal);
                });
            base.BuildChildCallbacks();
        }

        #endregion

        #region Unit of Work Implementation

        protected override void PersistNewItem(Submittal item)
        {
            StringBuilder builder = new StringBuilder(100);
            builder.Append(string.Format("INSERT INTO Submittal ({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21}) ",
                SubmittalFactory.FieldNames.SubmittalId,
                ProjectFactory.FieldNames.ProjectId,
                SubmittalFactory.FieldNames.SpecificationSectionId,
                SubmittalFactory.FieldNames.SpecificationSectionPrimaryIndex,
                SubmittalFactory.FieldNames.SpecificationSectionSecondaryIndex,
                SubmittalFactory.FieldNames.ProjectContactId,
                SubmittalFactory.FieldNames.EmployeeId,
                SubmittalFactory.FieldNames.TotalPages,
                SubmittalFactory.FieldNames.DeliveryMethod,
                SubmittalFactory.FieldNames.OtherDeliveryMethod,
                SubmittalFactory.FieldNames.PhaseNumber,
                SubmittalFactory.FieldNames.Reimbursable,
                SubmittalFactory.FieldNames.Final,
                SubmittalFactory.FieldNames.DateReceived,
                SubmittalFactory.FieldNames.ContractNumber,
                SubmittalFactory.FieldNames.Remarks,
                SubmittalFactory.FieldNames.Action,
                SubmittalFactory.FieldNames.ItemStatusId,
                SubmittalFactory.FieldNames.DateToField,
                SubmittalFactory.FieldNames.RemainderLocation,
                SubmittalFactory.FieldNames.RemainderUnderSubmittalNumber,
                SubmittalFactory.FieldNames.OtherRemainderLocation));
            builder.Append(string.Format("VALUES ({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21});",
                DataHelper.GetSqlValue(item.Key),
                DataHelper.GetSqlValue(item.ProjectKey),
                DataHelper.GetSqlValue(item.SpecSection.Key),
                DataHelper.GetSqlValue(item.SpecSectionPrimaryIndex),
                DataHelper.GetSqlValue(item.SpecSectionSecondaryIndex),
                DataHelper.GetSqlValue(item.To.Key),
                DataHelper.GetSqlValue(item.From.Key),
                DataHelper.GetSqlValue(item.TotalPages),
                DataHelper.GetSqlValue(item.DeliveryMethod),
                DataHelper.GetSqlValue(item.OtherDeliveryMethod),
                DataHelper.GetSqlValue(item.PhaseNumber),
                DataHelper.GetSqlValue(item.Reimbursable),
                DataHelper.GetSqlValue(item.Final),
                DataHelper.GetSqlValue(item.DateReceived),
                DataHelper.GetSqlValue(item.ContractNumber),
                DataHelper.GetSqlValue(item.Remarks),
                DataHelper.GetSqlValue(item.Action),
                DataHelper.GetSqlValue(item.Status.Id),
                DataHelper.GetSqlValue(item.DateToField),
                DataHelper.GetSqlValue(item.RemainderLocation),
                DataHelper.GetSqlValue(item.RemainderUnderSubmittalNumber),
                DataHelper.GetSqlValue(item.OtherRemainderLocation)));

            this.Database.ExecuteNonQuery(
                this.Database.GetSqlStringCommand(builder.ToString()));

            // Now do the child objects
            this.InsertCopyToList(item);
            this.InsertRoutingItems(item);
            this.InsertTrackingItems(item);
        }

        protected override void PersistUpdatedItem(Submittal item)
        {
            StringBuilder builder = new StringBuilder(100);
            builder.Append("UPDATE Submittal SET ");

            builder.Append(string.Format("{0} = {1}",
                SubmittalFactory.FieldNames.SpecificationSectionId,
                DataHelper.GetSqlValue(item.SpecSection.Key)));

            builder.Append(string.Format(",{0} = {1}",
                SubmittalFactory.FieldNames.SpecificationSectionPrimaryIndex,
                DataHelper.GetSqlValue(item.SpecSectionPrimaryIndex)));

            builder.Append(string.Format(",{0} = {1}",
                SubmittalFactory.FieldNames.SpecificationSectionSecondaryIndex,
                DataHelper.GetSqlValue(item.SpecSectionSecondaryIndex)));

            builder.Append(string.Format(",{0} = {1}",
                SubmittalFactory.FieldNames.ProjectContactId,
                DataHelper.GetSqlValue(item.To.Key)));

            builder.Append(string.Format(",{0} = {1}",
                SubmittalFactory.FieldNames.EmployeeId,
                DataHelper.GetSqlValue(item.From.Key)));

            builder.Append(string.Format(",{0} = {1}",
                SubmittalFactory.FieldNames.TotalPages,
                DataHelper.GetSqlValue(item.TotalPages)));

            builder.Append(string.Format(",{0} = {1}",
                SubmittalFactory.FieldNames.DeliveryMethod,
                DataHelper.GetSqlValue(item.DeliveryMethod)));

            builder.Append(string.Format(",{0} = {1}",
                SubmittalFactory.FieldNames.OtherDeliveryMethod,
                DataHelper.GetSqlValue(item.OtherDeliveryMethod)));

            builder.Append(string.Format(",{0} = {1}",
                SubmittalFactory.FieldNames.PhaseNumber,
                DataHelper.GetSqlValue(item.PhaseNumber)));

            builder.Append(string.Format(",{0} = {1}",
                SubmittalFactory.FieldNames.Reimbursable,
                DataHelper.GetSqlValue(item.Reimbursable)));

            builder.Append(string.Format(",{0} = {1}",
                SubmittalFactory.FieldNames.Final,
                DataHelper.GetSqlValue(item.Final)));

            builder.Append(string.Format(",{0} = {1}",
                SubmittalFactory.FieldNames.DateReceived,
                DataHelper.GetSqlValue(item.DateReceived)));

            builder.Append(string.Format(",{0} = {1}",
                SubmittalFactory.FieldNames.ContractNumber,
                DataHelper.GetSqlValue(item.ContractNumber)));

            builder.Append(string.Format(",{0} = {1}",
                SubmittalFactory.FieldNames.Remarks,
                DataHelper.GetSqlValue(item.Remarks)));

            builder.Append(string.Format(",{0} = {1}",
                SubmittalFactory.FieldNames.Action,
                DataHelper.GetSqlValue(item.Action)));

            builder.Append(string.Format(",{0} = {1}",
                SubmittalFactory.FieldNames.ItemStatusId,
                DataHelper.GetSqlValue(item.Status.Id)));

            builder.Append(string.Format(",{0} = {1}",
                SubmittalFactory.FieldNames.DateToField,
                DataHelper.GetSqlValue(item.DateToField)));

            builder.Append(string.Format(",{0} = {1}",
                SubmittalFactory.FieldNames.RemainderLocation,
                DataHelper.GetSqlValue(item.RemainderLocation)));

            builder.Append(string.Format(",{0} = {1}",
                SubmittalFactory.FieldNames.RemainderUnderSubmittalNumber,
                DataHelper.GetSqlValue(item.RemainderUnderSubmittalNumber)));

            builder.Append(string.Format(",{0} = {1}",
                SubmittalFactory.FieldNames.OtherRemainderLocation,
                DataHelper.GetSqlValue(item.OtherRemainderLocation)));

            builder.Append(" ");
            builder.Append(this.BuildBaseWhereClause(item.Key));

            this.Database.ExecuteNonQuery(
                this.Database.GetSqlStringCommand(builder.ToString()));

            // Now do the child objects

            // First, delete the existing ones
            this.DeleteCopyToList(item);
            this.DeleteRoutingItems(item);
            this.DeleteTrackingItems(item);

            // Now, add the current ones
            this.InsertCopyToList(item);
            this.InsertRoutingItems(item);
            this.InsertTrackingItems(item);
        }

        protected override void PersistDeletedItem(Submittal item)
        {
            // Delete the child objects first
            this.DeleteTrackingItems(item);

            // Now delete the submittal and its associated 
            // transmittal objects
            base.PersistDeletedItem(item);
        }

        #endregion

        #region Private Callback and Helper Methods

        private void AppendFrom(Submittal submittal, object fromEmployeeId)
        {
            submittal.From = EmployeeService.GetEmployee(fromEmployeeId);
        }

        private void AppendTo(Submittal submittal, object fromProjectContactKey)
        {
            submittal.To = ProjectService.GetProjectContact(submittal.ProjectKey, 
                fromProjectContactKey);
        }

        private void AppendTrackingItems(Submittal submittal)
        {
            StringBuilder builder = new StringBuilder(100);
            builder.Append("SELECT * FROM SubmittalTrackingItem sti");
            builder.Append(" INNER JOIN SpecificationSection ss ON ");
            builder.Append(" sti.SpecificationSectionID = ss.SpecificationSectionID ");
            builder.Append(string.Format(" WHERE SubmittalID = '{0}';", 
                submittal.Key));
            using (IDataReader reader = this.ExecuteReader(builder.ToString()))
            {
                while (reader.Read())
                {
                    submittal.TrackingItems.Add(
                        SubmittalFactory.BuildTrackingItem(reader));
                }
            }
        }

        private void DeleteTrackingItems(Submittal submittal)
        {
            string query = string.Format("DELETE FROM SubmittalTrackingItem {0}",
                this.BuildBaseWhereClause(submittal.Key));
            this.Database.ExecuteNonQuery(
                this.Database.GetSqlStringCommand(query));
        }

        private void InsertTrackingItems(Submittal submittal)
        {
            foreach (TrackingItem item in submittal.TrackingItems)
            {
                this.InsertTrackingItem(item, submittal.Key);
            }
        }

        private void InsertTrackingItem(TrackingItem item, object key)
        {
            StringBuilder builder = new StringBuilder(100);
            builder.Append(string.Format("INSERT INTO SubmittalTrackingItem ({0},{1},{2},{3},{4},{5},{6},{7}) ",
                SubmittalFactory.FieldNames.SubmittalId,
                SubmittalFactory.FieldNames.TotalItemsReceived,
                SubmittalFactory.FieldNames.TotalItemsSent,
                SubmittalFactory.FieldNames.DeferredApproval,
                SubmittalFactory.FieldNames.SubstitutionNumber,
                SubmittalFactory.FieldNames.SpecificationSectionId,
                SubmittalFactory.FieldNames.Description,
                SubmittalFactory.FieldNames.Status));
            builder.Append(string.Format("VALUES ({0},{1},{2},{3},{4},{5},{6},{7});",
                DataHelper.GetSqlValue(key),
                DataHelper.GetSqlValue(item.TotalItemsReceived),
                DataHelper.GetSqlValue(item.TotalItemsSent),
                DataHelper.GetSqlValue(item.DeferredApproval),
                DataHelper.GetSqlValue(item.SubstitutionNumber),
                DataHelper.GetSqlValue(item.SpecSection.Key),
                DataHelper.GetSqlValue(item.Description),
                DataHelper.GetSqlValue(item.Status)));

            this.Database.ExecuteNonQuery(
                this.Database.GetSqlStringCommand(builder.ToString()));
        }

        #endregion

        #region ISubmittalRepository Members

        public IList<Submittal> FindBy(Project project)
        {
            StringBuilder builder = this.GetBaseQueryBuilder();
            builder.Append(string.Format(" WHERE s.ProjectID = '{0}';", project.Key));
            return this.BuildEntitiesFromSql(builder.ToString());
        }

        public IList<SpecificationSection> FindAllSpecificationSections()
        {
            List<SpecificationSection> specSections = new List<SpecificationSection>();
            string query = "SELECT * FROM SpecificationSection";
            using (IDataReader reader = this.ExecuteReader(query))
            {
                while (reader.Read())
                {
                    specSections.Add(SubmittalFactory.BuildSpecSection(reader));
                }
            }
            return specSections;
        }

        public IList<ItemStatus> FindAllItemStatuses()
        {
            List<ItemStatus> statuses = new List<ItemStatus>();
            string query = "SELECT * FROM ItemStatus";
            using (IDataReader reader = this.ExecuteReader(query))
            {
                while (reader.Read())
                {
                    statuses.Add(TransmittalFactory.BuildItemStatus(reader));
                }
            }
            return statuses;
        }

        public IList<Discipline> FindAllDisciplines()
        {
            List<Discipline> disciplines = new List<Discipline>();
            string query = "SELECT * FROM Discipline";
            using (IDataReader reader = this.ExecuteReader(query))
            {
                while (reader.Read())
                {
                    disciplines.Add(TransmittalFactory.BuildDiscipline(reader));
                }
            }
            return disciplines;
        }

        #endregion
    }
}
