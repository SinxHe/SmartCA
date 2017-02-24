using System;
using System.Collections.Generic;
using SmartCA.Infrastructure.ReferenceData;
using Microsoft.Practices.EnterpriseLibrary.Data;
using SmartCA.DataContracts;
using System.Text;

namespace SmartCA.Infrastructure.Repositories
{
    public class SqlCeReferenceDataRepository : IReferenceDataRepository
    {
        private Database database;

        public SqlCeReferenceDataRepository()
        {
            this.database = DatabaseFactory.CreateDatabase();
        }

        #region IReferenceDataRepository Members

        public void Add(IList<DisciplineContract> disciplines)
        {
            foreach (DisciplineContract discipline in disciplines)
            {
                this.AddDiscipline(discipline);
            }
        }

        public void Add(IList<ItemStatusContract> itemStatuses)
        {
            foreach (ItemStatusContract itemStatus in itemStatuses)
            {
                this.AddItemStatus(itemStatus);
            }
        }

        public void Add(IList<MarketSectorContract> sectors)
        {
            foreach (MarketSectorContract sector in sectors)
            {
                this.AddMarketSector(sector);
            }
        }

        public void Add(IList<MarketSegmentContract> segments)
        {
            foreach (MarketSegmentContract segment in segments)
            {
                this.AddMarketSegment(segment);
            }
        }

        public void Add(IList<SpecificationSectionContract> specSections)
        {
            foreach (SpecificationSectionContract specSection in specSections)
            {
                this.AddSpecificationSection(specSection);
            }
        }

        #endregion

        #region Private Helper Methods

        private void AddDiscipline(DisciplineContract discipline)
        {
            StringBuilder builder = new StringBuilder(100);
            builder.Append("INSERT INTO Discipline ");
            builder.Append("(DisciplineID,DisciplineName,Description) ");
            builder.Append(string.Format("VALUES ({0},'{1}','{2}');",
                discipline.Key, discipline.Name, discipline.Description));
            this.database.ExecuteNonQuery(
                    this.database.GetSqlStringCommand(builder.ToString()));
        }

        private void AddItemStatus(ItemStatusContract itemStatus)
        {
            string query = string.Format("INSERT INTO ItemStatus (ItemStatusID,Status) VALUES ({0},'{1}');",
                itemStatus.Id, itemStatus.Status);
            this.database.ExecuteNonQuery(
                    this.database.GetSqlStringCommand(query));
        }

        private void AddMarketSector(MarketSectorContract sector)
        {
            string query = string.Format("INSERT INTO MarketSector (MarketSectorID,MarketSectorName) VALUES ({0},'{1}');",
                sector.Key, sector.Name);
            this.database.ExecuteNonQuery(
                    this.database.GetSqlStringCommand(query));

        }

        private void AddMarketSegment(MarketSegmentContract segment)
        {
            string query = string.Format("INSERT INTO MarketSegment (MarketSegmentID,MarketSectorID,Code,MarketSegmentName) VALUES ({0},{1},'{2}','{3}');",
                segment.Key, segment.ParentSector.Key, segment.Code, segment.Name);
            this.database.ExecuteNonQuery(
                    this.database.GetSqlStringCommand(query));

        }

        private void AddSpecificationSection(SpecificationSectionContract specSection)
        {
            string query = string.Format("INSERT INTO SpecificationSection (SpecificationSectionID,SpecificationSectionNumber,SpecificationSectionTitle,SpecificationSectionDescription) VALUES ({0},'{1}','{2}','{3}');",
                specSection.Key, specSection.Number, specSection.Title, specSection.Description);
            this.database.ExecuteNonQuery(
                    this.database.GetSqlStringCommand(query));
        }

        #endregion
    }
}
