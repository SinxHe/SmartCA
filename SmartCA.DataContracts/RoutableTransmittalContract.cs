using System;
using System.Collections.Generic;

namespace SmartCA.DataContracts
{
    [Serializable]
    public abstract class RoutableTransmittalContract : TransmittalContract
    {
        private List<RoutingItemContract> routingItems;

        protected RoutableTransmittalContract()
        {
            this.routingItems = new List<RoutingItemContract>();
        }

        public IList<RoutingItemContract> RoutingItems
        {
            get { return this.routingItems; }
        }
    }
}
