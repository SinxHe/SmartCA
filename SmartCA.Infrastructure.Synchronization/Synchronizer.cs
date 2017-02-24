using System;
using SmartCA.Infrastructure.Transactions;
using System.Collections.Generic;
using SmartCA.Infrastructure.DomainBase;
using SmartCA.DataContracts.Helpers;
using SmartCA.Infrastructure.RepositoryFramework;
using System.Reflection;
using SmartCA.Infrastructure.ReferenceData;

namespace SmartCA.Infrastructure.Synchronization
{
    public static class Synchronizer
    {
        private static DateTime? lastSynchronized;

        static Synchronizer()
        {
            Synchronizer.GetLastSynchronized();
        }

        private static void GetLastSynchronized()
        {
            Synchronizer.lastSynchronized = 
                ClientTransactionService.GetLastSynchronization();
        }

        private static void SetLastSynchronized()
        {
            // Persist the last synchronized datetime
            Synchronizer.lastSynchronized = DateTime.Now;
            ClientTransactionService.SetLastSynchronization(
                Synchronizer.lastSynchronized);
        }

        public static void Synchronize()
        {
            // Send pending transactions to the server
            SynchronizationServerProxy.SendTransactions(
                ClientTransactionService.GetPendingTransactions());

            // Get reference data from the server
            ReferenceDataContract referenceData = 
                SynchronizationServerProxy.GetReferenceData(lastSynchronized);

            // Process the reference data 
            Synchronizer.ProcessReferenceData(referenceData);

            // Get transactions from the server
            IList<ServerTransaction> serverTransactions = 
                SynchronizationServerProxy.GetTransactions(lastSynchronized);

            // Process the server transactions
            Synchronizer.ProcessServerTransactions(serverTransactions);

            // If the synchronization was successful, 
            // then record the timestamp
            Synchronizer.SetLastSynchronized();
        }

        private static void ProcessReferenceData(
            ReferenceDataContract referenceData)
        {
            if (referenceData != null)
            {
                IReferenceDataRepository repository = 
                    ReferenceDataRepositoryFactory.GetReferenceDataRepository();

                if (referenceData.Disciplines != null)
                {
                    repository.Add(referenceData.Disciplines);
                }
                if (referenceData.ItemStatuses != null)
                {
                    repository.Add(referenceData.ItemStatuses);
                }
                if (referenceData.Sectors != null)
                {
                    repository.Add(referenceData.Sectors);
                }
                if (referenceData.Segments != null)
                {
                    repository.Add(referenceData.Segments);
                }
                if (referenceData.SpecSections != null)
                {
                    repository.Add(referenceData.SpecSections);
                }
            }
        }

        private static void ProcessServerTransactions(
            IList<ServerTransaction> serverTransactions)
        {
            IEntity entity = null;
            Type serviceType = null;
            string saveMethodName = string.Empty;
            MethodInfo method = null;
            
            foreach (ServerTransaction transaction in serverTransactions)
            {
                // Convert the DataContract into an Entity
                // and use the right service class to save it

                // 1. Get the Entity from the DataContract
                entity = Converter.ToEntity(transaction.Contract);
                
                // 2. Get the right service class type for the entity
                serviceType = Type.GetType(string.Format("{0}Service", 
                    entity.GetType().Name));

                // 3. Use reflection to get the correct Save method
                saveMethodName = string.Format("Save{0}", entity.GetType().Name);
                method = serviceType.GetMethod("Save");
                
                // 4. Call the Save method
                method.Invoke(null, new object[] { entity });
            }
        }
    }
}
