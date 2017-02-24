using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartCA.Infrastructure;
using SmartCA.Infrastructure.RepositoryFramework;
using SmartCA.Model;
using SmartCA.Model.Companies;
using SmartCA.Model.Projects;
using SmartCA.Model.RFI;
using SmartCA.Model.Submittals;

namespace SmartCA.UnitTests.RFI
{
    /// <summary>
    /// Summary description for RequestForInformationRepositoryUnitTest
    /// </summary>
    [TestClass]
    public class RequestForInformationRepositoryUnitTest
    {
        private TestContext testContextInstance;
        private IUnitOfWork unitOfWork;
        private IRequestForInformationRepository repository;

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
            this.repository = RepositoryFactory.GetRepository<IRequestForInformationRepository,
                RequestForInformation>(this.unitOfWork);
        }

        #endregion

        [DeploymentItem("SmartCA.sdf"), TestMethod()]
        public void FindRfiByKeyTest()
        {
            RequestForInformation rfi = this.repository.FindBy("6c42ec93-10d7-4b55-b564-a5d1308a00a4");
            Assert.AreEqual(1, rfi.Number);
        }

        /// <summary>
        /// A test for FindBy(Project project)
        /// </summary>
        [DeploymentItem("SmartCA.sdf"), TestMethod()]
        public void FindRfisByProjectTest()
        {
            // Get a Project reference
            Project project =
                ProjectService.GetProject("5704f6b9-6ffa-444c-9583-35cc340fce2a");

            // FIns all of the RFI's for the Project
            IList<RequestForInformation> rfis = this.repository.FindBy(project);

            // Verify that at least one RFI was returned
            Assert.IsTrue(rfis.Count > 0);
        }

        /// <summary>
        ///A test for Add(RequestForInformation item)
        ///</summary>
        [DeploymentItem("SmartCA.sdf"), TestMethod()]
        public void AddRfiTest()
        {
            IList<RequestForInformation> rfis = this.repository.FindAll();

            // Create a new RequestForInformation
            Project project = ProjectService.GetAllProjects()[0];
            RequestForInformation rfi = new RequestForInformation(project.Key, 2);
            IList<ItemStatus> statuses = SubmittalService.GetItemStatuses();
            rfi.From = project.Contacts[0];
            rfi.Status = statuses[0];
            rfi.Contractor = CompanyService.GetAllCompanies()[0];
            IList<SpecificationSection> specSections =
                SubmittalService.GetSpecificationSections();
            rfi.SpecSection = specSections[0];

            // Add the RFI to the Repository
            this.repository.Add(rfi);

            // Commit the transaction
            this.unitOfWork.Commit();

            // Reload the RFI and verify it's number
            RequestForInformation savedRfi = repository.FindBy(rfi.Key);
            Assert.AreEqual(2, savedRfi.Number);
        }

        /// <summary>
        ///A test for Updating an RFI
        ///</summary>
        [DeploymentItem("SmartCA.sdf"), TestMethod()]
        public void UpdateRfiTest()
        {
            IList<RequestForInformation> rfis = this.repository.FindAll();

            // Change the RFI's DateReceived value
            DateTime dateReceived = DateTime.Now;
            rfis[0].DateReceived = dateReceived;

            // Update the Repository
            this.repository[rfis[0].Key] = rfis[0];

            // Commit the transaction
            this.unitOfWork.Commit();

            // Verify that the change was saved
            IList<RequestForInformation> refreshedRfis = repository.FindAll();
            Assert.AreEqual(dateReceived.Date,
                refreshedRfis[0].DateReceived.Value.Date);
        }

        /// <summary>
        ///A test for Remove(RequestForInformation item)
        ///</summary>
        [DeploymentItem("SmartCA.sdf"), TestMethod()]
        public void RemoveRfiTest()
        {
            IList<RequestForInformation> rfis = this.repository.FindAll();

            int expectedCount = rfis.Count - 1;

            // Remove the RFI from the Repository
            this.repository.Remove(rfis[0]);

            // Commit the transaction
            this.unitOfWork.Commit();

            // Verify that there is now one less RFI in the data store
            IList<RequestForInformation> refreshedRfis = this.repository.FindAll();
            Assert.AreEqual(expectedCount, refreshedRfis.Count);
        }
    }
}