using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartCA.Infrastructure;
using SmartCA.Infrastructure.RepositoryFramework;
using SmartCA.Model.Companies;
using SmartCA.Model.Employees;
using SmartCA.Model.Projects;
using SmartCA.Model.ProposalRequests;
using System;

namespace SmartCA.UnitTests.RFI
{
    /// <summary>
    /// Summary description for ProposalRequestRepositoryUnitTest
    /// </summary>
    [TestClass]
    public class ProposalRequestRepositoryUnitTest
    {
        private TestContext testContextInstance;
        private IUnitOfWork unitOfWork;
        private IProposalRequestRepository repository;

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

        // Use TestInitialize to run code before running each test 
        [TestInitialize()]
        public void MyTestInitialize()
        {
            this.unitOfWork = new UnitOfWork();
            this.repository = RepositoryFactory.GetRepository<IProposalRequestRepository,
                ProposalRequest>(this.unitOfWork);
        }

        #endregion

        [DeploymentItem("SmartCA.sdf"), TestMethod()]
        public void FindProposalRequestByKeyTest()
        {
            ProposalRequest pr = this.repository.FindBy("d41f41e1-47c6-43fc-ae82-e1eed25efa44");
            Assert.AreEqual(1, pr.Number);
        }

        /// <summary>
        /// A test for FindBy(Project project)
        /// </summary>
        [DeploymentItem("SmartCA.sdf"), TestMethod()]
        public void FindProposalRequestsByProjectTest()
        {
            // Get a Project reference
            Project project = ProjectService.GetAllProjects()[0];

            // Finds all of the Proposal Requests for the Project
            IList<ProposalRequest> proposalRequests =
                this.repository.FindBy(project);

            // Verify that at least one ProposalRequest was returned
            Assert.IsTrue(proposalRequests.Count > 0);
        }

        /// <summary>
        ///A test for Add(ProposalRequest item)
        ///</summary>
        [DeploymentItem("SmartCA.sdf"), TestMethod()]
        public void AddProposalRequestTest()
        {
            // Create a new ProposalRequest
            Project project = ProjectService.GetAllProjects()[0];
            ProposalRequest pr = new ProposalRequest(project.Key, 2);
            pr.From = EmployeeService.GetEmployees()[0];
            pr.Contractor = CompanyService.GetAllCompanies()[0];
            pr.To = project.Contacts[0];

            this.repository.SetUnitOfWork(this.unitOfWork);

            // Add the ProposalRequest to the Repository
            this.repository.Add(pr);

            // Commit the transaction
            this.unitOfWork.Commit();

            // Reload the ProposalRequest and verify it's number
            ProposalRequest savedPr =
                this.repository.FindBy(pr.Key);
            Assert.AreEqual(2, savedPr.Number);
        }

        /// <summary>
        ///A test for Updating a Proposal Request
        ///</summary>
        [DeploymentItem("SmartCA.sdf"), TestMethod()]
        public void UpdateProposalRequestTest()
        {
            IList<ProposalRequest> proposalRequests =
                this.repository.FindAll();

            // Change the Proposal Request's Description value
            proposalRequests[0].Description = "Test Description";

            // Update the Repository
            this.repository.SetUnitOfWork(this.unitOfWork);
            this.repository[proposalRequests[0].Key] = proposalRequests[0];

            // Commit the transaction
            this.unitOfWork.Commit();

            // Verify that the change was saved
            IList<ProposalRequest> refreshedProposalRequests =
                this.repository.FindAll();
            Assert.AreEqual("Test Description",
                refreshedProposalRequests[0].Description);
        }

        /// <summary>
        ///A test for Remove(ProposalRequest item)
        ///</summary>
        [DeploymentItem("SmartCA.sdf"), TestMethod()]
        public void RemoveProposalRequestTest()
        {
            IList<ProposalRequest> proposalRequests =
                this.repository.FindAll();

            int expectedCount = proposalRequests.Count - 1;

            // Remove the Proposal Request from the Repository
            this.repository.Remove(proposalRequests[0]);

            // Commit the transaction
            this.unitOfWork.Commit();

            // Verify that there is now one less Proposal Request in the data store
            IList<ProposalRequest> refreshedProposalRequests =
                this.repository.FindAll();
            Assert.AreEqual(expectedCount, refreshedProposalRequests.Count);

            this.AddProposalRequestTest();
        }
    }
}
