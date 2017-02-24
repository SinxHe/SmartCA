using System.Data;
using SmartCA.Infrastructure.EntityFactoryFramework;
using SmartCA.Infrastructure.Helpers;
using SmartCA.Model.ChangeOrders;

namespace SmartCA.Infrastructure.Repositories
{
    internal class ChangeOrderFactory : IEntityFactory<ChangeOrder>
    {
        #region Field Names

        internal static class FieldNames
        {
            public const string ChangeOrderId = "ChangeOrderID";
            public const string ProjectId = "ProjectID";
            public const string ChangeOrderNumber = "ChangeOrderNumber";
            public const string EffectiveDate = "EffectiveDate";
            public const string Description = "Description";
            public const string PriceChangeType = "PriceChangeType";
            public const string PriceChangeTypeDirection = "PriceChangeTypeDirection";
            public const string AmountChanged = "AmountChanged";
            public const string TimeChangeDirection = "TimeChangeDirection";
            public const string TimeChangedDays = "TimeChangedDays";
            public const string ItemStatusId = "ItemStatusID";
            public const string AgencyApprovedDate = "AgencyApprovedDate";
            public const string DateToField = "DateToField";
            public const string OwnerSignatureDate = "OwnerSignatureDate";
            public const string ArchitectSignatureDate = "ArchitectSignatureDate";
            public const string ContractorSignatureDate = "ContractorSignatureDate";
        }

        #endregion

        #region IEntityFactory<ChangeOrder> Members

        public ChangeOrder BuildEntity(IDataReader reader)
        {
            ChangeOrder co = new ChangeOrder(reader[FieldNames.ChangeOrderId],
                                      reader[FieldNames.ProjectId],
                                      DataHelper.GetInteger(reader[FieldNames.ChangeOrderNumber]));
            co.AgencyApprovedDate = DataHelper.GetNullableDateTime(reader[FieldNames.AgencyApprovedDate]);
            co.AmountChanged = DataHelper.GetDecimal(reader[FieldNames.AmountChanged]);
            co.ArchitectSignatureDate = DataHelper.GetNullableDateTime(reader[FieldNames.ArchitectSignatureDate]);
            co.ChangeType = DataHelper.GetEnumValue<PriceChangeType>(reader[FieldNames.PriceChangeType].ToString());
            co.PriceChangeDirection = DataHelper.GetEnumValue<ChangeDirection>(reader[FieldNames.PriceChangeTypeDirection].ToString());
            co.ContractorSignatureDate = DataHelper.GetNullableDateTime(reader[FieldNames.ContractorSignatureDate]);
            co.DateToField = DataHelper.GetNullableDateTime(reader[FieldNames.DateToField]);
            co.Description = DataHelper.GetString(reader[FieldNames.Description]);
            co.EffectiveDate = DataHelper.GetDateTime(reader[FieldNames.EffectiveDate]);
            co.OwnerSignatureDate = DataHelper.GetNullableDateTime(reader[FieldNames.OwnerSignatureDate]);
            co.TimeChanged = DataHelper.GetInteger(reader[FieldNames.TimeChangedDays]);
            co.Status = TransmittalFactory.BuildItemStatus(reader);
            co.TimeChangeDirection = DataHelper.GetEnumValue<ChangeDirection>(reader[FieldNames.TimeChangeDirection].ToString());
            return co;
        }

        #endregion
    }
}
