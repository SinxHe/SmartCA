using System;
using System.Collections.Generic;

namespace SmartCA.Model.Transmittals
{
    public interface IHasRoutingItems
    {
        IList<RoutingItem> RoutingItems { get; }
    }
}
