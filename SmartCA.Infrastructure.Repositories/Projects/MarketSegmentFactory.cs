using System;
using SmartCA.Model.Projects;
using SmartCA.Infrastructure.EntityFactoryFramework;
using System.Data;

namespace SmartCA.Infrastructure.Repositories
{
    internal class MarketSegmentFactory : IEntityFactory<MarketSegment>
    {
        #region Field Names

        internal static class FieldNames
        {
            public const string MarketSegmentId = "MarketSegmentID";
            public const string MarketSectorId = "MarketSectorID";
            public const string Code = "Code";
            public const string MarketSegmentName = "MarketSegmentName";
            public const string MarketSectorName = "MarketSectorName";
        }

        #endregion

        #region IEntityFactory<MarketSegment> Members

        public MarketSegment BuildEntity(IDataReader reader)
        {
            return new MarketSegment(reader[FieldNames.MarketSegmentId], 
                                        new MarketSector(reader[FieldNames.MarketSectorId],
                                            reader[FieldNames.MarketSectorName].ToString()),
                                        reader[FieldNames.MarketSegmentName].ToString(),
                                        reader[FieldNames.Code].ToString());
        }

        #endregion
    }
}
