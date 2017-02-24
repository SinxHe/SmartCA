using System;
using System.Collections.Generic;

namespace SmartCA.DataContracts
{
    [Serializable]
    public class MarketSectorContract : ContractBase
    {
        private string name;
        private List<MarketSegmentContract> segments;

        public MarketSectorContract()
        {
            this.name = string.Empty;
            this.segments = new List<MarketSegmentContract>();
        }

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public IList<MarketSegmentContract> Segments
        {
            get { return this.segments; }
        }
    }
}
