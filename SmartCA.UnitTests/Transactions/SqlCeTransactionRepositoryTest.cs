using SmartCA.Infrastructure.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartCA.Infrastructure.Transactions;
using SmartCA.Infrastructure.DomainBase;
using System;
using SmartCA.Model.Companies;
using System.Collections.Generic;

namespace SmartCA.UnitTests
{
    
    
    /// <summary>
    ///This is a test class for SqlCeTransactionRepositoryTest and is intended
    ///to contain all SqlCeTransactionRepositoryTest Unit Tests
    ///</summary>
    [TestClass()]
    public class SqlCeTransactionRepositoryTest
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
        ///A test for adding a Transaction
        ///</summary>
        [DeploymentItem("SmartCA.sdf"), TestMethod()]
        public void SqlCeClientTransactionRepositoryAddTest()
        {
            SqlCeClientTransactionRepository target = new SqlCeClientTransactionRepository();
            TransactionType type = TransactionType.Insert;
            Company entity = new Company();
            entity.Name = "Test 123";
            object unitOfWorkKey = Guid.NewGuid();
            target.Add(new ClientTransaction(unitOfWorkKey, type, entity));
        }

        /// <summary>
        /// A test for finding all of the pending transactions
        /// </summary>
        [DeploymentItem("SmartCA.sdf"), TestMethod()]
        public void FindPendingTest()
        {
            // Make sure there is at least one pending transaction 
            this.SqlCeClientTransactionRepositoryAddTest();

            // Get the pending transactions
            SqlCeClientTransactionRepository target = new SqlCeClientTransactionRepository();
            IList<ClientTransaction> transactions = target.FindPending();
            Assert.IsTrue(transactions.Count > 0);
        }

        /// <summary>
        ///A test for SetLastSynchronization
        ///</summary>
        [DeploymentItem("SmartCA.sdf"), TestMethod()]
        public void SetLastSynchronizationTest()
        {
            SqlCeClientTransactionRepository target = new SqlCeClientTransactionRepository();
            target.SetLastSynchronization(DateTime.Now);
        }

        /// <summary>
        ///A test for GetLastSynchronization
        ///</summary>
        [DeploymentItem("SmartCA.sdf"), TestMethod()]
        public void GetLastSynchronizationTest()
        {
            SqlCeClientTransactionRepository target = new SqlCeClientTransactionRepository();
            target.SetLastSynchronization(DateTime.Now);
            DateTime? lastSynchronization = target.GetLastSynchronization();
            Assert.IsTrue(lastSynchronization.HasValue);
            Assert.IsTrue(DateTime.Now > lastSynchronization.Value);
        }
    }
}
