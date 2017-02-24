using System;
using System.Collections.Generic;
using SmartCA.Infrastructure;
using SmartCA.Infrastructure.RepositoryFramework;

namespace SmartCA.Model.Employees
{
    public static class EmployeeService
    {
        private static IEmployeeRepository repository;
        private static IUnitOfWork unitOfWork;

        static EmployeeService()
        {
            EmployeeService.unitOfWork = new UnitOfWork();
            EmployeeService.repository 
                = RepositoryFactory.GetRepository<IEmployeeRepository, 
                Employee>(EmployeeService.unitOfWork);
        }

        public static Employee GetEmployee(object employeeKey)
        {
            return EmployeeService.repository.FindBy(employeeKey);
        }

        public static IList<Employee> GetEmployees()
        {
            return EmployeeService.repository.FindAll();
        }

        public static IList<Employee> GetConstructionAdministrators()
        {
            return EmployeeService.repository.GetConstructionAdministrators();
        }

        public static IList<Employee> GetPrincipals()
        {
            return EmployeeService.repository.GetPrincipals();
        }
    }
}
