using SmartCA.Model.ConstructionChangeDirectives;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartCA.Model.ChangeOrders;
using System;

namespace SmartCA.UnitTests
{
    
    
    /// <summary>
    ///This is a test class for ConstructionChangeDirectiveTest and is intended
    ///to contain all ConstructionChangeDirectiveTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ConstructionChangeDirectiveTest
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
        ///A test for TransformToChangeOrderTest
        ///</summary>
        [TestMethod()]
        [DeploymentItem("SmartCA.sdf")]
        public void TransformToChangeOrderTest()
        {
            ConstructionChangeDirective ccd = ConstructionChangeDirectiveService.GetConstructionChangeDirective(new Guid("1a2bfe3b-7577-456d-8484-4f34f67ce42c"));
            ChangeOrder co = ccd.TransformToChangeOrder();
            Assert.AreEqual(6, co.Number);
        }
    }
}
