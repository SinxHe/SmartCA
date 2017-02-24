using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartCA.Infrastructure;
using SmartCA.Infrastructure.RepositoryFramework;
using SmartCA.Model.ChangeOrders;
using SmartCA.Model.Companies;
using SmartCA.Model.ConstructionChangeDirectives;
using SmartCA.Model.Employees;
using SmartCA.Model.Projects;

namespace SmartCA.UnitTests
{


    /// <summary>
    ///This is a test class for ConstructionChangeDirectiveRepository and is intended
    ///to contain all ConstructionChangeDirectiveRepository Unit Tests
    ///</summary>
    [TestClass()]
    public class ConstructionChangeDirectiveRepositoryTest
    {
        private TestContext testContextInstance;
        private IConstructionChangeDirectiveRepository repository;
        private IUnitOfWork unitOfWork;

        public ConstructionChangeDirectiveRepositoryTest()
        {
            this.unitOfWork = new UnitOfWork();
            this.repository = RepositoryFactory.GetRepository<IConstructionChangeDirectiveRepository, ConstructionChangeDirective>(this.unitOfWork);
        }

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        /// <summary>
        ///A test for FindBy
        ///</summary>
        [TestMethod]
        [DeploymentItem("SmartCA.sdf")]
        public void FindConstructionChangeDirectiveByKey()
        {
            ConstructionChangeDirective ccd = this.repository.FindBy("1a2bfe3b-7577-456d-8484-4f34f67ce42c");
            Assert.AreEqual(1, ccd.Number);
        }

        [TestMethod]
        [DeploymentItem("SmartCA.sdf")]
        public void FindConstructionChangeDirectivesByProject()
        {
            Project project = ProjectService.GetProject("5704f6b9-6ffa-444c-9583-35cc340fce2a");
            IList<ConstructionChangeDirective> changeDirectives = this.repository.FindBy(project);
            Assert.AreEqual(1, changeDirectives.Count);
        }

        [TestMethod]
        [DeploymentItem("SmartCA.sdf")]
        public void SaveNewConstructionChangeDirective()
        {
            Project project = ProjectService.GetAllProjects()[0];
            ConstructionChangeDirective ccd = new ConstructionChangeDirective(project.Key, 5);
            ccd.AmountChanged = 150;
            ccd.ChangeType = PriceChangeType.ContractSum;
            ccd.PriceChangeDirection = ChangeDirection.Increased;
            ccd.To = project.Contacts[0];
            ccd.From = EmployeeService.GetEmployees()[0];
            ccd.Contractor = CompanyService.GetAllCompanies()[0];
            ccd.Description = "This is a test.";
            ccd.TimeChanged = 2;
            ccd.TimeChangeDirection = ChangeDirection.Increased;
            this.repository[ccd.Key] = ccd;
            this.unitOfWork.Commit();
            
            // Verify that the CCD was added
            ConstructionChangeDirective newCcd = this.repository.FindBy(ccd.Key);
            Assert.AreEqual(5, newCcd.Number);
        }

        [TestMethod]
        [DeploymentItem("SmartCA.sdf")]
        public void SaveExistingConstructionChangeDirective()
        {
            ConstructionChangeDirective ccd = this.repository.FindBy("1a2bfe3b-7577-456d-8484-4f34f67ce42c");
            this.testContextInstance.WriteLine("Copy To List Count:  {0}", ccd.CopyToList.Count);
            DateTime signatureDate = DateTime.Now.Date;
            ccd.ArchitectSignatureDate = signatureDate;
            this.repository[ccd.Key] = ccd;
            this.unitOfWork.Commit();

            // Verify that the CCD was updated
            ConstructionChangeDirective newCcd = this.repository.FindBy(ccd.Key);
            Assert.AreEqual(signatureDate, newCcd.ArchitectSignatureDate.Value);
        }
    }
}