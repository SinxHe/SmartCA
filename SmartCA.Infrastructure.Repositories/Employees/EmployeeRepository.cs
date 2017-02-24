using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartCA.Model.Employees;
using System.Data;

namespace SmartCA.Infrastructure.Repositories
{
    public class EmployeeRepository : SqlCeRepositoryBase<Employee>, IEmployeeRepository
    {
        #region Private Fields

        #endregion

        #region Public Constructors

        public EmployeeRepository()
            : this(null)
        {
        }

        public EmployeeRepository(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }

        #endregion

        #region IEmployeeRepository Members

        public IList<Employee> GetConstructionAdministrators()
        {
            //Construction Administrator
            StringBuilder builder = this.GetBaseQueryBuilder();
            return this.BuildEntitiesFromSql(builder.Append
                (" WHERE JobTitle LIKE '%Construction Administrator%';").ToString());
        }

        public IList<Employee> GetPrincipals()
        {
            //Principal-in-Charge
            StringBuilder builder = this.GetBaseQueryBuilder();
            return this.BuildEntitiesFromSql(builder.Append
                (" WHERE JobTitle LIKE '%Principal%';").ToString());
        }

        #endregion

        #region BuildChildCallbacks

        protected override void BuildChildCallbacks()
        {
        }

        #endregion

        #region GetBaseQuery

        protected override string GetBaseQuery()
        {
            return "SELECT * FROM Employee";
        }

        #endregion

        #region GetBaseWhereClause

        protected override string GetBaseWhereClause()
        {
            return " WHERE EmployeeID = {0};";
        }

        #endregion

        #region GetEntityName

        protected override string GetEntityName()
        {
            return "Employee";
        }

        #endregion

        #region GetKeyFieldName

        protected override string GetKeyFieldName()
        {
            return EmployeeFactory.FieldNames.EmployeeId;
        }

        #endregion

        #region Unit of Work Implementation

        protected override void PersistNewItem(Employee item)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void PersistUpdatedItem(Employee item)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion

        #region Private Callback and Helper Methods

        #endregion
    }
}
