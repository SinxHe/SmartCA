using System;

namespace SmartCA.Infrastructure.DomainBase
{
    /// <summary>
    /// This is a marker interface that indicates that an 
    /// Entity is an Aggregate Root.
    /// </summary>
    public interface IAggregateRoot : IEntity
    {
    }
}
