using System;
using SmartCA.Infrastructure.DomainBase;

namespace SmartCA.Model.Projects
{
    public class MarketSegment : EntityBase
    {
        private MarketSector parentSector;
        private string name;
        private string code;

        public MarketSegment(MarketSector parentSector, string name, string code)
            : this(null, parentSector, name, code)
        {
        }

        public MarketSegment(object key, MarketSector parentSector, string name, 
            string code) : base(key)
        {
            this.parentSector = parentSector;
            this.name = name;
            this.code = code;
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

        public MarketSector ParentSector
        {
            get { return this.parentSector; }
        }

        protected override void Validate()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override BrokenRuleMessages GetBrokenRuleMessages()
        {
            return new MarketSegmentRuleMessages();
        }
    }
}
