using System;
using System.Collections.Generic;
using SmartCA.Infrastructure.DomainBase;
using SmartCA.Infrastructure.RepositoryFramework;
using System.Transactions;
using SmartCA.Infrastructure.Transactions;
using System.Linq;

namespace SmartCA.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Fields

        private Guid key;
        private IClientTransactionRepository clientTransactionRepository;
        private List<Operation> operations;

        #endregion

        #region Constructors

        public UnitOfWork()
        {
            this.key = Guid.NewGuid();
            this.clientTransactionRepository = ClientTransactionRepositoryFactory.GetTransactionRepository();
            this.operations = new List<Operation>();
        }

        #endregion

        #region Operation

        /// <summary>
        /// Provides a snapshot of an entity and the repository reference it belongs to.
        /// </summary>
        private sealed class Operation
        {
            /// <summary>
            /// Gets or sets the entity.
            /// </summary>
            /// <value>The entity.</value>
            public IEntity Entity { get; set; }

            /// <summary>
            /// Gets or sets the process date.
            /// </summary>
            /// <value>The process date.</value>
            public DateTime ProcessDate { get; set; }

            /// <summary>
            /// Gets or sets the repository.
            /// </summary>
            /// <value>The repository.</value>
            public IUnitOfWorkRepository Repository { get; set; }

            /// <summary>
            /// Gets or sets the type of operation.
            /// </summary>
            /// <value>The type of operation.</value>
            public TransactionType Type { get; set; }
        }

        #endregion

        #region IUnitOfWork Members

        /// <summary>
        /// Registers an <see cref="IEntity" /> instance to be added through this <see cref="UnitOfWork" />.
        /// </summary>
        /// <param name="entity">The <see cref="IEntity" />.</param>
        /// <param name="repository">The <see cref="IUnitOfWorkRepository" /> participating in the transaction.</param>
        public void RegisterAdded(IEntity entity, 
            IUnitOfWorkRepository repository)
        {
            this.operations.Add(
                new Operation
                {
                    Entity = entity,
                    ProcessDate = DateTime.Now,
                    Repository = repository,
                    Type = TransactionType.Insert
                });

        }

        /// <summary>
        /// Registers an <see cref="IEntity" /> instance to be changed through this <see cref="UnitOfWork" />.
        /// </summary>
        /// <param name="entity">The <see cref="IEntity" />.</param>
        /// <param name="repository">The <see cref="IUnitOfWorkRepository" /> participating in the transaction.</param>
        public void RegisterChanged(IEntity entity, 
            IUnitOfWorkRepository repository)
        {
            this.operations.Add(
                new Operation
                {
                    Entity = entity,
                    ProcessDate = DateTime.Now,
                    Repository = repository,
                    Type = TransactionType.Update
                });
        }

        /// <summary>
        /// Registers an <see cref="IEntity" /> instance to be removed through this <see cref="UnitOfWork" />.
        /// </summary>
        /// <param name="entity">The <see cref="IEntity" />.</param>
        /// <param name="repository">The <see cref="IUnitOfWorkRepository" /> participating in the transaction.</param>
        public void RegisterRemoved(IEntity entity, 
            IUnitOfWorkRepository repository)
        {
            this.operations.Add(
                new Operation
                {
                    Entity = entity,
                    ProcessDate = DateTime.Now,
                    Repository = repository,
                    Type = TransactionType.Delete
                });
        }

        /// <summary>
        /// Commits all batched changes within the scope of a <see cref="TransactionScope" />.
        /// </summary>
        public void Commit()
        {
            using (var scope = new TransactionScope())
            {
                foreach (var operation in this.operations.OrderBy(o => o.ProcessDate))
                {
                    switch (operation.Type)
                    {
                        case TransactionType.Insert:
                            operation.Repository.PersistNewItem(operation.Entity);
                            this.clientTransactionRepository.Add(
                                new ClientTransaction(this.key,
                                    TransactionType.Insert, operation.Entity));
                            break;
                        case TransactionType.Delete:
                            operation.Repository.PersistDeletedItem(operation.Entity);
                            this.clientTransactionRepository.Add(
                                new ClientTransaction(this.key,
                                    TransactionType.Delete, operation.Entity));
                            break;
                        case TransactionType.Update:
                            operation.Repository.PersistUpdatedItem(operation.Entity);
                            this.clientTransactionRepository.Add(
                                new ClientTransaction(this.key,
                                    TransactionType.Update, operation.Entity));
                            break;
                    }
                }

                // Commit the transaction
                scope.Complete();
            }

            // Clear everything
            this.operations.Clear();
            this.key = Guid.NewGuid();
        }

        public object Key
        {
            get { return this.key; }
        }

        public IClientTransactionRepository ClientTransactionRepository
        {
            get { return this.clientTransactionRepository; }
        }

        #endregion
    }
}
