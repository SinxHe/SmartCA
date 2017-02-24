using System;
using System.Collections.Generic;
using SmartCA.Infrastructure.RepositoryFramework;

namespace SmartCA.Infrastructure.Transactions
{
    public static class ClientTransactionService
    {
        private static IClientTransactionRepository repository = 
            ClientTransactionRepositoryFactory.GetTransactionRepository();

        public static IList<ClientTransaction> GetPendingTransactions()
        {
            return ClientTransactionService.repository.FindPending();
        }

        public static DateTime? GetLastSynchronization()
        {
            return ClientTransactionService.repository.GetLastSynchronization();
        }

        public static void SetLastSynchronization(DateTime? lastSynchronization)
        {
            ClientTransactionService.repository.SetLastSynchronization(lastSynchronization);
        }
    }
}
