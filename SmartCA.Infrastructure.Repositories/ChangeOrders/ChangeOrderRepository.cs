using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using SmartCA.Infrastructure.Helpers;
using SmartCA.Infrastructure.Repositories.NumberedProjectChildren;
using SmartCA.Model.ChangeOrders;
using SmartCA.Model.Companies;
using SmartCA.Model.Projects;
using SmartCA.Model.Transmittals;

namespace SmartCA.Infrastructure.Repositories
{
    public class ChangeOrderRepository : SqlCeRepositoryBase<ChangeOrder>,
        IChangeOrderRepository
    {
        #region Private Fields

        #endregion

        #region Public Constructors

        public ChangeOrderRepository()
            : this(null)
        {
        }

        public ChangeOrderRepository(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }

        #endregion

        #region GetBaseQuery

        protected override string GetBaseQuery()
        {
            StringBuilder builder = new StringBuilder(200);
            builder.Append("SELECT * FROM ChangeOrder co ");
            builder.Append("INNER JOIN ItemStatus ist ");
            builder.Append("ON co.ItemStatusID = ist.ItemStatusID");
            return builder.ToString();
        }

        #endregion

        #region GetBaseWhereClause

        protected override string GetBaseWhereClause()
        {
            return " WHERE ChangeOrderID = '{0}';";
        }

        #endregion

        #region GetEntityName

        protected override string GetEntityName()
        {
            return "ChangeOrder";
        }

        #endregion

        #region GetKeyFieldName

        protected override string GetKeyFieldName()
        {
            return ChangeOrderFactory.FieldNames.ChangeOrderId;
        }

        #endregion

        #region BuildChildCallbacks

        protected override void BuildChildCallbacks()
        {
            this.ChildCallbacks.Add(CompanyFactory.FieldNames.CompanyId,
                this.AppendContractor);
            this.ChildCallbacks.Add("RoutingItems",
                delegate(ChangeOrder co, object childKeyName)
                {
                    this.AppendRoutingItems(co);
                });
        }

        #endregion

        #region Unit of Work Implementation

        protected override void PersistNewItem(ChangeOrder item)
        {
            StringBuilder builder = new StringBuilder(100);
            builder.Append(string.Format("INSERT INTO ChangeOrder ({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16}) ",
                ChangeOrderFactory.FieldNames.ChangeOrderId,
                ProjectFactory.FieldNames.ProjectId,
                ChangeOrderFactory.FieldNames.ChangeOrderNumber,
                ChangeOrderFactory.FieldNames.EffectiveDate,
                CompanyFactory.FieldNames.CompanyId,
                ChangeOrderFactory.FieldNames.Description,
                ChangeOrderFactory.FieldNames.PriceChangeType,
                ChangeOrderFactory.FieldNames.PriceChangeTypeDirection,
                ChangeOrderFactory.FieldNames.AmountChanged,
                ChangeOrderFactory.FieldNames.TimeChangeDirection,
                ChangeOrderFactory.FieldNames.TimeChangedDays,
                ChangeOrderFactory.FieldNames.ItemStatusId,
                ChangeOrderFactory.FieldNames.AgencyApprovedDate,
                ChangeOrderFactory.FieldNames.DateToField,
                ChangeOrderFactory.FieldNames.OwnerSignatureDate,
                ChangeOrderFactory.FieldNames.ArchitectSignatureDate,
                ChangeOrderFactory.FieldNames.ContractorSignatureDate
                ));
            builder.Append(string.Format("VALUES ({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16});",
                DataHelper.GetSqlValue(item.Key),
                DataHelper.GetSqlValue(item.ProjectKey),
                DataHelper.GetSqlValue(item.Number),
                DataHelper.GetSqlValue(item.EffectiveDate),
                DataHelper.GetSqlValue(item.Contractor.Key),
                DataHelper.GetSqlValue(item.Description),
                DataHelper.GetSqlValue(item.ChangeType),
                DataHelper.GetSqlValue(item.PriceChangeDirection),
                DataHelper.GetSqlValue(item.AmountChanged),
                DataHelper.GetSqlValue(item.TimeChangeDirection),
                DataHelper.GetSqlValue(item.TimeChanged),
                DataHelper.GetSqlValue(item.Status.Id),
                DataHelper.GetSqlValue(item.AgencyApprovedDate),
                DataHelper.GetSqlValue(item.DateToField),
                DataHelper.GetSqlValue(item.OwnerSignatureDate),
                DataHelper.GetSqlValue(item.ArchitectSignatureDate),
                DataHelper.GetSqlValue(item.ContractorSignatureDate)));

            this.Database.ExecuteNonQuery(
                this.Database.GetSqlStringCommand(builder.ToString()));

            // Now do the child objects
            this.InsertRoutingItems(item);
        }

        protected override void PersistUpdatedItem(ChangeOrder item)
        {
            StringBuilder builder = new StringBuilder(100);
            builder.Append("UPDATE ChangeOrder SET ");

            builder.Append(string.Format("{0} = {1}",
                ChangeOrderFactory.FieldNames.ChangeOrderNumber,
                DataHelper.GetSqlValue(item.Number)));

            builder.Append(string.Format(",{0} = {1}",
                ChangeOrderFactory.FieldNames.EffectiveDate,
                DataHelper.GetSqlValue(item.EffectiveDate)));

            builder.Append(string.Format(",{0} = {1}",
                CompanyFactory.FieldNames.CompanyId,
                DataHelper.GetSqlValue(item.Contractor.Key)));

            builder.Append(string.Format(",{0} = {1}",
                ChangeOrderFactory.FieldNames.Description,
                DataHelper.GetSqlValue(item.Description)));

            builder.Append(string.Format(",{0} = {1}",
                ChangeOrderFactory.FieldNames.PriceChangeType,
                DataHelper.GetSqlValue(item.ChangeType)));

            builder.Append(string.Format(",{0} = {1}",
                ChangeOrderFactory.FieldNames.PriceChangeTypeDirection,
                DataHelper.GetSqlValue(item.PriceChangeDirection)));

            builder.Append(string.Format(",{0} = {1}",
                ChangeOrderFactory.FieldNames.AmountChanged,
                DataHelper.GetSqlValue(item.AmountChanged)));

            builder.Append(string.Format(",{0} = {1}",
                ChangeOrderFactory.FieldNames.TimeChangeDirection,
                DataHelper.GetSqlValue(item.TimeChangeDirection)));

            builder.Append(string.Format(",{0} = {1}",
                ChangeOrderFactory.FieldNames.TimeChangedDays,
                DataHelper.GetSqlValue(item.TimeChanged)));

            builder.Append(string.Format(",{0} = {1}",
                ChangeOrderFactory.FieldNames.ItemStatusId,
                DataHelper.GetSqlValue(item.Status.Id)));

            builder.Append(string.Format(",{0} = {1}",
                ChangeOrderFactory.FieldNames.AgencyApprovedDate,
                DataHelper.GetSqlValue(item.AgencyApprovedDate)));

            builder.Append(string.Format(",{0} = {1}",
                ChangeOrderFactory.FieldNames.DateToField,
                DataHelper.GetSqlValue(item.DateToField)));

            builder.Append(string.Format(",{0} = {1}",
                ChangeOrderFactory.FieldNames.OwnerSignatureDate,
                DataHelper.GetSqlValue(item.OwnerSignatureDate)));

            builder.Append(string.Format(",{0} = {1}",
                ChangeOrderFactory.FieldNames.ArchitectSignatureDate,
                DataHelper.GetSqlValue(item.ArchitectSignatureDate)));        

            builder.Append(string.Format(",{0} = {1}",
                ChangeOrderFactory.FieldNames.ContractorSignatureDate,
                DataHelper.GetSqlValue(item.ContractorSignatureDate)));

            builder.Append(" ");
            builder.Append(this.BuildBaseWhereClause(item.Key));

            this.Database.ExecuteNonQuery(
                this.Database.GetSqlStringCommand(builder.ToString()));

            // Now do the child objects

            // First, delete the existing ones
            this.DeleteRoutingItems(item);
            
            // Now, add the current ones
            this.InsertRoutingItems(item);
        }

        #endregion

        #region Private Callback and Helper Methods

        private void AppendRoutingItems(ChangeOrder co)
        {
            StringBuilder builder = new StringBuilder(100);
            builder.Append(string.Format("SELECT * FROM {0}RoutingItem tri ", 
                this.EntityName));
            builder.Append(" INNER JOIN RoutingItem ri ON");
            builder.Append(" tri.RoutingItemID = ri.RoutingItemID");
            builder.Append(" INNER JOIN Discipline d ON");
            builder.Append(" ri.DisciplineID = d.DisciplineID");
            builder.Append(string.Format(" WHERE tri.{0} = '{1}';",
                this.KeyFieldName, co.Key));
            using (IDataReader reader = this.ExecuteReader(builder.ToString()))
            {
                while (reader.Read())
                {
                    co.RoutingItems.Add(TransmittalFactory.BuildRoutingItem(
                        co.ProjectKey, reader));
                }
            }
        }

        private void AppendContractor(ChangeOrder co, object contractorKey)
        {
            co.Contractor = CompanyService.GetCompany(contractorKey);
        }

        private void DeleteRoutingItems(ChangeOrder co)
        {
            string query = string.Format("DELETE FROM {0}RoutingItem {1}",
                this.EntityName, this.BuildBaseWhereClause(co.Key));
            this.Database.ExecuteNonQuery(
                this.Database.GetSqlStringCommand(query));
            foreach (RoutingItem item in co.RoutingItems)
            {
                this.DeleteRoutingItem(item);
            }
        }

        private void InsertRoutingItems(ChangeOrder co)
        {
            foreach (RoutingItem item in co.RoutingItems)
            {
                this.InsertRoutingItem(item, co.Key);
            }
        }

        private void DeleteRoutingItem(RoutingItem item)
        {
            StringBuilder query = new StringBuilder(50);
            query.Append("DELETE FROM RoutingItem ");
            query.Append(string.Format("WHERE RoutingItemID = '{0}'", item.Key));
            this.Database.ExecuteNonQuery(
                this.Database.GetSqlStringCommand(query.ToString()));
        }

        private void InsertRoutingItem(RoutingItem item, object key)
        {
            StringBuilder builder = new StringBuilder(100);
            builder.Append(string.Format("INSERT INTO RoutingItem ({0},{1},{2},{3},{4},{5}) ",
                TransmittalFactory.FieldNames.RoutingItemId,
                TransmittalFactory.FieldNames.DisciplineId,
                TransmittalFactory.FieldNames.RoutingOrder,
                TransmittalFactory.FieldNames.ProjectContactId,
                TransmittalFactory.FieldNames.DateSent,
                TransmittalFactory.FieldNames.DateReturned));
            builder.Append(string.Format("VALUES ({0},{1},{2},{3},{4},{5});",
                DataHelper.GetSqlValue(item.Key),
                DataHelper.GetSqlValue(item.Discipline.Key),
                DataHelper.GetSqlValue(item.RoutingOrder),
                DataHelper.GetSqlValue(item.Recipient.Key),
                DataHelper.GetSqlValue(item.DateSent),
                DataHelper.GetSqlValue(item.DateReturned)));

            this.Database.ExecuteNonQuery(
                this.Database.GetSqlStringCommand(builder.ToString()));

            builder = new StringBuilder(50);
            builder.Append(string.Format("INSERT INTO {0}RoutingItem ({1},{2}) ",
                this.EntityName,
                this.KeyFieldName,
                TransmittalFactory.FieldNames.RoutingItemId));
            builder.Append(string.Format("VALUES ({0},{1});",
                DataHelper.GetSqlValue(key),
                DataHelper.GetSqlValue(item.Key)));

            this.Database.ExecuteNonQuery(
                this.Database.GetSqlStringCommand(builder.ToString()));
        }

        #endregion

        #region IChangeOrderRepository Members

        public IList<ChangeOrder> FindBy(Project project)
        {
            return
                NumberedProjectChildRepositoryHelper.FindBy
                <ChangeOrder>(this, project);
        }

        public decimal GetPreviousAuthorizedAmountFrom(ChangeOrder co)
        {
            StringBuilder builder = new StringBuilder(100);
            builder.Append("SELECT SUM(AmountChanged) FROM ChangeOrder ");
            builder.Append(string.Format("WHERE ProjectID = '{0}' ", 
                co.ProjectKey.ToString()));
            builder.Append(string.Format("AND ChangeOrderNumber < '{0}';", 
                co.Number));
            object previousAuthorizedAmountResult = 
                this.Database.ExecuteScalar(
                this.Database.GetSqlStringCommand(builder.ToString()));
            return (previousAuthorizedAmountResult != null && previousAuthorizedAmountResult != DBNull.Value) ? 
                Convert.ToDecimal(previousAuthorizedAmountResult) : 0;
        }

        public int GetPreviousTimeChangedTotalFrom(ChangeOrder co)
        {
            StringBuilder builder = new StringBuilder(100);
            builder.Append("SELECT SUM(TimeChangedDays) FROM ChangeOrder ");
            builder.Append(string.Format("WHERE ProjectID = '{0}' ",
                co.ProjectKey.ToString()));
            builder.Append(string.Format("AND ChangeOrderNumber < '{0}';",
                co.Number));
            object previousTimeChangedTotalResult =
                this.Database.ExecuteScalar(
                this.Database.GetSqlStringCommand(builder.ToString()));
            return (previousTimeChangedTotalResult != null && previousTimeChangedTotalResult != DBNull.Value) ? 
                Convert.ToInt32(previousTimeChangedTotalResult) : 0;
        }

        #endregion
    }
}
