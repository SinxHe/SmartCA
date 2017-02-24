using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using SmartCA.Infrastructure.DomainBase;
using SmartCA.Infrastructure.EntityFactoryFramework;
using SmartCA.Infrastructure.Helpers;
using SmartCA.Infrastructure.RepositoryFramework;

namespace SmartCA.Infrastructure.Repositories
{
    public abstract class SqlCeRepositoryBase<T> : RepositoryBase<T>
        where T : IAggregateRoot
    {
        #region AppendChildData Delegate

        /// <summary>
        /// The delegate signature required for callback methods
        /// </summary>
        /// <param name="entityAggregate"></param>
        /// <param name="childEntityKey"></param>
        public delegate void AppendChildData(T entityAggregate,
            object childEntityKeyValue);

        #endregion

        #region Private Fields

        private Database database;
        private IEntityFactory<T> entityFactory;
        private Dictionary<string, AppendChildData> childCallbacks;
        private string baseQuery;
        private string baseWhereClause;
        private string entityName;
        private string keyFieldName;

        #endregion

        #region Constructors

        protected SqlCeRepositoryBase()
            : this(null)
        {
        }

        protected SqlCeRepositoryBase(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            this.database = DatabaseFactory.CreateDatabase();
            this.entityFactory = EntityFactoryBuilder.BuildFactory<T>();
            this.childCallbacks = new Dictionary<string, AppendChildData>();
            this.BuildChildCallbacks();
            this.baseQuery = this.GetBaseQuery();
            this.baseWhereClause = this.GetBaseWhereClause();
            this.entityName = this.GetEntityName();
            this.keyFieldName = this.GetKeyFieldName();
        }

        #endregion

        #region Abstract Methods

        protected abstract string GetBaseQuery();
        protected abstract string GetBaseWhereClause();
        protected abstract string GetEntityName();
        protected abstract string GetKeyFieldName();
        protected abstract void BuildChildCallbacks();
        protected abstract override void PersistNewItem(T item);
        protected abstract override void PersistUpdatedItem(T item);

        #endregion

        #region Properties

        protected Database Database
        {
            get { return this.database; }
        }

        protected Dictionary<string, AppendChildData> ChildCallbacks
        {
            get { return this.childCallbacks; }
        }

        internal string BaseQuery
        {
            get { return this.baseQuery; }
        }

        protected string EntityName
        {
            get { return this.entityName; }
        }

        protected string KeyFieldName
        {
            get { return this.keyFieldName; }
        }

        #endregion

        #region Public Methods

        public override IList<T> FindAll()
        {
            StringBuilder builder = this.GetBaseQueryBuilder();
            builder.Append(";");
            return this.BuildEntitiesFromSql(builder.ToString());
        }

        public override T FindBy(object key)
        {
            StringBuilder builder = this.GetBaseQueryBuilder();
            builder.Append(this.BuildBaseWhereClause(key));
            return this.BuildEntityFromSql(builder.ToString());
        }

        #endregion

        #region Internal Methods

        internal List<T> BuildEntitiesFromSql(StringBuilder builder)
        {
            return this.BuildEntitiesFromSql(builder.ToString());
        }

        #endregion

        #region Protected Methods

        protected IDataReader ExecuteReader(string sql)
        {
            DbCommand command = this.database.GetSqlStringCommand(sql);
            return this.database.ExecuteReader(command);
        }

        protected virtual T BuildEntityFromSql(string sql)
        {
            T entity = default(T);
            using (IDataReader reader = this.ExecuteReader(sql))
            {
                if (reader.Read())
                {
                    entity = this.BuildEntityFromReader(reader);
                }
            }
            return entity;
        }

        protected virtual T BuildEntityFromReader(IDataReader reader)
        {
            T entity = this.entityFactory.BuildEntity(reader);
            if (this.childCallbacks != null && this.childCallbacks.Count > 0)
            {
                object childKeyValue = null;
                DataTable columnData = reader.GetSchemaTable();
                foreach (string childKeyName in this.childCallbacks.Keys)
                {
                    if (DataHelper.ReaderContainsColumnName(columnData,
                        childKeyName))
                    {
                        childKeyValue = reader[childKeyName];
                    }
                    else
                    {
                        childKeyValue = null;
                    }
                    this.childCallbacks[childKeyName](entity, childKeyValue);
                }
            }
            return entity;
        }

        protected virtual List<T> BuildEntitiesFromSql(string sql)
        {
            List<T> entities = new List<T>();
            using (IDataReader reader = this.ExecuteReader(sql))
            {
                while (reader.Read())
                {
                    entities.Add(this.BuildEntityFromReader(reader));
                }
            }
            return entities;
        }

        protected virtual string BuildBaseWhereClause(object key)
        {
            return string.Format(this.baseWhereClause, key);
        }

        protected virtual StringBuilder GetBaseQueryBuilder()
        {
            StringBuilder builder = new StringBuilder(50);
            builder.Append(this.baseQuery);
            return builder;
        }

        protected override void PersistDeletedItem(T item)
        {
            // Delete the Entity
            string query = string.Format("DELETE FROM {0} {1}",
                this.entityName,
                this.BuildBaseWhereClause(item.Key));
            this.Database.ExecuteNonQuery(
                this.Database.GetSqlStringCommand(query));
        }

        #endregion

        #region Private Helper Methods

        #endregion
    }
}