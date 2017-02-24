using System.Data;
using System.Text;
using SmartCA.Infrastructure.DomainBase;
using SmartCA.Infrastructure.Helpers;
using SmartCA.Model.Transmittals;

namespace SmartCA.Infrastructure.Repositories.Transmittals
{
    public abstract class SqlCeTransmittalRepository<T> : SqlCeRepositoryBase<T>
        where T : IAggregateRoot, ITransmittal
    {
        #region Private Fields

        #endregion

        #region Constructors

        protected SqlCeTransmittalRepository()
            : this(null)
        {
        }

        protected SqlCeTransmittalRepository(IUnitOfWork unitOfWork) 
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
            this.ChildCallbacks.Add("CopyToList",
                delegate(T transmittal, object childKeyName)
                {
                    this.AppendCopyToList(transmittal);
                });
        }

        #endregion

        #region Protected Methods
        
        protected void AppendCopyToList(T transmittal)
        {
            StringBuilder builder = new StringBuilder(100);
            builder.Append(string.Format("SELECT * FROM {0}CopyList", 
                this.EntityName));
            builder.Append(string.Format(" WHERE {0} = '{1}';", 
                this.KeyFieldName,
                transmittal.Key));
            using (IDataReader reader = this.ExecuteReader(builder.ToString()))
            {
                while (reader.Read())
                {
                    transmittal.CopyToList.Add(TransmittalFactory.BuildCopyTo(
                        transmittal.ProjectKey, reader));
                }
            }
        }

        protected void DeleteCopyToList(T transmittal)
        {
            string query = string.Format("DELETE FROM {0}CopyList {1}",
                this.EntityName, this.BuildBaseWhereClause(transmittal.Key));
            this.Database.ExecuteNonQuery(
                this.Database.GetSqlStringCommand(query));
        }

        protected void InsertCopyToList(T transmittal)
        {
            foreach (CopyTo copyTo in transmittal.CopyToList)
            {
                this.InsertCopyTo(copyTo, transmittal.Key);
            }
        }

        #endregion

        #region Private Methods

        private void InsertCopyTo(CopyTo copyTo, object key)
        {
            StringBuilder builder = new StringBuilder(100);
            builder.Append(string.Format("INSERT INTO {0}CopyList ({1},{2},{3}) ",
                this.EntityName,
                this.KeyFieldName,
                TransmittalFactory.FieldNames.ProjectContactId,
                TransmittalFactory.FieldNames.Notes));
            builder.Append(string.Format("VALUES ({0},{1},{2});",
                DataHelper.GetSqlValue(key),
                DataHelper.GetSqlValue(copyTo.Contact.Key),
                DataHelper.GetSqlValue(copyTo.Notes)));

            this.Database.ExecuteNonQuery(
                this.Database.GetSqlStringCommand(builder.ToString()));
        }

        #endregion
    }
}
