using System;
using SmartCA.Model.Contacts;
using SmartCA.Infrastructure.EntityFactoryFramework;
using System.Data;
using SmartCA.Model;

namespace SmartCA.Infrastructure.Repositories
{
    internal class ContactFactory : IEntityFactory<Contact>
    {
        #region Field Names

        internal static class FieldNames
        {
            public const string ContactId = "ContactID";
            public const string CompanyId = "CompanyID";
            public const string FirstName = "FirstName";
            public const string LastName = "LastName";
            public const string JobTitle = "JobTitle";
            public const string Email = "Email";
            public const string Phone = "Phone";
            public const string MobilePhone = "MobilePhone";
            public const string Fax = "Fax";
            public const string Remarks = "Remarks";
        }

        #endregion

        #region IEntityFactory<Contact> Members

        public Contact BuildEntity(IDataReader reader)
        {
            Contact contact = new Contact(reader[FieldNames.ContactId], 
                                  reader[FieldNames.FirstName].ToString(), 
                                  reader[FieldNames.LastName].ToString());
            contact.Email = reader[FieldNames.Email].ToString();
            contact.FaxNumber = reader[FieldNames.Fax].ToString();
            contact.FirstName = reader[FieldNames.FirstName].ToString();
            contact.LastName = reader[FieldNames.LastName].ToString();
            contact.MobilePhoneNumber = reader[FieldNames.MobilePhone].ToString();
            contact.PhoneNumber = reader[FieldNames.Phone].ToString();
            contact.Remarks = reader[FieldNames.Remarks].ToString();
            return contact;
        }

        #endregion
    }
}
