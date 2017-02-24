using System.Data;
using SmartCA.Infrastructure.EntityFactoryFramework;
using SmartCA.Infrastructure.Helpers;
using SmartCA.Model.RFI;
using SmartCA.Model.Transmittals;

namespace SmartCA.Infrastructure.Repositories
{
    internal class RequestForInformationFactory : IEntityFactory<RequestForInformation>
    {
        #region Field Names

        internal static class FieldNames
        {
            public const string RequestForInformationId = "RequestForInformationID";
            public const string EmployeeId = "EmployeeID";
            public const string TransmittalDate = "TransmittalDate";
            public const string ProjectId = "ProjectID";
            public const string RequestForInformationNumber = "RequestForInformationNumber";
            public const string ProjectContactId = "ProjectContactID";
            public const string SpecificationSectionId = "SpecificationSectionID";
            public const string TotalPages = "TotalPages";
            public const string DeliveryMethod = "DeliveryMethod";
            public const string OtherDeliveryMethod = "OtherDeliveryMethod";
            public const string PhaseNumber = "PhaseNumber";
            public const string Reimbursable = "Reimbursable";
            public const string Final = "Final";
            public const string DateReceived = "DateReceived";
            public const string DateRequestedBy = "DateRequestedBy";
            public const string Question = "Question";
            public const string Description = "Description";
            public const string ContractorProposedSolution = "ContractorProposedSolution";
            public const string NoChange = "NoChange";
            public const string Cause = "Cause";
            public const string Origin = "Origin";
            public const string ItemStatusId = "ItemStatusID";
            public const string DateToField = "DateToField";
            public const string ShortAnswer = "ShortAnswer";
            public const string LongAnswer = "LongAnswer";
            public const string Remarks = "Remarks";
        }

        #endregion

        #region IEntityFactory<RequestForInformation> Members

        public RequestForInformation BuildEntity(IDataReader reader)
        {
            RequestForInformation rfi = new RequestForInformation(reader[FieldNames.RequestForInformationId],
                                      reader[FieldNames.ProjectId],
                                      DataHelper.GetInteger(reader[FieldNames.RequestForInformationNumber]));
            rfi.TransmittalDate = DataHelper.GetDateTime(reader[FieldNames.TransmittalDate]);
            rfi.DateReceived = DataHelper.GetNullableDateTime(reader[FieldNames.DateReceived]);
            rfi.DateToField = DataHelper.GetNullableDateTime(reader[FieldNames.DateToField]);
            rfi.DeliveryMethod = DataHelper.GetEnumValue<Delivery>(reader[FieldNames.DeliveryMethod].ToString());
            rfi.Final = DataHelper.GetBoolean(reader[FieldNames.Final]);
            rfi.OtherDeliveryMethod = reader[FieldNames.OtherDeliveryMethod].ToString();
            rfi.PhaseNumber = reader[FieldNames.PhaseNumber].ToString();
            rfi.Reimbursable = DataHelper.GetBoolean(reader[FieldNames.Reimbursable]);
            rfi.Remarks = reader[FieldNames.Remarks].ToString();
            rfi.SpecSection = SubmittalFactory.BuildSpecSection(reader);
            rfi.Status = TransmittalFactory.BuildItemStatus(reader);
            rfi.TotalPages = DataHelper.GetInteger(reader[FieldNames.TotalPages]);
            return rfi;
        }

        #endregion
    }
}
