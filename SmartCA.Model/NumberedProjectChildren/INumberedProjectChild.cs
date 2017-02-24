using System;
using SmartCA.Infrastructure.DomainBase;

namespace SmartCA.Model.NumberedProjectChildren
{
    public interface INumberedProjectChild : IEntity
    {
        object ProjectKey { get; }
        int Number { get; set; }
    }
}
