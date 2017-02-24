using System;
using System.Collections.Generic;
using SmartCA.Infrastructure;
using SmartCA.Infrastructure.RepositoryFramework;

namespace SmartCA.Model.Companies
{
    public static class CompanyService
    {
        private static ICompanyRepository repository;
        private static IUnitOfWork unitOfWork;

        static CompanyService()
        {
            CompanyService.unitOfWork = new UnitOfWork();
            CompanyService.repository = 
                RepositoryFactory.GetRepository<ICompanyRepository, 
                Company>(CompanyService.unitOfWork);
        }

        public static IList<Company> GetOwners()
        {
            return CompanyService.GetAllCompanies();
        }

        public static IList<Company> GetAllCompanies()
        {
            return CompanyService.repository.FindAll();
        }

        public static void SaveCompany(Company company)
        {
            CompanyService.repository[company.Key] = company;
            CompanyService.unitOfWork.Commit();
        }

        public static Company GetCompany(object companyKey)
        {
            return CompanyService.repository.FindBy(companyKey);
        }
    }
}
