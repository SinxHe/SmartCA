using System;
using SmartCA.Infrastructure.DomainBase;
using SmartCA.Infrastructure.Transactions;
using SmartCA.DataContracts.Helpers;
using System.Collections.Generic;

namespace SmartCA.Infrastructure.Repositories
{
    public abstract class ClientTransactionRepository : 
        IClientTransactionRepository
    {
        #region IClientTransactionRepository Members

        public abstract DateTime? GetLastSynchronization();
        public abstract void SetLastSynchronization(DateTime? lastSynchronization);

        public void Add(ClientTransaction transaction)
        {
            // Convert the entity to one of the data contract types 
            object contract = Converter.ToContract(transaction.Entity);

            // Serialize the data contract into an array of bytes
            byte[] serializedContractData = Serializer.Serialize(contract);

            // Persist the transaction (delegate to the derived class)
            this.PersistNewTransaction(transaction.Type, 
                serializedContractData, transaction.Key);
        }

        public abstract IList<ClientTransaction> FindPending();

        #endregion

        protected abstract void PersistNewTransaction(TransactionType type,
            byte[] serializedContractData, object transactionKey);
    }
}
