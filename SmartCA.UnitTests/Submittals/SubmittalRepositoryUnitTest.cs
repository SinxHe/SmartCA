using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartCA.Infrastructure;
using SmartCA.Infrastructure.RepositoryFramework;
using SmartCA.Model;
using SmartCA.Model.Employees;
using SmartCA.Model.Projects;
using SmartCA.Model.Submittals;

namespace SmartCA.UnitTests.Submittals
{
    /// <summary>
    /// Summary description for SubmittalRepositoryUnitTest
    /// </summary>
    [TestClass]
    public class SubmittalRepositoryUnitTest
    {
        private TestContext testContextInstance;
        private IUnitOfWork unitOfWork;
        private ISubmittalRepository repository;

        public SubmittalRepositoryUnitTest()
        {
            this.unitOfWork = new UnitOfWork();
            this.repository = RepositoryFactory.GetRepository<ISubmittalRepository,
                Submittal>(this.unitOfWork);
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

        [DeploymentItem("SmartCA.sdf"), TestMethod()]
        public void FindSubmittalByKeyTest()
        {
            Submittal submittal = this.repository.FindBy("a5d32de3-25a6-465f-b9ea-f49d774ac920");
            Assert.AreEqual("03300.01.00", submittal.Number);
        }

        /// <summary>
        /// A test for FindBy(Project project)
        /// </summary>
        [DeploymentItem("SmartCA.sdf"), TestMethod()]
        public void FindSubmittalsByProjectTest()
        {
            // Get a Project reference
            Project project = ProjectService.GetAllProjects()[0];

            // FIns all of the Submittals for the Project
            IList<Submittal> submittals = this.repository.FindBy(project);

            // Verify that at least one Submittal was returned
            Assert.IsTrue(submittals.Count > 0);
        }

        /// <summary>
        /// A test for FindAllSpecificationSections
        /// </summary>
        [DeploymentItem("SmartCA.sdf"), TestMethod()]
        public void FindAllSpecificationSectionsTest()
        {
            // Get the list of all Specifications
            IList<SpecificationSection> specSections =
                this.repository.FindAllSpecificationSections();

            // Verify the count
            Assert.AreEqual(2238, specSections.Count);
        }

        [DeploymentItem("SmartCA.sdf"), TestMethod()]
        public void FindAllSubmittalStatusesTest()
        {
            IList<ItemStatus> statuses = this.repository.FindAllItemStatuses();
            Assert.AreEqual(34, statuses.Count);
        }

        [DeploymentItem("SmartCA.sdf"), TestMethod()]
        public void FindAllDisciplinesTest()
        {
            IList<Discipline> disciplines = this.repository.FindAllDisciplines();
            Assert.AreEqual(30, disciplines.Count);
        }

        /// <summary>
        ///A test for Add(Submittal item)
        ///</summary>
        [DeploymentItem("SmartCA.sdf"), TestMethod()]
        public void AddSubmittalTest()
        {
            // Create a new Submittal
            IList<SpecificationSection> specSections =
                this.repository.FindAllSpecificationSections();
            Project project = ProjectService.GetAllProjects()[0];
            Guid projectKey = new Guid("5704f6b9-6ffa-444c-9583-35cc340fce2a");
            Submittal submittal = new Submittal(specSections[0], project.Key);
            submittal.To = project.Contacts[0];
            submittal.From = EmployeeService.GetEmployees()[0];
            IList<ItemStatus> statuses =
                this.repository.FindAllItemStatuses();
            submittal.Status = statuses[0];

            // Add the Submittal to the Repository
            repository.Add(submittal);

            // Commit the transaction
            this.unitOfWork.Commit();

            // Reload the Submittal and verify it's number
            Submittal savedSubmittal = this.repository.FindBy(submittal.Key);
            Assert.AreEqual("00 11 13.01.00", savedSubmittal.Number);
        }

        /// <summary>
        ///A test for Updating a Submittal
        ///</summary>
        [DeploymentItem("SmartCA.sdf"), TestMethod()]
        public void UpdateSubmittalTest()
        {
            IList<Submittal> submittals = this.repository.FindAll();

            // Change the Submittal's DateReceived value
            DateTime dateReceived = DateTime.Now;
            submittals[0].DateReceived = dateReceived;

            // Update the Repository
            this.repository[submittals[0].Key] = submittals[0];

            // Commit the transaction
            this.unitOfWork.Commit();

            // Verify that the change was saved
            IList<Submittal> refreshedSubmittals = this.repository.FindAll();
            Assert.AreEqual(dateReceived.Date,
                refreshedSubmittals[0].DateReceived.Value.Date);
        }

        /// <summary>
        ///A test for Remove(Submittal item)
        ///</summary>
        [DeploymentItem("SmartCA.sdf"), TestMethod()]
        public void RemoveSubmittalTest()
        {
            IList<Submittal> submittals = this.repository.FindAll();

            int oldCount = submittals.Count;

            // Remove the Submittal from the Repository
            this.repository.Remove(submittals[0]);

            // Commit the transaction
            this.unitOfWork.Commit();

            // Verify that there is now one less Submittal in the data store
            IList<Submittal> refreshedSubmittals = this.repository.FindAll();
            Assert.AreEqual(oldCount - 1, refreshedSubmittals.Count);

            // Reset the state
            this.AddSubmittalTest();
        }
    }
}