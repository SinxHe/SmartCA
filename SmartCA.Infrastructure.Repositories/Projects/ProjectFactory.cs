using System.Data;
using SmartCA.Infrastructure.EntityFactoryFramework;
using SmartCA.Infrastructure.Helpers;
using SmartCA.Infrastructure.RepositoryFramework;
using SmartCA.Model.Companies;
using SmartCA.Model.Contacts;
using SmartCA.Model.Projects;

namespace SmartCA.Infrastructure.Repositories
{
    internal class ProjectFactory : IEntityFactory<Project>
    {
        #region Field Names

        internal static class FieldNames
        {
            public const string ProjectId = "ProjectID";
            public const string ProjectNumber = "ProjectNumber";
            public const string ProjectName = "ProjectName";
            public const string ConstructionAdministratorEmployeeId = "ConstructionAdministratorEmployeeID";
            public const string PrincipalEmployeeId = "PrincipalEmployeeID";
            public const string ProjectAddress = "Street";
            public const string ProjectCity = "City";
            public const string ProjectState = "State";
            public const string ProjectPostalCode = "PostalCode";
            public const string OwnerCompanyId = "OwnerCompanyID";
            public const string ContractDate = "ContractDate";
            public const string EstimatedStartDate = "EstimatedStartDate";
            public const string EstimatedCompletionDate = "EstimatedCompletionDate";
            public const string CurrentCompletionDate = "CurrentCompletionDate";
            public const string ActualCompletionDate = "ActualCompletionDate";
            public const string ContingencyAllowanceAmount = "ContingencyAllowanceAmount";
            public const string TestingAllowanceAmount = "TestingAllowanceAmount";
            public const string UtilityAllowanceAmount = "UtilityAllowanceAmount";
            public const string OriginalConstructionCost = "OriginalConstructionCost";
            public const string AeChangeOrderAmount = "AEChangeOrderAmount";
            public const string ContractReason = "ContractReason";
            public const string TotalSquareFeet = "TotalSquareFeet";
            public const string PercentComplete = "PercentComplete";
            public const string Remarks = "Remarks";
            public const string AgencyApplicationNumber = "AgencyApplicationNumber";
            public const string AgencyFileNumber = "AgencyFileNumber";
            public const string MarketSegmentId = "MarketSegmentID";
            public const string MarketSegmentCode = "Code";
            public const string MarketSegmentName = "MarketSegmentName";
            public const string AllowanceTitle = "AllowanceTitle";
            public const string AllowanceAmount = "Amount";
            public const string ProjectContactId = "ProjectContactID";
            public const string ContactId = "ContactID";
            public const string OnFinalDistributionList = "OnFinalDistributionList";

            public const string ContractId = "ContractID";
            public const string CompanyId = "CompanyID";
            public const string Scope = "Scope";
            public const string BidPackageNumber = "BidPackageNumber";
            public const string NoticeToProceed = "NoticeToProceed";
            public const string ContractAmount = "ContractAmount";
        }

        #endregion

        #region IEntityFactory<Project> Members

        public Project BuildEntity(IDataReader reader)
        {
            Project project = new Project(reader[FieldNames.ProjectId],
                              reader[FieldNames.ProjectNumber].ToString(),
                              reader[FieldNames.ProjectName].ToString());
            project.Address = AddressFactory.BuildAddress(reader);
            project.ContractDate = DataHelper.GetNullableDateTime(reader[FieldNames.ContractDate]);
            project.EstimatedStartDate = DataHelper.GetNullableDateTime(reader[FieldNames.EstimatedStartDate]);
            project.EstimatedCompletionDate = DataHelper.GetNullableDateTime(reader[FieldNames.EstimatedCompletionDate]);
            project.CurrentCompletionDate = DataHelper.GetNullableDateTime(reader[FieldNames.CurrentCompletionDate]);
            project.ActualCompletionDate = DataHelper.GetNullableDateTime(reader[FieldNames.ActualCompletionDate]);
            project.ContingencyAllowanceAmount = DataHelper.GetDecimal(reader[FieldNames.ContingencyAllowanceAmount]);
            project.TestingAllowanceAmount = DataHelper.GetDecimal(reader[FieldNames.TestingAllowanceAmount]);
            project.UtilityAllowanceAmount = DataHelper.GetDecimal(reader[FieldNames.UtilityAllowanceAmount]);
            project.OriginalConstructionCost = DataHelper.GetDecimal(reader[FieldNames.OriginalConstructionCost]);
            project.TotalSquareFeet = DataHelper.GetInteger(reader[FieldNames.TotalSquareFeet]);
            project.PercentComplete = DataHelper.GetInteger(reader[FieldNames.PercentComplete]);
            project.Remarks = reader[FieldNames.Remarks].ToString();
            project.AeChangeOrderAmount = DataHelper.GetDecimal(reader[FieldNames.AeChangeOrderAmount]);
            project.ContractReason = reader[FieldNames.ContractReason].ToString();
            project.AgencyApplicationNumber = reader[FieldNames.AgencyApplicationNumber].ToString();
            project.AgencyFileNumber = reader[FieldNames.AgencyFileNumber].ToString();
            project.Segment = new MarketSegment(reader[FieldNames.MarketSegmentId],
                                  null,
                                  reader[FieldNames.MarketSegmentCode].ToString(),
                                  reader[FieldNames.MarketSegmentName].ToString());
            return project;
        }

        #endregion

        internal static Allowance BuildAllowance(IDataReader reader)
        {
            return new Allowance(reader[FieldNames.AllowanceTitle].ToString(), 
                       DataHelper.GetDecimal(reader[FieldNames.AllowanceAmount]));
        }

        internal static ProjectContact BuildProjectContact(Project project, IDataReader reader)
        {
            ProjectContact contact = new ProjectContact(project, 
                                         DataHelper.GetGuid(reader[FieldNames.ProjectContactId]), 
                                         ContactService.GetContact(reader[FieldNames.ContactId]));
            contact.OnFinalDistributionList = DataHelper.GetBoolean(reader[FieldNames.OnFinalDistributionList]);
            return contact;
        }

        internal static Contract BuildContract(IDataReader reader)
        {
            Contract contract = new Contract(reader[FieldNames.ContractId]);
            contract.BidPackageNumber = reader[FieldNames.BidPackageNumber].ToString();
            contract.ContractAmount = DataHelper.GetDecimal(reader[FieldNames.ContractAmount]);
            contract.ContractDate = DataHelper.GetNullableDateTime(reader[FieldNames.ContractDate]);
            ICompanyRepository repository = RepositoryFactory.GetRepository<ICompanyRepository, Company>();
            contract.Contractor = repository.FindBy(reader[FieldNames.CompanyId]);
            contract.NoticeToProceedDate = DataHelper.GetNullableDateTime(reader[FieldNames.NoticeToProceed]);
            contract.ScopeOfWork = reader[FieldNames.Scope].ToString();
            return contract;
        }
    }
}
