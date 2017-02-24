using SmartCA.Model.Membership;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace SmartCA.UnitTests
{
    
    
    /// <summary>
    ///This is a test class for ClientMembershipUserTest and is intended
    ///to contain all ClientMembershipUserTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ClientMembershipUserTest
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
        ///A test for PasswordAttemptSucceeded
        ///</summary>
        [TestMethod()]
        public void PasswordAttemptSucceededTest()
        {
            User user = ClientMembershipService.GetUser("timm");
            string passwordSalt = "1U3h6r/tQ+dGWhLm9Unyng==";
            PasswordFormat passwordFormat = PasswordFormat.Hashed;
            int failedPasswordAttemptCount = 0;
            DateTime failedPasswordAttemptWindowStart = DateTime.MinValue;
            int failedPasswordAnswerAttemptCount = 0;
            DateTime failedPasswordAnswerAttemptWindowStart = DateTime.MinValue;

            ClientMembershipUser target = new ClientMembershipUser(user,
                passwordSalt, passwordFormat, failedPasswordAttemptCount,
                failedPasswordAttemptWindowStart, failedPasswordAnswerAttemptCount,
                failedPasswordAnswerAttemptWindowStart);

            target.PasswordAttemptSucceeded();

            Assert.AreEqual(DateTime.MinValue, target.FailedPasswordAttemptWindowStart);
            Assert.AreEqual(0, target.FailedPasswordAttemptCount);
            Assert.AreEqual(false, target.IsLockedOut);
            Assert.AreEqual(DateTime.MinValue, target.LastLockoutDate);
        }

        /// <summary>
        ///A test for PasswordAttemptFailed
        ///</summary>
        [TestMethod()]
        public void PasswordAttemptFailedTest()
        {
            User user = ClientMembershipService.GetUser("timm");
            string passwordSalt = "1U3h6r/tQ+dGWhLm9Unyng==";
            PasswordFormat passwordFormat = PasswordFormat.Hashed;
            int failedPasswordAttemptCount = 0;
            DateTime failedPasswordAttemptWindowStart = DateTime.MinValue;
            int failedPasswordAnswerAttemptCount = 0;
            DateTime failedPasswordAnswerAttemptWindowStart = DateTime.MinValue;

            ClientMembershipUser target = new ClientMembershipUser(user,
                passwordSalt, passwordFormat, failedPasswordAttemptCount,
                failedPasswordAttemptWindowStart, failedPasswordAnswerAttemptCount,
                failedPasswordAnswerAttemptWindowStart);

            target.PasswordAttemptSucceeded();
            target.PasswordAttemptFailed();

            Assert.AreNotEqual(DateTime.MinValue, target.FailedPasswordAttemptWindowStart);
            Assert.AreEqual(1, target.FailedPasswordAttemptCount);
            Assert.AreEqual(false, target.IsLockedOut);
            
            target.PasswordAttemptFailed();
            target.PasswordAttemptFailed();
            target.PasswordAttemptFailed();
            target.PasswordAttemptFailed();

            Assert.AreEqual(true, target.IsLockedOut);
            Assert.AreNotEqual(DateTime.MinValue, target.LastLockoutDate);
        }

        /// <summary>
        ///A test for PasswordAnswerAttemptSucceeded
        ///</summary>
        [TestMethod()]
        public void PasswordAnswerAttemptSucceededTest()
        {
            User user = ClientMembershipService.GetUser("timm");
            string passwordSalt = "1U3h6r/tQ+dGWhLm9Unyng==";
            PasswordFormat passwordFormat = PasswordFormat.Hashed;
            int failedPasswordAttemptCount = 0;
            DateTime failedPasswordAttemptWindowStart = DateTime.MinValue;
            int failedPasswordAnswerAttemptCount = 0;
            DateTime failedPasswordAnswerAttemptWindowStart = DateTime.MinValue;

            ClientMembershipUser target = new ClientMembershipUser(user,
                passwordSalt, passwordFormat, failedPasswordAttemptCount,
                failedPasswordAttemptWindowStart, failedPasswordAnswerAttemptCount,
                failedPasswordAnswerAttemptWindowStart);

            target.PasswordAnswerAttemptSucceeded();

            Assert.AreEqual(DateTime.MinValue, target.FailedPasswordAnswerAttemptWindowStart);
            Assert.AreEqual(0, target.FailedPasswordAnswerAttemptCount);
            Assert.AreEqual(false, target.IsLockedOut);
            Assert.AreEqual(DateTime.MinValue, target.LastLockoutDate);
        }

        /// <summary>
        ///A test for PasswordAnswerAttemptFailed
        ///</summary>
        [TestMethod()]
        public void PasswordAnswerAttemptFailedTest()
        {
            User user = ClientMembershipService.GetUser("timm");
            string passwordSalt = "1U3h6r/tQ+dGWhLm9Unyng==";
            PasswordFormat passwordFormat = PasswordFormat.Hashed;
            int failedPasswordAttemptCount = 0;
            DateTime failedPasswordAttemptWindowStart = DateTime.MinValue;
            int failedPasswordAnswerAttemptCount = 0;
            DateTime failedPasswordAnswerAttemptWindowStart = DateTime.MinValue;

            ClientMembershipUser target = new ClientMembershipUser(user,
                passwordSalt, passwordFormat, failedPasswordAttemptCount,
                failedPasswordAttemptWindowStart, failedPasswordAnswerAttemptCount,
                failedPasswordAnswerAttemptWindowStart);

            target.PasswordAnswerAttemptSucceeded();
            target.PasswordAnswerAttemptFailed();

            Assert.AreNotEqual(DateTime.MinValue, target.FailedPasswordAnswerAttemptWindowStart);
            Assert.AreEqual(1, target.FailedPasswordAnswerAttemptCount);
            Assert.AreEqual(false, target.IsLockedOut);

            target.PasswordAnswerAttemptFailed();
            target.PasswordAnswerAttemptFailed();
            target.PasswordAnswerAttemptFailed();
            target.PasswordAnswerAttemptFailed();

            Assert.AreEqual(true, target.IsLockedOut);
            Assert.AreNotEqual(DateTime.MinValue, target.LastLockoutDate);
        }
    }
}
