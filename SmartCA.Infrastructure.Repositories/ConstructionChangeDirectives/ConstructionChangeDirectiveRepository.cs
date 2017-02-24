using System.Collections.Generic;
using System.Text;
using SmartCA.Infrastructure.Helpers;
using SmartCA.Infrastructure.Repositories.NumberedProjectChildren;
using SmartCA.Infrastructure.Repositories.Transmittals;
using SmartCA.Model.Companies;
using SmartCA.Model.ConstructionChangeDirectives;
using SmartCA.Model.Employees;
using SmartCA.Model.Projects;

namespace SmartCA.Infrastructure.Repositories
{
    public class ConstructionChangeDirectiveRepository 
        : SqlCeTransmittalRepository<ConstructionChangeDirective>,
        IConstructionChangeDirectiveRepository
    {
        #region Private Fields

        #endregion

        #region Public Constructors

        public ConstructionChangeDirectiveRepository()
            : this(null)
        {
        }

        public ConstructionChangeDirectiveRepository(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }

        #endregion

        #region GetBaseQuery

        protected override string GetBaseQuery()
        {
            return "SELECT * FROM ConstructionChangeDirective";
        }

        #endregion

        #region GetBaseWhereClause

        protected override string GetBaseWhereClause()
        {
            return " WHERE ConstructionChangeDirectiveID = '{0}';";
        }

        #endregion

        #region GetEntityName

        protected override string GetEntityName()
        {
            return "ConstructionChangeDirective";
        }

        #endregion

        #region GetKeyFieldName

        protected override string GetKeyFieldName()
        {
            return ConstructionChangeDirectiveFactory.FieldNames.ConstructionChangeDirectiveId;
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

        protected override void PersistNewItem(ConstructionChangeDirective item)
        {
            StringBuilder builder = new StringBuilder(100);
            builder.Append(string.Format("INSERT INTO ConstructionChangeDirective ({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23},{24},{25},{26},{27},{28},{29}) ",
                ConstructionChangeDirectiveFactory.FieldNames.ConstructionChangeDirectiveId,
                ProjectFactory.FieldNames.ProjectId,
                ConstructionChangeDirectiveFactory.FieldNames.ConstructionChangeDirectiveNumber,
                TransmittalFactory.FieldNames.TransmittalDate,
                ProjectFactory.FieldNames.ProjectContactId,
                EmployeeFactory.FieldNames.EmployeeId,
                TransmittalFactory.FieldNames.TotalPages,
                TransmittalFactory.FieldNames.DeliveryMethod,
                TransmittalFactory.FieldNames.OtherDeliveryMethod,
                TransmittalFactory.FieldNames.PhaseNumber,
                TransmittalFactory.FieldNames.Reimbursable,
                TransmittalFactory.FieldNames.Final,
                ConstructionChangeDirectiveFactory.FieldNames.IssueDate,
                CompanyFactory.FieldNames.CompanyId,
                ConstructionChangeDirectiveFactory.FieldNames.Description,
                ConstructionChangeDirectiveFactory.FieldNames.Attachment,
                ConstructionChangeDirectiveFactory.FieldNames.Reason,
                ConstructionChangeDirectiveFactory.FieldNames.Initiator,
                ConstructionChangeDirectiveFactory.FieldNames.Cause,
                ConstructionChangeDirectiveFactory.FieldNames.Origin,
                ConstructionChangeDirectiveFactory.FieldNames.Remarks,
                TransmittalFactory.FieldNames.TransmittalRemarks,
                ConstructionChangeDirectiveFactory.FieldNames.PriceChangeType,
                ConstructionChangeDirectiveFactory.FieldNames.PriceChangeTypeDirection,
                ConstructionChangeDirectiveFactory.FieldNames.AmountChanged,
                ConstructionChangeDirectiveFactory.FieldNames.TimeChangeDirection,
                ConstructionChangeDirectiveFactory.FieldNames.TimeChangedDays,
                ConstructionChangeDirectiveFactory.FieldNames.OwnerSignatureDate,
                ConstructionChangeDirectiveFactory.FieldNames.ArchitectSignatureDate,
                ConstructionChangeDirectiveFactory.FieldNames.ContractorSignatureDate
                ));
            builder.Append(string.Format("VALUES ({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23},{24},{25},{26},{27},{28},{29});",
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
                DataHelper.GetSqlValue(item.TransmittalRemarks),
                DataHelper.GetSqlValue(item.ChangeType),
                DataHelper.GetSqlValue(item.PriceChangeDirection),
                DataHelper.GetSqlValue(item.AmountChanged),
                DataHelper.GetSqlValue(item.TimeChangeDirection),
                DataHelper.GetSqlValue(item.TimeChanged),
                DataHelper.GetSqlValue(item.OwnerSignatureDate),
                DataHelper.GetSqlValue(item.ArchitectSignatureDate),
                DataHelper.GetSqlValue(item.ContractorSignatureDate)));

            this.Database.ExecuteNonQuery(
                this.Database.GetSqlStringCommand(builder.ToString()));

            // Now do the child objects
            this.InsertCopyToList(item);
        }

        protected override void PersistUpdatedItem(ConstructionChangeDirective item)
        {
            StringBuilder builder = new StringBuilder(100);
            builder.Append("UPDATE ConstructionChangeDirective SET ");

            builder.Append(string.Format("{0} = {1}",
                ConstructionChangeDirectiveFactory.FieldNames.ConstructionChangeDirectiveNumber,
                DataHelper.GetSqlValue(item.Number)));

            builder.Append(string.Format(",{0} = {1}",
                TransmittalFactory.FieldNames.TransmittalDate,
                DataHelper.GetSqlValue(item.TransmittalDate)));

            builder.Append(string.Format(",{0} = {1}",
                ProjectFactory.FieldNames.ProjectContactId,
                DataHelper.GetSqlValue(item.To.Key)));

            builder.Append(string.Format(",{0} = {1}",
                EmployeeFactory.FieldNames.EmployeeId,
                DataHelper.GetSqlValue(item.From.Key)));

            builder.Append(string.Format(",{0} = {1}",
                TransmittalFactory.FieldNames.TotalPages,
                DataHelper.GetSqlValue(item.TotalPages)));

            builder.Append(string.Format(",{0} = {1}",
                TransmittalFactory.FieldNames.DeliveryMethod,
                DataHelper.GetSqlValue(item.DeliveryMethod)));

            builder.Append(string.Format(",{0} = {1}",
                TransmittalFactory.FieldNames.OtherDeliveryMethod,
                DataHelper.GetSqlValue(item.OtherDeliveryMethod)));

            builder.Append(string.Format(",{0} = {1}",
                TransmittalFactory.FieldNames.PhaseNumber,
                DataHelper.GetSqlValue(item.PhaseNumber)));

            builder.Append(string.Format(",{0} = {1}",
                TransmittalFactory.FieldNames.Reimbursable,
                DataHelper.GetSqlValue(item.Reimbursable)));

            builder.Append(string.Format(",{0} = {1}",
                TransmittalFactory.FieldNames.Final,
                DataHelper.GetSqlValue(item.Final)));

            builder.Append(string.Format(",{0} = {1}",
                ConstructionChangeDirectiveFactory.FieldNames.IssueDate,
                DataHelper.GetSqlValue(item.IssueDate)));

            builder.Append(string.Format(",{0} = {1}",
                CompanyFactory.FieldNames.CompanyId,
                DataHelper.GetSqlValue(item.Contractor.Key)));
            
            builder.Append(string.Format(",{0} = {1}",
                ConstructionChangeDirectiveFactory.FieldNames.Description,
                DataHelper.GetSqlValue(item.Description)));

            builder.Append(string.Format(",{0} = {1}",
                ConstructionChangeDirectiveFactory.FieldNames.Attachment,
                DataHelper.GetSqlValue(item.Attachment)));

            builder.Append(string.Format(",{0} = {1}",
                ConstructionChangeDirectiveFactory.FieldNames.Reason,
                DataHelper.GetSqlValue(item.Reason)));

            builder.Append(string.Format(",{0} = {1}",
                ConstructionChangeDirectiveFactory.FieldNames.Initiator,
                DataHelper.GetSqlValue(item.Initiator)));

            builder.Append(string.Format(",{0} = {1}",
                ConstructionChangeDirectiveFactory.FieldNames.Cause,
                DataHelper.GetSqlValue(item.Cause)));

            builder.Append(string.Format(",{0} = {1}",
                ConstructionChangeDirectiveFactory.FieldNames.Origin,
                DataHelper.GetSqlValue(item.Origin)));

            builder.Append(string.Format(",{0} = {1}",
                ConstructionChangeDirectiveFactory.FieldNames.Remarks,
                DataHelper.GetSqlValue(item.Remarks)));

            builder.Append(string.Format(",{0} = {1}",
                ConstructionChangeDirectiveFactory.FieldNames.TransmittalRemarks,
                DataHelper.GetSqlValue(item.TransmittalRemarks)));

            builder.Append(string.Format(",{0} = {1}",
                ConstructionChangeDirectiveFactory.FieldNames.PriceChangeType,
                DataHelper.GetSqlValue(item.ChangeType)));

            builder.Append(string.Format(",{0} = {1}",
                ConstructionChangeDirectiveFactory.FieldNames.PriceChangeTypeDirection,
                DataHelper.GetSqlValue(item.PriceChangeDirection)));

            builder.Append(string.Format(",{0} = {1}",
                ConstructionChangeDirectiveFactory.FieldNames.AmountChanged,
                DataHelper.GetSqlValue(item.AmountChanged)));

            builder.Append(string.Format(",{0} = {1}",
                ConstructionChangeDirectiveFactory.FieldNames.TimeChangeDirection,
                DataHelper.GetSqlValue(item.TimeChangeDirection)));

            builder.Append(string.Format(",{0} = {1}",
                ConstructionChangeDirectiveFactory.FieldNames.TimeChangedDays,
                DataHelper.GetSqlValue(item.TimeChanged)));

            builder.Append(string.Format(",{0} = {1}",
                ConstructionChangeDirectiveFactory.FieldNames.OwnerSignatureDate,
                DataHelper.GetSqlValue(item.OwnerSignatureDate)));

            builder.Append(string.Format(",{0} = {1}",
                ConstructionChangeDirectiveFactory.FieldNames.ArchitectSignatureDate,
                DataHelper.GetSqlValue(item.ArchitectSignatureDate)));        

            builder.Append(string.Format(",{0} = {1}",
                ConstructionChangeDirectiveFactory.FieldNames.ContractorSignatureDate,
                DataHelper.GetSqlValue(item.ContractorSignatureDate)));

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

        private void AppendContractor(ConstructionChangeDirective ccd, 
            object contractorKey)
        {
            ccd.Contractor = CompanyService.GetCompany(contractorKey);
        }

        private void AppendTo(ConstructionChangeDirective ccd,
            object toProjectContactKey)
        {
            ccd.To = ProjectService.GetProjectContact(
                ccd.ProjectKey, toProjectContactKey);
        }

        private void AppendFrom(ConstructionChangeDirective ccd,
            object fromEmployeeKey)
        {
            ccd.From =
                EmployeeService.GetEmployee(fromEmployeeKey);
        }

        #endregion

        #region IConstructionChangeDirective Members

        public IList<ConstructionChangeDirective> FindBy(Project project)
        {
            return 
                NumberedProjectChildRepositoryHelper.FindBy
                <ConstructionChangeDirective>(this, project);
        }

        #endregion
    }
}
