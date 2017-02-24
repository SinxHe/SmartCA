using SmartCA.Infrastructure.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartCA.Model.Companies;
using System.Collections.Generic;
using SmartCA.Infrastructure.RepositoryFramework;
using SmartCA.Infrastructure;
using System;

namespace SmartCA.UnitTests
{
    /// <summary>
    ///This is a test class for ProjectRepositoryTest and is intended
    ///to contain all ProjectRepositoryTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CompanyRepositoryTest
    {
        private TestContext testContextInstance;
        private IUnitOfWork unitOfWork;
        private ICompanyRepository repository;

        public CompanyRepositoryTest()
        {
            this.unitOfWork = new UnitOfWork();
            this.repository = RepositoryFactory.GetRepository<ICompanyRepository,
                Company>(this.unitOfWork);
        }

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return this.testContextInstance;
            }
            set
            {
                this.testContextInstance = value;
            }
        }

        /// <summary>
        ///A test for FindBy(object key)
        ///</summary>
        [DeploymentItem("SmartCA.sdf"), TestMethod()]
        public void FindByKeyTest()
        {
            // Set the Key value
            object key = "8b6a05be-6106-45fb-b6cc-b03cfa5ab74b";
            // Find the Company
            Company company = this.repository.FindBy(key);
            // Verify the Company's name
            Assert.AreEqual("My Company", company.Name);
        }

        /// <summary>
        ///A test for FindAll()
        ///</summary>
        [DeploymentItem("SmartCA.sdf"), TestMethod()]
        public void FindAllTest()
        {
            // Get all of the Companies
            IList<Company> companies = this.repository.FindAll();
            // Make sure there are two
            Assert.AreEqual(2, companies.Count);
        }

        /// <summary>
        ///A test for Add(Company item)
        ///</summary>
        [DeploymentItem("SmartCA.sdf"), TestMethod()]
        public void AddTest()
        {
            // Create a new Company and give it a fake name
            Company company = new Company();
            company.Name = "My Test Company";

            //IUnitOfWork unitOfWork = new UnitOfWork();
            //ICompanyRepository repository = RepositoryFactory.GetRepository<ICompanyRepository,
            //    Company>(unitOfWork);

            // Add the Company to the Repository
            this.repository.Add(company);

            // Commit the transaction
            this.unitOfWork.Commit();

            // Reload the Company and verify it's name
            Company savedCompany = this.repository.FindBy(company.Key);
            Assert.AreEqual("My Test Company", savedCompany.Name);

            // Clean up
            this.repository.Remove(savedCompany);
            this.unitOfWork.Commit();
        }

        /// <summary>
        ///A test for Updating a Company
        ///</summary>
        [DeploymentItem("SmartCA.sdf"), TestMethod()]
        public void UpdateTest()
        {
            IUnitOfWork unitOfWork = new UnitOfWork();
            ICompanyRepository repository = RepositoryFactory.GetRepository<ICompanyRepository,
                Company>(unitOfWork);

            // Find the Company
            Company company = repository.FindAll()[0];

            // Change the Company's Name
            company.Name = "My Updated Company";

            // Update the Repository
            repository[company.Key] = company;

            // Commit the transaction
            unitOfWork.Commit();

            // Verify that the change was saved
            Company savedCompany = repository.FindBy(company.Key);
            Assert.AreEqual("My Updated Company", savedCompany.Name);
        }

        /// <summary>
        ///A test for Remove(Company item)
        ///</summary>
        [DeploymentItem("SmartCA.sdf"), TestMethod()]
        public void RemoveTest()
        {
            this.AddTest();

            IUnitOfWork unitOfWork = new UnitOfWork();
            ICompanyRepository repository = RepositoryFactory.GetRepository<ICompanyRepository,
                Company>(unitOfWork);

            int oldCount = repository.FindAll().Count;

            // Find the Company
            Company company = repository.FindAll()[oldCount - 1];

            // Remove the Company from the Repository
            repository.Remove(company);

            // Commit the transaction
            unitOfWork.Commit();

            //// Verify that there is now one less Company in the data store
            IList<Company> companies = repository.FindAll();
            Assert.AreEqual(oldCount - 1, companies.Count);
        }
    }
}