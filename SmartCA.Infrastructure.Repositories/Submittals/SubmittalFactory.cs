using System.Data;
using SmartCA.Infrastructure.EntityFactoryFramework;
using SmartCA.Infrastructure.Helpers;
using SmartCA.Model.Submittals;
using SmartCA.Model.Transmittals;

namespace SmartCA.Infrastructure.Repositories
{
    public class SubmittalFactory : IEntityFactory<Submittal>
    {
        #region Field Names

        internal static class FieldNames
        {
            public const string SubmittalId = "SubmittalID";
            public const string EmployeeId = "EmployeeID";
            public const string TransmittalDate = "TransmittalDate";
            public const string ProjectId = "ProjectID";
            public const string ProjectContactId = "ProjectContactID";
            public const string SpecificationSectionId = "SpecificationSectionID";
            public const string SpecificationSectionNumber = "SpecificationSectionNumber";
            public const string SpecificationSectionTitle = "SpecificationSectionTitle";
            public const string SpecificationSectionDescription = "SpecificationSectionDescription";
            public const string SpecificationSectionPrimaryIndex = "SpecificationSectionPrimaryIndex";
            public const string SpecificationSectionSecondaryIndex = "SpecificationSectionSecondaryIndex";
            public const string TotalPages = "TotalPages";
            public const string DeliveryMethod = "DeliveryMethod";
            public const string OtherDeliveryMethod = "OtherDeliveryMethod";
            public const string PhaseNumber = "PhaseNumber";
            public const string Reimbursable = "Reimbursable";
            public const string DateReceived = "DateReceived";
            public const string ContractNumber = "ContractNumber";
            public const string Remarks = "Remarks";
            public const string Action = "Action";
            public const string ItemStatusId = "ItemStatusID";
            public const string ItemStatus = "Status";
            public const string DateToField = "DateToField";
            public const string RemainderLocation = "RemainderLocation";
            public const string RemainderUnderSubmittalNumber = "RemainderUnderSubmittalNumber";
            public const string OtherRemainderLocation = "OtherRemainderLocation";
            public const string Final = "Final";
            public const string Notes = "Notes";
            public const string SubmittalTrackingItemId = "SubmittalTrackingItemID";
            public const string TotalItemsReceived = "TotalItemsReceived";
            public const string TotalItemsSent = "TotalItemsSent";
            public const string DeferredApproval = "DeferredApproval";
            public const string SubstitutionNumber = "SubstitutionNumber";
            public const string Description = "Description";
            public const string Status = "Status";
            public const string DisciplineId = "DisciplineID";
            public const string DisciplineName = "DisciplineName";
            public const string RoutingItemId = "RoutingItemID";
            public const string RoutingOrder = "RoutingOrder";
            public const string DateSent = "DateSent";
            public const string DateReturned = "DateReturned";
        }

        #endregion

        #region IEntityFactory<Submittal> Members

        public Submittal BuildEntity(IDataReader reader)
        {
            Submittal submittal = new Submittal(reader[FieldNames.SubmittalId], 
                                      SubmittalFactory.BuildSpecSection(reader),
                                      reader[FieldNames.ProjectId]);
            submittal.TransmittalDate = DataHelper.GetDateTime(reader[FieldNames.TransmittalDate]);
            submittal.Action = DataHelper.GetEnumValue<ActionStatus>(reader[FieldNames.Action].ToString());
            submittal.ContractNumber = reader[FieldNames.ContractNumber].ToString();
            submittal.DateReceived = DataHelper.GetNullableDateTime(reader[FieldNames.DateReceived]);
            submittal.DateToField = DataHelper.GetNullableDateTime(reader[FieldNames.DateToField]);
            submittal.DeliveryMethod = DataHelper.GetEnumValue<Delivery>(reader[FieldNames.DeliveryMethod].ToString());
            submittal.Final = DataHelper.GetBoolean(reader[FieldNames.Final]);
            submittal.OtherDeliveryMethod = reader[FieldNames.OtherDeliveryMethod].ToString();
            submittal.OtherRemainderLocation = reader[FieldNames.OtherRemainderLocation].ToString();
            submittal.PhaseNumber = reader[FieldNames.PhaseNumber].ToString();
            submittal.Reimbursable = DataHelper.GetBoolean(reader[FieldNames.Reimbursable]);
            submittal.RemainderLocation = DataHelper.GetEnumValue<SubmittalRemainderLocation>(reader[FieldNames.RemainderLocation].ToString());
            submittal.RemainderUnderSubmittalNumber = reader[FieldNames.RemainderUnderSubmittalNumber].ToString();
            submittal.Remarks = reader[FieldNames.Remarks].ToString();
            submittal.SpecSectionPrimaryIndex = reader[FieldNames.SpecificationSectionPrimaryIndex].ToString();
            submittal.SpecSectionSecondaryIndex = reader[FieldNames.SpecificationSectionSecondaryIndex].ToString();
            submittal.Status = TransmittalFactory.BuildItemStatus(reader);
            submittal.TotalPages = DataHelper.GetInteger(reader[FieldNames.TotalPages]);
            return submittal;
        }

        #endregion

        internal static SpecificationSection BuildSpecSection(IDataReader reader)
        {
            return new SpecificationSection(DataHelper.GetInteger(reader[FieldNames.SpecificationSectionId]), 
                       reader[FieldNames.SpecificationSectionNumber].ToString(),
                       reader[FieldNames.SpecificationSectionTitle].ToString(),
                       reader[FieldNames.SpecificationSectionDescription].ToString());
        }

        internal static TrackingItem BuildTrackingItem(IDataReader reader)
        {
            TrackingItem item = new TrackingItem(SubmittalFactory.BuildSpecSection(reader));
            item.TotalItemsReceived = DataHelper.GetInteger(
                reader[FieldNames.TotalItemsReceived]);
            item.TotalItemsSent = DataHelper.GetInteger(
                reader[FieldNames.TotalItemsSent]);
            item.DeferredApproval = DataHelper.GetInteger(
                reader[FieldNames.DeferredApproval]);
            item.SubstitutionNumber = DataHelper.GetInteger(
                reader[FieldNames.SubstitutionNumber]);
            item.Description = reader[FieldNames.Description].ToString();
            item.Status = DataHelper.GetEnumValue<ActionStatus>(
                reader[FieldNames.Status].ToString());
            return item;
        }
    }
}
