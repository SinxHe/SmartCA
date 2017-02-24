using System;
using System.Collections.Generic;
using SmartCA.Infrastructure.Transactions;

namespace SmartCA.Infrastructure.Synchronization
{
    public static class SynchronizationServerProxy
    {
        public static void SendTransactions(
            IList<ClientTransaction> transactions)
        {
        }

        public static IList<ServerTransaction> GetTransactions(
            DateTime? lastSynchronized)
        {
            return new List<ServerTransaction>();
        }

        public static ReferenceDataContract GetReferenceData(
            DateTime? lastSynchronized)
        {
            return null;
        }
    }
}
