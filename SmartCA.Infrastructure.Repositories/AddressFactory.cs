using System;
using System.Data;
using SmartCA.Model.Addresses;

namespace SmartCA.Infrastructure.Repositories
{
    internal static class AddressFactory
    {
        #region Field Names

        internal static class FieldNames
        {
            public const string Street = "Street";
            public const string City = "City";
            public const string State = "State";
            public const string PostalCode = "PostalCode";
        }

        #endregion

        internal static Address BuildAddress(IDataReader reader)
        {
            return new Address(reader[FieldNames.Street].ToString(),
                       reader[FieldNames.City].ToString(),
                       reader[FieldNames.State].ToString(),
                       reader[FieldNames.PostalCode].ToString());
        }
    }
}
