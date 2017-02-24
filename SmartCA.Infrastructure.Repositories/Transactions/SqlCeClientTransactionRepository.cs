using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using SmartCA.Infrastructure.EntityFactoryFramework;
using SmartCA.Infrastructure.Helpers;
using SmartCA.Infrastructure.Transactions;

namespace SmartCA.Infrastructure.Repositories
{
    public class SqlCeClientTransactionRepository : ClientTransactionRepository
    {
        private Database database;

        public SqlCeClientTransactionRepository()
        {
            this.database = DatabaseFactory.CreateDatabase();
        }

        protected override void PersistNewTransaction(TransactionType type,
            byte[] serializedContractData, object transactionKey)
        {
            // See if the parent transaction already exists

            // Perform a query to see if it exists
            StringBuilder builder = new StringBuilder(100);
            builder.Append("SELECT 1 FROM ClientTransaction ");
            builder.Append(string.Format("WHERE ClientTransactionID='{0}';",
                transactionKey));
            object result = this.database.ExecuteScalar(
                this.database.GetSqlStringCommand(builder.ToString()));

            // Test the result of the query to see if it exists
            if (result == null)
            {
                // It does not exist, so create a new parent transaction
                builder = new StringBuilder(100);
                builder.Append("INSERT INTO ClientTransaction ");
                builder.Append("(ClientTransactionID) ");
                builder.Append(string.Format("VALUES ('{0}');", transactionKey));
                this.database.ExecuteNonQuery(
                    this.database.GetSqlStringCommand(builder.ToString()));
            }

            // Insert the details of the transaction, 
            // including the serialized object's byte array
            builder = new StringBuilder(100);
            builder.Append("INSERT INTO ClientTransactionDetail ");
            builder.Append("(ClientTransactionID,TransactionType,ObjectData) ");
            builder.Append(string.Format("VALUES ('{0}',{1},@data);",
                transactionKey, (int)type));
            using (DbCommand command = 
                this.database.GetSqlStringCommand(builder.ToString()))
            {
                this.database.AddInParameter(command, "@data",
                    DbType.Binary, serializedContractData);
                this.database.ExecuteNonQuery(command);
            }
        }

        public override IList<ClientTransaction> FindPending()
        {
            List<ClientTransaction> transactions = new List<ClientTransaction>();
            StringBuilder builder = new StringBuilder(100);
            builder.Append("SELECT ctd.ClientTransactionID,");
            builder.Append("ctd.TransactionType,ctd.ObjectData ");
            builder.Append("FROM ClientTransaction ct ");
            builder.Append("INNER JOIN ClientTransactionDetail ctd ");
            builder.Append("ON ct.ClientTransactionID = ctd.ClientTransactionID ");
            builder.Append("WHERE ct.ReconciliationResult = 1;");
            using (DbCommand command = this.database.GetSqlStringCommand(builder.ToString()))
            {
                using (IDataReader reader = this.database.ExecuteReader(command))
                {
                    IEntityFactory<ClientTransaction> entityFactory =
                        EntityFactoryBuilder.BuildFactory<ClientTransaction>();
                    while (reader.Read())
                    {
                        transactions.Add(entityFactory.BuildEntity(reader));
                    }
                }
            }
            return transactions;
        }

        public override DateTime? GetLastSynchronization()
        {
            string query = "SELECT LastSynchronization FROM Synchronization";
            using (DbCommand command = 
                this.database.GetSqlStringCommand(query))
            {
                return DataHelper.GetNullableDateTime(
                    this.database.ExecuteScalar(command));
            }
        }

        public override void SetLastSynchronization(DateTime? lastSynchronization)
        {
            if (lastSynchronization.HasValue)
            {
                string query = "SELECT COUNT(*) FROM Synchronization";
                bool synchronizationRecordExists = false;
                using (DbCommand command = 
                    this.database.GetSqlStringCommand(query))
                {
                    synchronizationRecordExists = 
                        ((int)this.database.ExecuteScalar(command) > 0);
                }

                StringBuilder builder = new StringBuilder(50);

                if (synchronizationRecordExists)
                {
                    builder.Append("UPDATE Synchronization ");
                    builder.Append(
                        string.Format("SET LastSynchronization = '{0}'", 
                        lastSynchronization.Value));
                }
                else
                {
                    builder.Append("INSERT INTO Synchronization ");
                    builder.Append("(LastSynchronization) ");
                    builder.Append(string.Format("VALUES ('{0}')", 
                        lastSynchronization));
                }
                using (DbCommand command = 
                    this.database.GetSqlStringCommand(builder.ToString()))
                {
                    this.database.ExecuteNonQuery(command);
                }
            }
        }
    }
}
