using System;
using SmartCA.Infrastructure.DomainBase;
using System.Collections.Generic;

namespace SmartCA.Infrastructure.RepositoryFramework
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        void SetUnitOfWork(IUnitOfWork unitOfWork);
        T FindBy(object key);
        IList<T> FindAll();
        void Add(T item);
        T this[object key] { get; set; }
        void Remove(T item);
    }
}
