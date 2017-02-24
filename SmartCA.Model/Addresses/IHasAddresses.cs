using System;
using System.Collections.Generic;

namespace SmartCA.Model.Addresses
{
    public interface IHasAddresses
    {
        IList<Address> Addresses { get; }
    }
}
