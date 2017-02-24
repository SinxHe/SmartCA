using System;
using SmartCA.Infrastructure.DomainBase;

namespace SmartCA.Infrastructure.Transactions
{
    public class ClientTransaction : Transaction
    {
        private IEntity entity;
        
        public ClientTransaction(object key, TransactionType type,
            IEntity entity)
            : base(key, type)
        {
            this.entity = entity;
        }

        public IEntity Entity
        {
            get { return this.entity; }
        }
    }
}
