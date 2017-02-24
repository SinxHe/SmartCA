using System.Data;
using System.Text;
using SmartCA.Infrastructure.Helpers;
using SmartCA.Model.Addresses;
using SmartCA.Model.Companies;

namespace SmartCA.Infrastructure.Repositories
{
    public class CompanyRepository : SqlCeRepositoryBase<Company>, ICompanyRepository
    {
        #region Private Fields

        #endregion

        #region Public Constructors

        public CompanyRepository()
            : this(null)
        {
        }

        public CompanyRepository(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }

        #endregion

        #region BuildChildCallbacks

        protected override void BuildChildCallbacks()
        {
            this.ChildCallbacks.Add("addresses",
                delegate(Company company, object childKeyName)
                {
                    this.AppendAddresses(company);
                });
        }

        #endregion

        #region GetBaseQuery

        protected override string GetBaseQuery()
        {
            return "SELECT * FROM Company";
        }

        #endregion

        #region GetBaseWhereClause

        protected override string GetBaseWhereClause()
        {
            return " WHERE CompanyID = '{0}';";
        }

        #endregion

        #region GetEntityName

        protected override string GetEntityName()
        {
            return "Company";
        }

        #endregion

        #region GetKeyFieldName

        protected override string GetKeyFieldName()
        {
            return CompanyFactory.FieldNames.CompanyId;
        }

        #endregion

        #region Unit of Work Implementation

        protected override void PersistNewItem(Company item)
        {
            StringBuilder builder = new StringBuilder(100);
            builder.Append(string.Format("INSERT INTO Company ({0},{1},{2},{3},{4},{5},{6}) ",
                CompanyFactory.FieldNames.CompanyId,
                CompanyFactory.FieldNames.CompanyName,
                CompanyFactory.FieldNames.CompanyShortName,
                CompanyFactory.FieldNames.Phone,
                CompanyFactory.FieldNames.Fax,
                CompanyFactory.FieldNames.Url,
                CompanyFactory.FieldNames.Remarks));
            builder.Append(string.Format("VALUES ({0},{1},{2},{3},{4},{5},{6});",
                DataHelper.GetSqlValue(item.Key),
                DataHelper.GetSqlValue(item.Name),
                DataHelper.GetSqlValue(item.Abbreviation),
                DataHelper.GetSqlValue(item.PhoneNumber),
                DataHelper.GetSqlValue(item.FaxNumber),
                DataHelper.GetSqlValue(item.Url),
                DataHelper.GetSqlValue(item.Remarks)));

            this.Database.ExecuteNonQuery(
                this.Database.GetSqlStringCommand(builder.ToString()));

            // Now do the addresses
            this.InsertAddresses(item);
        }

        protected override void PersistUpdatedItem(Company item)
        {
            StringBuilder builder = new StringBuilder(100);
            builder.Append("UPDATE Company SET ");

            builder.Append(string.Format("{0} = {1}",
                CompanyFactory.FieldNames.CompanyName,
                DataHelper.GetSqlValue(item.Name)));

            builder.Append(string.Format(",{0} = {1}",
                CompanyFactory.FieldNames.CompanyShortName,
                DataHelper.GetSqlValue(item.Abbreviation)));

            builder.Append(string.Format(",{0} = {1}",
                CompanyFactory.FieldNames.Phone,
                DataHelper.GetSqlValue(item.PhoneNumber)));

            builder.Append(string.Format(",{0} = {1}",
                CompanyFactory.FieldNames.Fax,
                DataHelper.GetSqlValue(item.FaxNumber)));

            builder.Append(string.Format(",{0} = {1}",
                CompanyFactory.FieldNames.Url,
                DataHelper.GetSqlValue(item.Url)));

            builder.Append(string.Format(",{0} = {1}",
                CompanyFactory.FieldNames.Remarks,
                DataHelper.GetSqlValue(item.Remarks)));

            builder.Append(" ");
            builder.Append(this.BuildBaseWhereClause(item.Key));

            this.Database.ExecuteNonQuery(
                this.Database.GetSqlStringCommand(builder.ToString()));

            // Now do the addresses

            // First, delete the existing ones
            this.DeleteAddresses(item);

            // Now, add the current ones
            this.InsertAddresses(item);
        }

        protected override void PersistDeletedItem(Company item)
        {
            // Delete the company addresses first
            this.DeleteAddresses(item);

            // Now delete the company
            base.PersistDeletedItem(item);
        }

        #endregion

        #region Private Callback and Helper Methods

        private void DeleteAddresses(Company company)
        {
            string query = string.Format("DELETE FROM CompanyAddress {0}",
                this.BuildBaseWhereClause(company.Key));
            this.Database.ExecuteNonQuery(
                this.Database.GetSqlStringCommand(query));
        }

        private void InsertAddresses(Company company)
        {
            foreach (Address address in company.Addresses)
            {
                this.InsertAddress(address, company.Key, 
                    (company.HeadquartersAddress == address));
            }
        }

        private void InsertAddress(Address address, object key, 
            bool isHeadquartersAddress)
        {
            StringBuilder builder = new StringBuilder(100);
            builder.Append(string.Format("INSERT INTO CompanyAddress ({0},{1},{2},{3},{4},{5}) ",
                CompanyFactory.FieldNames.CompanyId,
                AddressFactory.FieldNames.Street,
                AddressFactory.FieldNames.City,
                AddressFactory.FieldNames.State,
                AddressFactory.FieldNames.PostalCode,
                CompanyFactory.FieldNames.IsHeadquarters));
            builder.Append(string.Format("VALUES ({0},{1},{2},{3},{4},{5});",
                DataHelper.GetSqlValue(key),
                DataHelper.GetSqlValue(address.Street),
                DataHelper.GetSqlValue(address.City),
                DataHelper.GetSqlValue(address.State),
                DataHelper.GetSqlValue(address.PostalCode),
                DataHelper.GetSqlValue(isHeadquartersAddress)));

            this.Database.ExecuteNonQuery(
                this.Database.GetSqlStringCommand(builder.ToString()));
        }

        private void AppendAddresses(Company company)
        {
            string sql = string.Format
                ("SELECT * FROM CompanyAddress WHERE CompanyID = '{0}'", 
                company.Key);
            using (IDataReader reader = this.ExecuteReader(sql))
            {
                Address address = null;
                while (reader.Read())
                {
                    address = AddressFactory.BuildAddress(reader);
                    company.Addresses.Add(address);
                    if (CompanyFactory.IsHeadquartersAddress(reader))
                    {
                        company.HeadquartersAddress = address;
                    }
                }
            }
        }

        #endregion
    }
}
