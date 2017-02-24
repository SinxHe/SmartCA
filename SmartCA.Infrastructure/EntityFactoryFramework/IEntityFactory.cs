using System;
using SmartCA.Infrastructure.DomainBase;
using System.Data;

namespace SmartCA.Infrastructure.EntityFactoryFramework
{
    public interface IEntityFactory<T> where T : IEntity
    {
        T BuildEntity(IDataReader reader);
    }
}
