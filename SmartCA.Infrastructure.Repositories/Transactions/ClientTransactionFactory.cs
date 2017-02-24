using System.Data;
using SmartCA.DataContracts;
using SmartCA.DataContracts.Helpers;
using SmartCA.Infrastructure.DomainBase;
using SmartCA.Infrastructure.EntityFactoryFramework;
using SmartCA.Infrastructure.Helpers;
using SmartCA.Infrastructure.Transactions;

namespace SmartCA.Infrastructure.Repositories
{
    public class ClientTransactionFactory : IEntityFactory<ClientTransaction>
    {

        internal static class FieldNames
        {
            public const string ClientTransactionId = "ClientTransactionID";
            public const string ReconciliationResult = "ReconciliationResult";
            public const string ReconciliationErrorMessage = 
                "ReconciliationErrorMessage";
            public const string TransactionType = "TransactionType";
            public const string ObjectData = "ObjectData";
        }
        #region IEntityFactory<ClientTransaction> Members

        public ClientTransaction BuildEntity(IDataReader reader)
        {
            byte[] objectData = 
                DataHelper.GetByteArrayValue(reader[FieldNames.ObjectData]);
            ContractBase contract = 
                Serializer.Deserialize(objectData) as ContractBase;
            IEntity entity = Converter.ToEntity(contract);
            return new ClientTransaction(reader[FieldNames.ClientTransactionId],
                (TransactionType)DataHelper.GetInteger(reader[FieldNames.TransactionType]),
                    entity);
        }

        #endregion
    }
}
