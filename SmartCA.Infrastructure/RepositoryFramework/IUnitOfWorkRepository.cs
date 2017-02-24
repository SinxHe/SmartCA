using System;
using SmartCA.Infrastructure.DomainBase;

namespace SmartCA.Infrastructure.RepositoryFramework
{
    public interface IUnitOfWorkRepository
    {
        void PersistNewItem(IEntity item);
        void PersistUpdatedItem(IEntity item);
        void PersistDeletedItem(IEntity item);
    }
}