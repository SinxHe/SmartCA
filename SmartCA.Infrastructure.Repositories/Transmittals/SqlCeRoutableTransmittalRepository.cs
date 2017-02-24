using System.Data;
using System.Text;
using SmartCA.Infrastructure.DomainBase;
using SmartCA.Infrastructure.Helpers;
using SmartCA.Model.Transmittals;

namespace SmartCA.Infrastructure.Repositories.Transmittals
{
    public abstract class SqlCeRoutableTransmittalRepository<T> : 
        SqlCeTransmittalRepository<T>
        where T : IAggregateRoot, IRoutableTransmittal
    {
        #region Private Fields

        #endregion

        #region Constructors

        protected SqlCeRoutableTransmittalRepository()
            : this(null)
        {
        }

        protected SqlCeRoutableTransmittalRepository(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }

        #endregion

        #region Abstract Methods

        #endregion

        #region Properties

        #endregion

        #region BuildChildCallbacks

        protected override void BuildChildCallbacks()
        {
            this.ChildCallbacks.Add("RoutingItems",
                delegate(T transmittal, object childKeyName)
                {
                    this.AppendRoutingItems(transmittal);
                });
            base.BuildChildCallbacks();
        }

        #endregion

        #region Protected Methods

        protected void AppendRoutingItems(T transmittal)
        {
            StringBuilder builder = new StringBuilder(100);
            builder.Append(string.Format("SELECT * FROM {0}RoutingItem tri ", this.EntityName));
            builder.Append(" INNER JOIN RoutingItem ri ON");
            builder.Append(" tri.RoutingItemID = ri.RoutingItemID");
            builder.Append(" INNER JOIN Discipline d ON");
            builder.Append(" ri.DisciplineID = d.DisciplineID");
            builder.Append(string.Format(" WHERE tri.{0} = '{1}';",
                this.KeyFieldName, transmittal.Key));
            using (IDataReader reader = this.ExecuteReader(builder.ToString()))
            {
                while (reader.Read())
                {
                    transmittal.RoutingItems.Add(TransmittalFactory.BuildRoutingItem(
                        transmittal.ProjectKey, reader));
                }
            }
        }

        protected void DeleteRoutingItems(T transmittal)
        {
            string query = string.Format("DELETE FROM {0}RoutingItem {1}",
                this.EntityName, this.BuildBaseWhereClause(transmittal.Key));
            this.Database.ExecuteNonQuery(
                this.Database.GetSqlStringCommand(query));
            foreach (RoutingItem item in transmittal.RoutingItems)
            {
                this.DeleteRoutingItem(item);
            }
        }

        protected void InsertRoutingItems(T transmittal)
        {
            foreach (RoutingItem item in transmittal.RoutingItems)
            {
                this.InsertRoutingItem(item, transmittal.Key);
            }
        }

        protected override void PersistDeletedItem(T transmittal)
        {
            // Delete the child objects first
            this.DeleteCopyToList(transmittal);
            this.DeleteRoutingItems(transmittal);

            // Delete the transmittal entity
            base.PersistDeletedItem(transmittal);
        }

        #endregion

        #region Private Methods

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
    }
}
