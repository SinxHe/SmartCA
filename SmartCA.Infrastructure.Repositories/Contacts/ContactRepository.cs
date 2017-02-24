using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartCA.Model.Contacts;
using System.Data;

namespace SmartCA.Infrastructure.Repositories
{
    public class ContactRepository : SqlCeRepositoryBase<Contact>, IContactRepository
    {
        #region Private Fields

        #endregion

        #region Public Constructors

        public ContactRepository()
            : this(null)
        {
        }

        public ContactRepository(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }

        #endregion

        #region BuildChildCallbacks

        protected override void BuildChildCallbacks()
        {
            this.ChildCallbacks.Add("addresses",
                delegate(Contact contact, object childKeyName)
                {
                    this.AppendAddresses(contact);
                });
        }

        #endregion

        #region GetBaseQuery

        protected override string GetBaseQuery()
        {
            return "SELECT * FROM Contact";
        }

        #endregion

        #region GetBaseWhereClause

        protected override string GetBaseWhereClause()
        {
            return " WHERE ContactID = '{0}';";
        }

        #endregion

        #region GetEntityName

        protected override string GetEntityName()
        {
            return "Contact";
        }

        #endregion

        #region GetKeyFieldName

        protected override string GetKeyFieldName()
        {
            return ContactFactory.FieldNames.ContactId;
        }

        #endregion

        #region Unit of Work Implementation

        protected override void PersistNewItem(Contact item)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void PersistUpdatedItem(Contact item)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion

        #region Private Callback and Helper Methods

        private void AppendAddresses(Contact contact)
        {
            string sql =
                string.Format("SELECT * FROM ContactAddress WHERE ContactID = '{0}'", contact.Key);
            using (IDataReader reader = this.ExecuteReader(sql))
            {
                while (reader.Read())
                {
                    contact.Addresses.Add(AddressFactory.BuildAddress(reader));
                }
            }
        }

        #endregion
    }
}
