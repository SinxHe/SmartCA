using SmartCA.Model.Membership;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
namespace SmartCA.UnitTests
{
    
    
    /// <summary>
    ///This is a test class for ClientMembershipServiceTest and is intended
    ///to contain all ClientMembershipServiceTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ClientMembershipServiceTest
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
        ///A test for Application
        ///</summary>
        [TestMethod()]
        public void ApplicationTest()
        {
            Application actual = ClientMembershipService.Application;
            Assert.AreEqual(true, actual.EnablePasswordReset);
            Assert.AreEqual(false, actual.EnablePasswordRetrieval);
            Assert.AreEqual("SHA1", actual.HashAlgorithmType);
        }

        /// <summary>
        ///A test for logging in
        ///</summary>
        [TestMethod()]
        public void ValidateUserTest()
        {
            string username = "timm";
            string password = "Password!23";
            bool expected = true;
            bool actual = ClientMembershipService.ValidateUser(username, password);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for UpdateUser
        ///</summary>
        [TestMethod()]
        public void UpdateUserTest()
        {
            User user = ClientMembershipService.GetUser("timm");
            user.Email = "tmccarthy@interknowlogy.com";
            ClientMembershipService.UpdateUser(user);

            // Change it back so the other tests pass
            user.Email = "timm@interknowlogy.com";
            ClientMembershipService.UpdateUser(user);
        }

        /// <summary>
        ///A test for ResetPassword
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(NotSupportedException))]
        public void ResetPasswordTest1()
        {
            string username = "timm";
            string actual = ClientMembershipService.ResetPassword(username);
            this.testContextInstance.WriteLine("New Password:  {0}", actual);
            Assert.AreNotEqual("Password!23", actual);

            // Change it back so the other tests pass
            ClientMembershipService.ChangePassword(username, actual, "Password!23");
        }

        /// <summary>
        ///A test for ResetPassword
        ///</summary>
        [TestMethod()]
        public void ResetPasswordTest()
        {
            string username = "timm";
            string passwordAnswer = "Blue";
            string actual = ClientMembershipService.ResetPassword(username, passwordAnswer);
            this.testContextInstance.WriteLine("New Password:  {0}", actual);
            Assert.AreNotEqual("Password!23", actual);

            // Change it back so the other tests pass
            ClientMembershipService.ChangePassword(username, actual, "Password!23");
        }

        /// <summary>
        ///A test for GetUser
        ///</summary>
        [TestMethod()]
        public void GetUserTest1()
        {
            string username = "timm";
            User actual = ClientMembershipService.GetUser(username);
            Assert.AreEqual("timm@interknowlogy.com", actual.Email);
        }

        /// <summary>
        ///A test for GetUser
        ///</summary>
        [TestMethod()]
        public void GetUserTest()
        {
            object userKey = new Guid("52bafb83-98f2-455c-9290-c7c31019f150");
            User actual = ClientMembershipService.GetUser(userKey);
            Assert.AreEqual("timm", actual.UserName);
        }

        /// <summary>
        ///A test for GetPassword
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(NotSupportedException))]
        public void GetPasswordTest1()
        {
            string username = "timm";
            string passwordAnswer = "Blue";
            string expected = "Password!23";
            string actual = ClientMembershipService.GetPassword(username, passwordAnswer);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetPassword
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(NotSupportedException))]
        public void GetPasswordTest()
        {
            string username = "timm";
            string expected = "Password!23";
            string actual = ClientMembershipService.GetPassword(username);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ChangePasswordQuestionAndAnswer
        ///</summary>
        [TestMethod()]
        public void ChangePasswordQuestionAndAnswerTest()
        {
            string username = "timm";
            string password = "Password!23";
            string newPasswordQuestion = "What color is the sky now?";
            string newPasswordAnswer = "Grey";
            bool expected = true;
            bool actual = ClientMembershipService.ChangePasswordQuestionAndAnswer(
                username, password, newPasswordQuestion, newPasswordAnswer);
            Assert.AreEqual(expected, actual);

            // Change it back so the other tests don't fail
            ClientMembershipService.ChangePasswordQuestionAndAnswer(
                username, password, "What color is the sky?", "Blue");
        }

        /// <summary>
        ///A test for ChangePassword
        ///</summary>
        [TestMethod()]
        public void ChangePasswordTest()
        {
            string username = "timm";
            string oldPassword = "Password!23";
            string newPassword = "Password!24";
            bool expected = true;
            bool actual = ClientMembershipService.ChangePassword(username, 
                oldPassword, newPassword);
            Assert.AreEqual(expected, actual);

            // Change it back so the other tests don't fail
            ClientMembershipService.ChangePassword(username,
                newPassword, oldPassword);
        }
    }
}
