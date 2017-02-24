using System;
using System.Collections.Generic;
using System.Text;
using SmartCA.Model.Employees;
using System.Data;
using SmartCA.Infrastructure.EntityFactoryFramework;

namespace SmartCA.Infrastructure.Repositories
{
    internal class EmployeeFactory : IEntityFactory<Employee>
    {
        #region Field Names

        internal static class FieldNames
        {
            public const string EmployeeId = "EmployeeID";
            public const string FirstName = "FirstName";
            public const string LastName = "LastName";
        }

        #endregion

        #region IEntityFactory<Employee> Members

        public Employee BuildEntity(IDataReader reader)
        {
            Employee employee = new Employee(reader[FieldNames.EmployeeId]);
            employee.FirstName = reader[FieldNames.FirstName].ToString();
            employee.LastName = reader[FieldNames.LastName].ToString();
            return employee;
        }

        #endregion
    }
}
