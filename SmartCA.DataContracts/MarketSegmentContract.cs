using System;

namespace SmartCA.DataContracts
{
    [Serializable]
    public class MarketSegmentContract : ContractBase
    {
        private MarketSectorContract parentSector;
        private string name;
        private string code;

        public MarketSegmentContract()
        {
            this.parentSector = null;
            this.name = string.Empty;
            this.code = string.Empty;
        }

        public MarketSectorContract ParentSector
        {
            get { return this.parentSector; }
            set { this.parentSector = value; }
        }

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public string Code
        {
            get { return this.code; }
            set { this.code = value; }
        }
    }
}
