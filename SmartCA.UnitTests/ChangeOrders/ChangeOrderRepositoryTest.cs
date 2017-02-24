using SmartCA.Infrastructure.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartCA.Model.ChangeOrders;
using System;
using SmartCA.Model.Projects;
using System.Collections.Generic;
using SmartCA.Infrastructure.RepositoryFramework;
using SmartCA.Model.Companies;
using SmartCA.Model;
using SmartCA.Infrastructure;

namespace SmartCA.UnitTests
{
    
    
    /// <summary>
    ///This is a test class for ChangeOrderRepositoryTest and is intended
    ///to contain all ChangeOrderRepositoryTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ChangeOrderRepositoryTest
    {
        private TestContext testContextInstance;
        private IChangeOrderRepository repository;
        private IUnitOfWork unitOfWork;

        public ChangeOrderRepositoryTest()
        {
            this.unitOfWork = new UnitOfWork();
            this.repository = RepositoryFactory.GetRepository<IChangeOrderRepository, ChangeOrder>(this.unitOfWork);
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

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for FindBy
        ///</summary>
        [TestMethod]
        public void FindChangeOrderByKey()
        {
            ChangeOrder co = this.repository.FindBy("df501465-2ffd-4f8c-870f-ce169c02f6a7");
            Assert.AreEqual(2, co.Number);
        }

        [TestMethod]
        [DeploymentItem("SmartCA.sdf")]
        public void FindChangeOrdersByProject()
        {
            Project project = ProjectService.GetAllProjects()[0];
            IList<ChangeOrder> changeOrders = this.repository.FindBy(project);
            Assert.AreEqual(4, changeOrders.Count);
        }

        [TestMethod]
        [DeploymentItem("SmartCA.sdf")]
        public void SaveNewChangeOrder()
        {
            Project project = ProjectService.GetAllProjects()[0];
            ChangeOrder co = new ChangeOrder(project.Key, 5);
            co.AmountChanged = 150;
            co.ChangeType = PriceChangeType.ContractSum;
            co.PriceChangeDirection = ChangeDirection.Increased;
            co.Contractor = CompanyService.GetAllCompanies()[0];
            co.Description = "This is a test.";
            co.TimeChanged = 2;
            co.TimeChangeDirection = ChangeDirection.Increased;
            co.Status = new ItemStatus(8, "Pend. Arch.");
            this.repository[co.Key] = co;
            this.unitOfWork.Commit();

            // Verify that the Change Order was added
            ChangeOrder newChangeOrder = this.repository.FindBy(co.Key);
            Assert.AreEqual(5, newChangeOrder.Number);
        }

        [TestMethod]
        [DeploymentItem("SmartCA.sdf")]
        public void SaveExistingChangeOrder()
        {
            ChangeOrder co = this.repository.FindAll()[0];
            this.testContextInstance.WriteLine("Routing Items Count:  {0}", co.RoutingItems.Count);
            DateTime agencyApprovedDate = DateTime.Now.Date;
            co.AgencyApprovedDate = agencyApprovedDate;
            this.repository[co.Key] = co;
            this.unitOfWork.Commit();

            // Verify that the Change Order was updated
            ChangeOrder newChangeOrder = this.repository.FindBy(co.Key);
            Assert.AreEqual(agencyApprovedDate, newChangeOrder.AgencyApprovedDate);
        }

        /// <summary>
        ///A test for GetPreviousTimeChangedTotalFromTest
        ///</summary>
        [TestMethod]
        [DeploymentItem("SmartCA.sdf")]
        public void GetPreviousTimeChangedTotalFromTest()
        {
            ChangeOrder co = this.repository.FindBy("8d44e769-2f9d-45e8-ba41-724a8947553c");
            int expected = 3;
            int actual = this.repository.GetPreviousTimeChangedTotalFrom(co);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetPreviousAuthorizedAmountFromTest
        ///</summary>
        [TestMethod]
        [DeploymentItem("SmartCA.sdf")]
        public void GetPreviousAuthorizedAmountFromTest()
        {
            ChangeOrder co = this.repository.FindBy("8d44e769-2f9d-45e8-ba41-724a8947553c");
            decimal expected = 3000M;
            decimal actual = this.repository.GetPreviousAuthorizedAmountFrom(co);
            Assert.AreEqual(expected, actual);
        }
    }
}
