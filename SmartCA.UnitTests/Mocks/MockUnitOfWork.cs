using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartCA.Infrastructure;
using System.Diagnostics;
using SmartCA.Infrastructure.DomainBase;
using SmartCA.Infrastructure.RepositoryFramework;
using SmartCA.Infrastructure.Transactions;

namespace SmartCA.UnitTests
{
    public class MockUnitOfWork : IUnitOfWork
    {
        private Dictionary<IEntity, IUnitOfWorkRepository> addedEntities;
        private object key;
        private IClientTransactionRepository transactionRepository;

        public MockUnitOfWork()
        {
            this.addedEntities = new Dictionary<IEntity, IUnitOfWorkRepository>();
            this.key = Guid.NewGuid();
            this.transactionRepository = ClientTransactionRepositoryFactory.GetTransactionRepository();
        }

        #region IUnitOfWork Members

        public void RegisterAdded(IEntity entity, IUnitOfWorkRepository repository)
        {
            Debug.WriteLine("RegisterAdded called...");
            this.addedEntities.Add(entity, repository);
        }

        public void RegisterChanged(IEntity entity, IUnitOfWorkRepository repository)
        {
            Debug.WriteLine("RegisterChanged called...");
        }

        public void RegisterRemoved(IEntity entity, IUnitOfWorkRepository repository)
        {
            Debug.WriteLine("RegisterRemoved called...");
        }

        public void Commit()
        {
            Debug.WriteLine("Commit called...");
            Debug.WriteLine("Committing the following added entities:  ");
            foreach (IEntity entity in this.addedEntities.Keys)
            {
                Debug.WriteLine(string.Format("Entity Key:  {0}  Associated Repository Type:  {1}", 
                    entity.Key.ToString(), this.addedEntities[entity].ToString()));
                this.addedEntities[entity].PersistNewItem(entity);
            }
        }

        public object Key
        {
            get { return this.key; }
        }

        public IClientTransactionRepository ClientTransactionRepository
        {
            get { return this.transactionRepository; }
        }

        #endregion
    }
}
