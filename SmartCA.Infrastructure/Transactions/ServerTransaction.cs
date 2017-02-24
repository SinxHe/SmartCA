using System;
using SmartCA.Infrastructure.DomainBase;
using SmartCA.DataContracts;

namespace SmartCA.Infrastructure.Transactions
{
    public class ServerTransaction : Transaction
    {
        private ContractBase contract;

        public ServerTransaction(object key, TransactionType type,
            ContractBase contract)
            : base(key, type)
        {
            this.contract = contract;
        }

        public ContractBase Contract
        {
            get { return this.contract; }
        }
    }
}
