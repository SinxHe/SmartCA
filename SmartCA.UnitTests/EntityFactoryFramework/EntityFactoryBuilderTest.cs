using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartCA.Infrastructure.DomainBase;
using SmartCA.Model.Projects;
using SmartCA.Infrastructure;
using SmartCA.Infrastructure.EntityFactoryFramework;

namespace SmartCA.UnitTests
{
    /// <summary>
    ///This is a test class for RepositoryFactoryTest and is intended
    ///to contain all RepositoryFactoryTest Unit Tests
    ///</summary>
    [TestClass()]
    public class EntityFactoryBuilderTest
    {
        private TestContext testContextInstance;

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

        [TestMethod()]
        public void BuildFactoryTest()
        {
            IEntityFactory<Project> entityFactory = EntityFactoryBuilder.BuildFactory<Project>();
            Assert.AreNotEqual(null, entityFactory);
            Assert.AreEqual("ProjectFactory", entityFactory.GetType().Name);
            this.testContextInstance.WriteLine("Created an IEntityFactory<Project> of type {0}", entityFactory.GetType().FullName);
        }
    }
}
