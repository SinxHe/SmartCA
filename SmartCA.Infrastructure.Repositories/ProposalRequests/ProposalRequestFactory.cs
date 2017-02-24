using System.Data;
using SmartCA.Infrastructure.EntityFactoryFramework;
using SmartCA.Infrastructure.Helpers;
using SmartCA.Model.ProposalRequests;
using SmartCA.Model.Transmittals;

namespace SmartCA.Infrastructure.Repositories
{
    internal class ProposalRequestFactory : IEntityFactory<ProposalRequest>
    {
        #region Field Names

        internal static class FieldNames
        {
            public const string ProposalRequestId = "ProposalRequestID";
            public const string EmployeeId = "EmployeeID";
            public const string TransmittalDate = "TransmittalDate";
            public const string ProjectId = "ProjectID";
            public const string ProposalRequestNumber = "ProposalRequestNumber";
            public const string ProjectContactId = "ProjectContactID";
            public const string TotalPages = "TotalPages";
            public const string DeliveryMethod = "DeliveryMethod";
            public const string OtherDeliveryMethod = "OtherDeliveryMethod";
            public const string PhaseNumber = "PhaseNumber";
            public const string Reimbursable = "Reimbursable";
            public const string Final = "Final";
            public const string IssueDate = "IssueDate";
            public const string Description = "Description";
            public const string Attachment = "Attachment";
            public const string Reason = "Reason";
            public const string Initiator = "Initiator";
            public const string Cause = "Cause";
            public const string Origin = "Origin";
            public const string Remarks = "Remarks";
            public const string TransmittalRemarks = "TransmittalRemarks";
        }

        #endregion

        #region IEntityFactory<ProposalRequest> Members

        public ProposalRequest BuildEntity(IDataReader reader)
        {
            ProposalRequest proposalRequest = new ProposalRequest(reader[FieldNames.ProposalRequestId],
                                      reader[FieldNames.ProjectId],
                                      DataHelper.GetInteger(reader[FieldNames.ProposalRequestNumber]));
            proposalRequest.Attachment = DataHelper.GetString(reader[FieldNames.Attachment]);
            proposalRequest.Cause = DataHelper.GetInteger(reader[FieldNames.Cause]);
            proposalRequest.Description = DataHelper.GetString(reader[FieldNames.Description]);
            proposalRequest.Initiator = DataHelper.GetString(reader[FieldNames.Initiator]);
            proposalRequest.IssueDate = DataHelper.GetNullableDateTime(reader[FieldNames.IssueDate]);
            proposalRequest.Origin = DataHelper.GetInteger(reader[FieldNames.Origin]);
            proposalRequest.OtherDeliveryMethod = DataHelper.GetString(reader[FieldNames.OtherDeliveryMethod]);
            proposalRequest.Reason = DataHelper.GetString(reader[FieldNames.Reason]);
            proposalRequest.TransmittalRemarks = DataHelper.GetString(reader[FieldNames.TransmittalRemarks]);
            proposalRequest.TransmittalDate = DataHelper.GetDateTime(reader[FieldNames.TransmittalDate]);
            proposalRequest.DeliveryMethod = DataHelper.GetEnumValue<Delivery>(reader[FieldNames.DeliveryMethod].ToString());
            proposalRequest.Final = DataHelper.GetBoolean(reader[FieldNames.Final]);
            proposalRequest.OtherDeliveryMethod = DataHelper.GetString(reader[FieldNames.OtherDeliveryMethod]);
            proposalRequest.PhaseNumber = reader[FieldNames.PhaseNumber].ToString();
            proposalRequest.Reimbursable = DataHelper.GetBoolean(reader[FieldNames.Reimbursable]);
            proposalRequest.Remarks = reader[FieldNames.Remarks].ToString();
            proposalRequest.TotalPages = DataHelper.GetInteger(reader[FieldNames.TotalPages]);
            return proposalRequest;
        }

        #endregion
    }
}
