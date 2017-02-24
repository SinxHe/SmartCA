using System;
using System.Collections.Generic;
using SmartCA.DataContracts;

namespace SmartCA.Infrastructure.Synchronization
{
    public class ReferenceDataContract
    {
        private IList<DisciplineContract> disciplines;
        private IList<ItemStatusContract> itemStatuses;
        private IList<MarketSectorContract> sectors;
        private IList<MarketSegmentContract> segments;
        private IList<SpecificationSectionContract> specSections;

        public IList<DisciplineContract> Disciplines
        {
            get { return this.disciplines; }
            set { this.disciplines = value; }
        }

        public IList<ItemStatusContract> ItemStatuses
        {
            get { return this.itemStatuses; }
            set { this.itemStatuses = value; }
        }

        public IList<MarketSectorContract> Sectors
        {
            get { return this.sectors; }
            set { this.sectors = value; }
        }

        public IList<MarketSegmentContract> Segments
        {
            get { return this.segments; }
            set { this.segments = value; }
        }

        public IList<SpecificationSectionContract> SpecSections
        {
            get { return this.specSections; }
            set { this.specSections = value; }
        }
    }
}
