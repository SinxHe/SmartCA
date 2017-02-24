using System.Collections.Generic;
using System.Text;
using SmartCA.Infrastructure.Helpers;
using SmartCA.Infrastructure.Repositories.NumberedProjectChildren;
using SmartCA.Infrastructure.Repositories.Transmittals;
using SmartCA.Model.Projects;
using SmartCA.Model.RFI;
using SmartCA.Model.Companies;
using SmartCA.Model.Submittals;

namespace SmartCA.Infrastructure.Repositories
{
    public class RequestForInformationRepository : SqlCeRoutableTransmittalRepository<RequestForInformation>, 
        IRequestForInformationRepository
    {
        #region Private Fields

        #endregion

        #region Public Constructors

        public RequestForInformationRepository()
            : this(null)
        {
        }

        public RequestForInformationRepository(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }

        #endregion

        #region GetBaseQuery

        protected override string GetBaseQuery()
        {
            StringBuilder builder = new StringBuilder(200);
            builder.Append("SELECT * FROM RequestForInformation rfi ");
            builder.Append("INNER JOIN SpecificationSection ss ");
            builder.Append("ON rfi.SpecificationSectionID = ss.SpecificationSectionID ");
            builder.Append("INNER JOIN ItemStatus ist ");
            builder.Append("ON rfi.ItemStatusID = ist.ItemStatusID");
            return builder.ToString();
        }

        #endregion

        #region GetBaseWhereClause

        protected override string GetBaseWhereClause()
        {
            return " WHERE RequestForInformationID = '{0}';";
        }

        #endregion

        #region GetEntityName

        protected override string GetEntityName()
        {
            return "RequestForInformation";
        }

        #endregion

        #region GetKeyFieldName

        protected override string GetKeyFieldName()
        {
            return RequestForInformationFactory.FieldNames.RequestForInformationId;
        }

        #endregion

        #region BuildChildCallbacks

        protected override void BuildChildCallbacks()
        {
            this.ChildCallbacks.Add(ProjectFactory.FieldNames.ProjectContactId,
                this.AppendFrom);
            this.ChildCallbacks.Add(CompanyFactory.FieldNames.CompanyId, 
                this.AppendContractor);
            base.BuildChildCallbacks();
        }

        #endregion

        #region Unit of Work Implementation

        protected override void PersistNewItem(RequestForInformation item)
        {
            StringBuilder builder = new StringBuilder(100);
            builder.Append(string.Format("INSERT INTO RequestForInformation ({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23},{24},{25}) ",
                RequestForInformationFactory.FieldNames.RequestForInformationId,
                ProjectFactory.FieldNames.ProjectId,
                RequestForInformationFactory.FieldNames.RequestForInformationNumber,
                RequestForInformationFactory.FieldNames.TransmittalDate,
                RequestForInformationFactory.FieldNames.ProjectContactId,
                RequestForInformationFactory.FieldNames.TotalPages,
                RequestForInformationFactory.FieldNames.DeliveryMethod,
                RequestForInformationFactory.FieldNames.OtherDeliveryMethod,
                RequestForInformationFactory.FieldNames.PhaseNumber,
                RequestForInformationFactory.FieldNames.Reimbursable,
                RequestForInformationFactory.FieldNames.Final,
                RequestForInformationFactory.FieldNames.DateReceived,
                RequestForInformationFactory.FieldNames.DateRequestedBy,
                CompanyFactory.FieldNames.CompanyId,
                SubmittalFactory.FieldNames.SpecificationSectionId,
                RequestForInformationFactory.FieldNames.Question,
                RequestForInformationFactory.FieldNames.Description,
                RequestForInformationFactory.FieldNames.ContractorProposedSolution,
                RequestForInformationFactory.FieldNames.NoChange,
                RequestForInformationFactory.FieldNames.Cause,
                RequestForInformationFactory.FieldNames.Origin,
                RequestForInformationFactory.FieldNames.ItemStatusId,
                RequestForInformationFactory.FieldNames.DateToField,
                RequestForInformationFactory.FieldNames.ShortAnswer,
                RequestForInformationFactory.FieldNames.LongAnswer,
                RequestForInformationFactory.FieldNames.Remarks                
                ));
            builder.Append(string.Format("VALUES ({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23},{24},{25});",
                DataHelper.GetSqlValue(item.Key),
                DataHelper.GetSqlValue(item.ProjectKey),
                DataHelper.GetSqlValue(item.Number),
                DataHelper.GetSqlValue(item.TransmittalDate),
                DataHelper.GetSqlValue(item.From.Key),
                DataHelper.GetSqlValue(item.TotalPages),
                DataHelper.GetSqlValue(item.DeliveryMethod),
                DataHelper.GetSqlValue(item.OtherDeliveryMethod),
                DataHelper.GetSqlValue(item.PhaseNumber),
                DataHelper.GetSqlValue(item.Reimbursable),
                DataHelper.GetSqlValue(item.Final),
                DataHelper.GetSqlValue(item.DateReceived),
                DataHelper.GetSqlValue(item.DateRequestedBy),
                DataHelper.GetSqlValue(item.Contractor.Key),
                DataHelper.GetSqlValue(item.SpecSection.Key),
                DataHelper.GetSqlValue(item.Question),
                DataHelper.GetSqlValue(item.Description),
                DataHelper.GetSqlValue(item.ContractorProposedSolution),
                DataHelper.GetSqlValue(item.Change),
                DataHelper.GetSqlValue(item.Cause),
                DataHelper.GetSqlValue(item.Origin),
                DataHelper.GetSqlValue(item.Status.Id),
                DataHelper.GetSqlValue(item.DateToField),
                DataHelper.GetSqlValue(item.ShortAnswer),
                DataHelper.GetSqlValue(item.LongAnswer),
                DataHelper.GetSqlValue(item.Remarks)));

            this.Database.ExecuteNonQuery(
                this.Database.GetSqlStringCommand(builder.ToString()));

            // Now do the child objects
            this.InsertCopyToList(item);
            this.InsertRoutingItems(item);
        }

        protected override void PersistUpdatedItem(RequestForInformation item)
        {
            StringBuilder builder = new StringBuilder(100);
            builder.Append("UPDATE RequestForInformation SET ");

            builder.Append(string.Format("{0} = {1}",
                RequestForInformationFactory.FieldNames.RequestForInformationNumber,
                DataHelper.GetSqlValue(item.Number)));

            builder.Append(string.Format(",{0} = {1}",
                RequestForInformationFactory.FieldNames.TransmittalDate,
                DataHelper.GetSqlValue(item.TransmittalDate)));

            builder.Append(string.Format(",{0} = {1}",
                ProjectFactory.FieldNames.ProjectContactId,
                DataHelper.GetSqlValue(item.From.Key)));

            builder.Append(string.Format(",{0} = {1}",
                RequestForInformationFactory.FieldNames.TotalPages,
                DataHelper.GetSqlValue(item.TotalPages)));

            builder.Append(string.Format(",{0} = {1}",
                RequestForInformationFactory.FieldNames.DeliveryMethod,
                DataHelper.GetSqlValue(item.DeliveryMethod)));

            builder.Append(string.Format(",{0} = {1}",
                RequestForInformationFactory.FieldNames.OtherDeliveryMethod,
                DataHelper.GetSqlValue(item.OtherDeliveryMethod)));

            builder.Append(string.Format(",{0} = {1}",
                RequestForInformationFactory.FieldNames.PhaseNumber,
                DataHelper.GetSqlValue(item.PhaseNumber)));

            builder.Append(string.Format(",{0} = {1}",
                RequestForInformationFactory.FieldNames.Reimbursable,
                DataHelper.GetSqlValue(item.Reimbursable)));

            builder.Append(string.Format(",{0} = {1}",
                RequestForInformationFactory.FieldNames.Final,
                DataHelper.GetSqlValue(item.Final)));

            builder.Append(string.Format(",{0} = {1}",
                RequestForInformationFactory.FieldNames.DateReceived,
                DataHelper.GetSqlValue(item.DateReceived)));

            builder.Append(string.Format(",{0} = {1}",
                RequestForInformationFactory.FieldNames.DateRequestedBy,
                DataHelper.GetSqlValue(item.DateRequestedBy)));

            builder.Append(string.Format(",{0} = {1}",
                CompanyFactory.FieldNames.CompanyId,
                DataHelper.GetSqlValue(item.Contractor.Key)));

            builder.Append(string.Format(",{0} = {1}",
                RequestForInformationFactory.FieldNames.SpecificationSectionId,
                DataHelper.GetSqlValue(item.SpecSection.Key)));

            builder.Append(string.Format(",{0} = {1}",
                RequestForInformationFactory.FieldNames.Question,
                DataHelper.GetSqlValue(item.Question)));

            builder.Append(string.Format(",{0} = {1}",
                RequestForInformationFactory.FieldNames.Description,
                DataHelper.GetSqlValue(item.Description)));

            builder.Append(string.Format(",{0} = {1}",
                RequestForInformationFactory.FieldNames.ContractorProposedSolution,
                DataHelper.GetSqlValue(item.ContractorProposedSolution)));

            builder.Append(string.Format(",{0} = {1}",
                RequestForInformationFactory.FieldNames.NoChange,
                DataHelper.GetSqlValue(item.Change)));

            builder.Append(string.Format(",{0} = {1}",
                RequestForInformationFactory.FieldNames.Cause,
                DataHelper.GetSqlValue(item.Cause)));

            builder.Append(string.Format(",{0} = {1}",
                RequestForInformationFactory.FieldNames.Origin,
                DataHelper.GetSqlValue(item.Origin)));

            builder.Append(string.Format(",{0} = {1}",
                RequestForInformationFactory.FieldNames.ItemStatusId,
                DataHelper.GetSqlValue(item.Status.Id)));

            builder.Append(string.Format(",{0} = {1}",
                RequestForInformationFactory.FieldNames.DateToField,
                DataHelper.GetSqlValue(item.DateToField)));

            builder.Append(string.Format(",{0} = {1}",
                RequestForInformationFactory.FieldNames.ShortAnswer,
                DataHelper.GetSqlValue(item.ShortAnswer)));

            builder.Append(string.Format(",{0} = {1}",
                RequestForInformationFactory.FieldNames.LongAnswer,
                DataHelper.GetSqlValue(item.LongAnswer)));

            builder.Append(string.Format(",{0} = {1}",
                RequestForInformationFactory.FieldNames.Remarks,
                DataHelper.GetSqlValue(item.Remarks)));

            builder.Append(" ");
            builder.Append(this.BuildBaseWhereClause(item.Key));

            this.Database.ExecuteNonQuery(
                this.Database.GetSqlStringCommand(builder.ToString()));

            // Now do the child objects

            // First, delete the existing ones
            this.DeleteCopyToList(item);
            this.DeleteRoutingItems(item);

            // Now, add the current ones
            this.InsertCopyToList(item);
            this.InsertRoutingItems(item);
        }

        #endregion

        #region Private Callback and Helper Methods

        private void AppendFrom(RequestForInformation rfi, object fromProjectContactKey)
        {
            rfi.From = ProjectService.GetProjectContact(rfi.ProjectKey,
                fromProjectContactKey);
        }

        private void AppendContractor(RequestForInformation rfi, object contractorKey)
        {
            rfi.Contractor = CompanyService.GetCompany(contractorKey);
        }

        #endregion

        #region IRequestForInformationRepository Members

        public IList<RequestForInformation> FindBy(Project project)
        {
           return
                NumberedProjectChildRepositoryHelper.FindBy
                <RequestForInformation>(this, project);
        }

        #endregion
    }
}
