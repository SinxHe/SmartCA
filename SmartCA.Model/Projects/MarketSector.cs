using System;
using System.Collections.Generic;
using SmartCA.Infrastructure.DomainBase;

namespace SmartCA.Model.Projects
{
    public class MarketSector : EntityBase
    {
        private string name;
        private List<MarketSegment> segments;

        public MarketSector(string name)
            : this(null, name)
        {
            this.name = name;
        }

        public MarketSector(object key, string name) 
            : base(key)
        {
            this.name = name;
            this.segments = new List<MarketSegment>();
        }

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public IList<MarketSegment> Segments
        {
            get { return this.segments; }
        }

        protected override void Validate()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override BrokenRuleMessages GetBrokenRuleMessages()
        {
            return new MarketSectorRuleMessages();
        }
    }
}
