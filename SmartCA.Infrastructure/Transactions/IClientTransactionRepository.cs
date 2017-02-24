using System;
using SmartCA.Infrastructure.DomainBase;
using System.Collections.Generic;

namespace SmartCA.Infrastructure.Transactions
{
    public interface IClientTransactionRepository
    {
        DateTime? GetLastSynchronization();
        void SetLastSynchronization(DateTime? lastSynchronization);
        void Add(ClientTransaction transaction);
        IList<ClientTransaction> FindPending();
    }
}
