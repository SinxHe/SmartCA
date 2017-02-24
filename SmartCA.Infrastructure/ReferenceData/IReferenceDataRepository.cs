using System;
using System.Collections.Generic;
using SmartCA.DataContracts;

namespace SmartCA.Infrastructure.ReferenceData
{
    public interface IReferenceDataRepository
    {
        void Add(IList<DisciplineContract> disciplines);
        void Add(IList<ItemStatusContract> itemStatuses);
        void Add(IList<MarketSectorContract> sectors);
        void Add(IList<MarketSegmentContract> segments);
        void Add(IList<SpecificationSectionContract> specSections);
    }
}
