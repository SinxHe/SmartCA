using System.Collections.Generic;
using System.Configuration;
using System.Text;
using SmartCA.Infrastructure.Helpers;
using SmartCA.Infrastructure.Repositories.NumberedProjectChildren;
using SmartCA.Infrastructure.Repositories.Transmittals;
using SmartCA.Model.Employees;
using SmartCA.Model.Projects;
using SmartCA.Model.ProposalRequests;
using SmartCA.Model.Companies;

namespace SmartCA.Infrastructure.Repositories
{
    public class ProposalRequestRepository : SqlCeTransmittalRepository<ProposalRequest>,
        IProposalRequestRepository
    {
        #region Private Fields

        private const string ExpectedContractorReturnDaysConfigName = "ExpectedProposalRequestContractorReturnDays";

        #endregion

        #region Public Constructors

        public ProposalRequestRepository()
            : this(null)
        {
        }

        public ProposalRequestRepository(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }

        #endregion

        #region GetBaseQuery

        protected override string GetBaseQuery()
        {
            return "SELECT * FROM ProposalRequest";
        }

        #endregion

        #region GetBaseWhereClause

        protected override string GetBaseWhereClause()
        {
            return " WHERE ProposalRequestID = '{0}';";
        }

        #endregion

        #region GetEntityName

        protected override string GetEntityName()
        {
            return "ProposalRequest";
        }

        #endregion

        #region GetKeyFieldName

        protected override string GetKeyFieldName()
        {
            return ProposalRequestFactory.FieldNames.ProposalRequestId;
        }

        #endregion

        #region BuildChildCallbacks

        protected override void BuildChildCallbacks()
        {
            this.ChildCallbacks.Add(
                ProposalRequestFactory.FieldNames.ProjectContactId,
                this.AppendTo);
            this.ChildCallbacks.Add(
                ProposalRequestFactory.FieldNames.EmployeeId,
                this.AppendFrom);
            this.ChildCallbacks.Add(CompanyFactory.FieldNames.CompanyId,
                this.AppendContractor);
            base.BuildChildCallbacks();
        }

        #endregion

        #region Unit of Work Implementation

        protected override void PersistNewItem(ProposalRequest item)
        {
            StringBuilder builder = new StringBuilder(100);
            builder.Append(string.Format("INSERT INTO ProposalRequest ({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21}) ",
                ProposalRequestFactory.FieldNames.ProposalRequestId,
                ProjectFactory.FieldNames.ProjectId,
                ProposalRequestFactory.FieldNames.ProposalRequestNumber,
                ProposalRequestFactory.FieldNames.TransmittalDate,
                ProposalRequestFactory.FieldNames.ProjectContactId,
                ProposalRequestFactory.FieldNames.EmployeeId,
                ProposalRequestFactory.FieldNames.TotalPages,
                ProposalRequestFactory.FieldNames.DeliveryMethod,
                ProposalRequestFactory.FieldNames.OtherDeliveryMethod,
                ProposalRequestFactory.FieldNames.PhaseNumber,
                ProposalRequestFactory.FieldNames.Reimbursable,
                ProposalRequestFactory.FieldNames.Final,
                ProposalRequestFactory.FieldNames.IssueDate,
                CompanyFactory.FieldNames.CompanyId,
                ProposalRequestFactory.FieldNames.Description,
                ProposalRequestFactory.FieldNames.Attachment,
                ProposalRequestFactory.FieldNames.Reason,
                ProposalRequestFactory.FieldNames.Initiator,
                ProposalRequestFactory.FieldNames.Cause,
                ProposalRequestFactory.FieldNames.Origin,
                ProposalRequestFactory.FieldNames.Remarks,
                ProposalRequestFactory.FieldNames.TransmittalRemarks
                ));
            builder.Append(string.Format("VALUES ({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21});",
                DataHelper.GetSqlValue(item.Key),
                DataHelper.GetSqlValue(item.ProjectKey),
                DataHelper.GetSqlValue(item.Number),
                DataHelper.GetSqlValue(item.TransmittalDate),
                DataHelper.GetSqlValue(item.To.Key),
                DataHelper.GetSqlValue(item.From.Key),
                DataHelper.GetSqlValue(item.TotalPages),
                DataHelper.GetSqlValue(item.DeliveryMethod),
                DataHelper.GetSqlValue(item.OtherDeliveryMethod),
                DataHelper.GetSqlValue(item.PhaseNumber),
                DataHelper.GetSqlValue(item.Reimbursable),
                DataHelper.GetSqlValue(item.Final),
                DataHelper.GetSqlValue(item.IssueDate),
                DataHelper.GetSqlValue(item.Contractor.Key),
                DataHelper.GetSqlValue(item.Description),
                DataHelper.GetSqlValue(item.Attachment),
                DataHelper.GetSqlValue(item.Reason),
                DataHelper.GetSqlValue(item.Initiator),
                DataHelper.GetSqlValue(item.Cause),
                DataHelper.GetSqlValue(item.Origin),
                DataHelper.GetSqlValue(item.Remarks),
                DataHelper.GetSqlValue(item.TransmittalRemarks)));

            this.Database.ExecuteNonQuery(
                this.Database.GetSqlStringCommand(builder.ToString()));

            // Now do the child objects
            this.InsertCopyToList(item);
        }

        protected override void PersistUpdatedItem(ProposalRequest item)
        {
            StringBuilder builder = new StringBuilder(100);
            builder.Append("UPDATE ProposalRequest SET ");

            builder.Append(string.Format("{0} = {1}",
                ProposalRequestFactory.FieldNames.ProposalRequestNumber,
                DataHelper.GetSqlValue(item.Number)));

            builder.Append(string.Format(",{0} = {1}",
                ProposalRequestFactory.FieldNames.TransmittalDate,
                DataHelper.GetSqlValue(item.TransmittalDate)));

            builder.Append(string.Format(",{0} = {1}",
                ProjectFactory.FieldNames.ProjectContactId,
                DataHelper.GetSqlValue(item.To.Key)));

            builder.Append(string.Format(",{0} = {1}",
                ProposalRequestFactory.FieldNames.EmployeeId,
                DataHelper.GetSqlValue(item.From.Key)));

            builder.Append(string.Format(",{0} = {1}",
                ProposalRequestFactory.FieldNames.TotalPages,
                DataHelper.GetSqlValue(item.TotalPages)));

            builder.Append(string.Format(",{0} = {1}",
                ProposalRequestFactory.FieldNames.DeliveryMethod,
                DataHelper.GetSqlValue(item.DeliveryMethod)));

            builder.Append(string.Format(",{0} = {1}",
                ProposalRequestFactory.FieldNames.OtherDeliveryMethod,
                DataHelper.GetSqlValue(item.OtherDeliveryMethod)));

            builder.Append(string.Format(",{0} = {1}",
                ProposalRequestFactory.FieldNames.PhaseNumber,
                DataHelper.GetSqlValue(item.PhaseNumber)));

            builder.Append(string.Format(",{0} = {1}",
                ProposalRequestFactory.FieldNames.Reimbursable,
                DataHelper.GetSqlValue(item.Reimbursable)));

            builder.Append(string.Format(",{0} = {1}",
                ProposalRequestFactory.FieldNames.Final,
                DataHelper.GetSqlValue(item.Final)));

            builder.Append(string.Format(",{0} = {1}",
                ProposalRequestFactory.FieldNames.IssueDate,
                DataHelper.GetSqlValue(item.IssueDate)));

            builder.Append(string.Format(",{0} = {1}",
                CompanyFactory.FieldNames.CompanyId,
                DataHelper.GetSqlValue(item.Contractor.Key)));

            builder.Append(string.Format(",{0} = {1}",
                ProposalRequestFactory.FieldNames.Description,
                DataHelper.GetSqlValue(item.Description)));

            builder.Append(string.Format(",{0} = {1}",
                ProposalRequestFactory.FieldNames.Attachment,
                DataHelper.GetSqlValue(item.Attachment)));

            builder.Append(string.Format(",{0} = {1}",
                ProposalRequestFactory.FieldNames.Reason,
                DataHelper.GetSqlValue(item.Reason)));

            builder.Append(string.Format(",{0} = {1}",
                ProposalRequestFactory.FieldNames.Initiator,
                DataHelper.GetSqlValue(item.Initiator)));

            builder.Append(string.Format(",{0} = {1}",
                ProposalRequestFactory.FieldNames.Cause,
                DataHelper.GetSqlValue(item.Cause)));

            builder.Append(string.Format(",{0} = {1}",
                ProposalRequestFactory.FieldNames.Origin,
                DataHelper.GetSqlValue(item.Origin)));

            builder.Append(string.Format(",{0} = {1}",
                ProposalRequestFactory.FieldNames.Remarks,
                DataHelper.GetSqlValue(item.Remarks)));

            builder.Append(string.Format(",{0} = {1}",
                ProposalRequestFactory.FieldNames.TransmittalRemarks,
                DataHelper.GetSqlValue(item.TransmittalRemarks)));

            builder.Append(" ");
            builder.Append(this.BuildBaseWhereClause(item.Key));

            this.Database.ExecuteNonQuery(
                this.Database.GetSqlStringCommand(builder.ToString()));

            // Now do the child objects

            // First, delete the existing ones
            this.DeleteCopyToList(item);
            
            // Now, add the current ones
            this.InsertCopyToList(item);
        }

        #endregion

        #region Private Callback and Helper Methods

        private void AppendTo(ProposalRequest proposalRequest,
            object toProjectContactKey)
        {
            proposalRequest.To = ProjectService.GetProjectContact(
                proposalRequest.ProjectKey, toProjectContactKey);
        }

        private void AppendFrom(ProposalRequest proposalRequest,
            object fromEmployeeKey)
        {
            proposalRequest.From =
                EmployeeService.GetEmployee(fromEmployeeKey);
        }

        private void AppendContractor(ProposalRequest proposalRequest,
            object contractorKey)
        {
            proposalRequest.Contractor =
                CompanyService.GetCompany(contractorKey);
        }

        #endregion

        #region IProposalRequestRepository Members

        public IList<ProposalRequest> FindBy(Project project)
        {
            return
                NumberedProjectChildRepositoryHelper.FindBy
                <ProposalRequest>(this, project);
        }

        public int GetExpectedContractorReturnDays()
        {
            int configExpectedContractorReturnDays = 0;
            if (ConfigurationManager.AppSettings[
                ProposalRequestRepository.ExpectedContractorReturnDaysConfigName].Length > 0)
            {
                int.TryParse(ConfigurationManager.AppSettings[
                    ProposalRequestRepository.ExpectedContractorReturnDaysConfigName],
                    out configExpectedContractorReturnDays);
            }
            return configExpectedContractorReturnDays;
        }

        #endregion
    }
}
