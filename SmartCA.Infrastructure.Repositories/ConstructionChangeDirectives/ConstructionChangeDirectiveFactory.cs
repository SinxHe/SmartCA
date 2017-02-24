using System.Data;
using SmartCA.Infrastructure.EntityFactoryFramework;
using SmartCA.Infrastructure.Helpers;
using SmartCA.Model.ChangeOrders;
using SmartCA.Model.ConstructionChangeDirectives;
using SmartCA.Model.Transmittals;

namespace SmartCA.Infrastructure.Repositories
{
    internal class ConstructionChangeDirectiveFactory : IEntityFactory<ConstructionChangeDirective>
    {
        #region Field Names

        internal static class FieldNames
        {
            public const string ConstructionChangeDirectiveId = "ConstructionChangeDirectiveID";
            public const string ConstructionChangeDirectiveNumber = "ConstructionChangeDirectiveNumber";
            public const string IssueDate = "IssueDate";
            public const string Description = "Description";
            public const string Attachment = "Attachment";
            public const string Reason = "Reason";
            public const string Initiator = "Initiator";
            public const string Cause = "Cause";
            public const string Origin = "Origin";
            public const string Remarks = "Remarks";
            public const string TransmittalRemarks = "TransmittalRemarks";
            public const string PriceChangeType = "PriceChangeType";
            public const string PriceChangeTypeDirection = "PriceChangeTypeDirection";
            public const string AmountChanged = "AmountChanged";
            public const string TimeChangeDirection = "TimeChangeDirection";
            public const string TimeChangedDays = "TimeChangedDays";
            public const string ItemStatusId = "ItemStatusID";
            public const string OwnerSignatureDate = "OwnerSignatureDate";
            public const string ArchitectSignatureDate = "ArchitectSignatureDate";
            public const string ContractorSignatureDate = "ContractorSignatureDate";
        }

        #endregion

        #region IEntityFactory<ConstructionChangeDirective> Members

        public ConstructionChangeDirective BuildEntity(IDataReader reader)
        {
            ConstructionChangeDirective ccd = new ConstructionChangeDirective(reader[FieldNames.ConstructionChangeDirectiveId],
                                      reader[ProjectFactory.FieldNames.ProjectId],
                                      DataHelper.GetInteger(reader[FieldNames.ConstructionChangeDirectiveNumber]));
            ccd.AmountChanged = DataHelper.GetDecimal(reader[FieldNames.AmountChanged]);
            ccd.ArchitectSignatureDate = DataHelper.GetNullableDateTime(reader[FieldNames.ArchitectSignatureDate]);
            ccd.Attachment = DataHelper.GetString(reader[FieldNames.Attachment]);
            ccd.Cause = DataHelper.GetInteger(reader[FieldNames.Cause]);
            ccd.ChangeType = DataHelper.GetEnumValue<PriceChangeType>(reader[FieldNames.PriceChangeType].ToString());
            ccd.ContractorSignatureDate = DataHelper.GetNullableDateTime(reader[FieldNames.ContractorSignatureDate]);
            ccd.DeliveryMethod = DataHelper.GetEnumValue<Delivery>(reader[TransmittalFactory.FieldNames.DeliveryMethod]);
            ccd.Description = DataHelper.GetString(reader[FieldNames.Description]);
            ccd.Final = DataHelper.GetBoolean(reader[TransmittalFactory.FieldNames.Final]);
            ccd.Initiator = DataHelper.GetString(reader[FieldNames.Initiator]);
            ccd.IssueDate = DataHelper.GetNullableDateTime(reader[FieldNames.IssueDate]);
            ccd.Origin = DataHelper.GetInteger(reader[FieldNames.Origin]);
            ccd.OtherDeliveryMethod = DataHelper.GetString(reader[TransmittalFactory.FieldNames.OtherDeliveryMethod]);
            ccd.OwnerSignatureDate = DataHelper.GetNullableDateTime(reader[FieldNames.OwnerSignatureDate]);
            ccd.PhaseNumber = DataHelper.GetString(reader[TransmittalFactory.FieldNames.PhaseNumber]);
            ccd.PriceChangeDirection = DataHelper.GetEnumValue<ChangeDirection>(reader[FieldNames.PriceChangeTypeDirection].ToString());
            ccd.Reason = DataHelper.GetString(reader[FieldNames.Reason]);
            ccd.Reimbursable = DataHelper.GetBoolean(reader[TransmittalFactory.FieldNames.Reimbursable]);
            ccd.Remarks = DataHelper.GetString(reader[FieldNames.Remarks]);
            ccd.TimeChanged = DataHelper.GetInteger(reader[FieldNames.TimeChangedDays]);
            ccd.TimeChangeDirection = DataHelper.GetEnumValue<ChangeDirection>(reader[FieldNames.TimeChangeDirection].ToString());
            ccd.TotalPages = DataHelper.GetInteger(reader[TransmittalFactory.FieldNames.TotalPages]);
            ccd.TransmittalDate = DataHelper.GetDateTime(reader[TransmittalFactory.FieldNames.TransmittalDate]);
            ccd.TransmittalRemarks = DataHelper.GetString(reader[TransmittalFactory.FieldNames.TransmittalRemarks]);
            return ccd;
        }

        #endregion
    }
}
