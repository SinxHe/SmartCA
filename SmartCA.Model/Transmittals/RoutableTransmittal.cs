using System;
using System.Collections.Generic;

namespace SmartCA.Model.Transmittals
{
    public abstract class RoutableTransmittal : Transmittal, IRoutableTransmittal
    {
        #region Private Fields

        private List<RoutingItem> routingItems;

        #endregion

        #region Constructors

        protected RoutableTransmittal(object projectKey) 
            : this(null, projectKey)
        {
        }

        protected RoutableTransmittal(object key, object projectKey) 
            : base(key, projectKey)
        {
            this.routingItems = new List<RoutingItem>();
        }

        #endregion

        #region IRoutableTransmittal Members

        public IList<RoutingItem> RoutingItems
        {
            get { return this.routingItems; }
        }

        #endregion
    }
}
