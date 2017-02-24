using System.Data;
using SmartCA.Infrastructure.EntityFactoryFramework;
using SmartCA.Infrastructure.Helpers;
using SmartCA.Model.Companies;

namespace SmartCA.Infrastructure.Repositories
{
    internal class CompanyFactory : IEntityFactory<Company>
    {
        #region Field Names

        internal static class FieldNames
        {
            public const string CompanyId = "CompanyID";
            public const string CompanyName = "CompanyName";
            public const string CompanyShortName = "CompanyShortName";
            public const string Phone = "Phone";
            public const string Fax = "Fax";
            public const string Url = "URL";
            public const string Remarks = "Remarks";
            public const string IsHeadquarters = "IsHeadquarters";
        }

        #endregion

        #region IEntityFactory<Company> Members

        public Company BuildEntity(IDataReader reader)
        {
 	        Company company = new Company(reader[FieldNames.CompanyId]);
            company.Name = reader[FieldNames.CompanyName].ToString();
            company.Abbreviation = reader[FieldNames.CompanyShortName].ToString();
            company.PhoneNumber = reader[FieldNames.Phone].ToString();
            company.FaxNumber = reader[FieldNames.Fax].ToString();
            company.Url = reader[FieldNames.Url].ToString();
            company.Remarks = reader[FieldNames.Remarks].ToString();
            return company;
        }

        internal static bool IsHeadquartersAddress(IDataReader reader)
        {
            return DataHelper.GetBoolean(reader[FieldNames.IsHeadquarters]);
        }

        #endregion
    }
}
