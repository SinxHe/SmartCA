using SmartCA.Model.ChangeOrders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace SmartCA.UnitTests
{
    
    
    /// <summary>
    ///This is a test class for ChangeOrderTest and is intended
    ///to contain all ChangeOrderTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ChangeOrderTest
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
        ///A test for PreviousTimeChangedTotalTest
        ///</summary>
        [DeploymentItem("SmartCA.sdf"), TestMethod()]
        public void PreviousTimeChangedTotalTest()
        {
            object projectKey = new Guid("5704f6b9-6ffa-444c-9583-35cc340fce2a");
            int number = 5;
            ChangeOrder target = new ChangeOrder(projectKey, number);
            int actual = target.PreviousTimeChangedTotal;
            Assert.AreEqual(4, actual);
        }

        /// <summary>
        ///A test for PreviousAuthorizedChangeOrderAmountTest
        ///</summary>
        [DeploymentItem("SmartCA.sdf"), TestMethod()]
        public void PreviousAuthorizedChangeOrderAmountTest()
        {
            object projectKey = new Guid("5704f6b9-6ffa-444c-9583-35cc340fce2a");
            int number = 5;
            ChangeOrder target = new ChangeOrder(projectKey, number);
            decimal actual = target.PreviousAuthorizedAmount;
            Assert.AreEqual(4000M, actual);
        }

        /// <summary>
        ///A test for OriginalConstructionCostTest
        ///</summary>
        [DeploymentItem("SmartCA.sdf"), TestMethod()]
        public void OriginalConstructionCostTest()
        {
            object projectKey = new Guid("5704f6b9-6ffa-444c-9583-35cc340fce2a");
            int number = 5;
            ChangeOrder target = new ChangeOrder(projectKey, number);
            decimal actual = target.OriginalConstructionCost;
            Assert.AreEqual(100000M, actual);
        }

        /// <summary>
        ///A test for NewConstructionCostTest
        ///</summary>
        [DeploymentItem("SmartCA.sdf"), TestMethod()]
        public void NewConstructionCostTest()
        {
            object projectKey = new Guid("5704f6b9-6ffa-444c-9583-35cc340fce2a");
            int number = 5;
            ChangeOrder target = new ChangeOrder(projectKey, number);
            target.AmountChanged = 1000M;
            decimal actual = target.NewConstructionCost;
            Assert.AreEqual(105000M, actual);
        }

        /// <summary>
        ///A test for ChangeOrderConstructorTest1
        ///</summary>
        [DeploymentItem("SmartCA.sdf"), TestMethod()]
        public void ChangeOrderConstructorTestShort()
        {
            object projectKey = new Guid("5704f6b9-6ffa-444c-9583-35cc340fce2a");
            int number = 5;
            ChangeOrder target = new ChangeOrder(projectKey, number);
            // There should be 3 broken rules
            Assert.AreEqual(3, target.GetBrokenRules().Count);
        }

        /// <summary>
        ///A test for ChangeOrderConstructorTest
        ///</summary>
        [DeploymentItem("SmartCA.sdf"), TestMethod()]
        public void ChangeOrderConstructorTestFull()
        {
            object key = Guid.NewGuid();
            object projectKey = new Guid("5704f6b9-6ffa-444c-9583-35cc340fce2a");
            int number = 5;
            ChangeOrder target = new ChangeOrder(key, projectKey, number);
            // There should be 3 broken rules
            Assert.AreEqual(3, target.GetBrokenRules().Count);
        }
    }
}
