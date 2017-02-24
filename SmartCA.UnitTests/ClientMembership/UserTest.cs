using SmartCA.Model.Membership;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace SmartCA.UnitTests
{
    
    
    /// <summary>
    ///This is a test class for UserTest and is intended
    ///to contain all UserTest Unit Tests
    ///</summary>
    [TestClass()]
    public class UserTest
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
        ///A test for ValidatePassword
        ///</summary>
        [TestMethod()]
        public void ValidatePasswordTest()
        {
            string password = "Password!23";
            User.ValidatePassword(password);
        }

        /// <summary>
        ///A test for ValidatePassword - password empty
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void ValidatePasswordEmptyPasswordTest()
        {
            string password = string.Empty;
            User.ValidatePassword(password);
        }

        /// <summary>
        ///A test for ValidatePassword - password null
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ValidatePasswordNullPasswordTest()
        {
            string password = null;
            User.ValidatePassword(password);
        }

        /// <summary>
        ///A test for ValidatePassword - MinRequiredNonAlphanumericCharacters
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void ValidatePasswordMinRequiredNonAlphanumericCharactersTest()
        {
            string password = "Password";
            User.ValidatePassword(password);
        }

        /// <summary>
        ///A test for ValidatePassword - MaxPasswordSize
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void ValidatePasswordMaxPasswordSizeTest()
        {
            string password = "Password!23dddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddd";
            User.ValidatePassword(password);
        }

        /// <summary>
        ///A test for ToString
        ///</summary>
        [TestMethod()]
        public void ToStringTest()
        {
            User target = new User(Guid.NewGuid(), "timm", 
                "timm@interknowlogy.com", "What color is the sky?", 
                false, DateTime.Now, DateTime.MinValue,
                DateTime.Now, DateTime.Now, DateTime.MinValue);
            string expected = "timm";
            string actual = target.ToString();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ResetPassword
        ///</summary>
        [TestMethod()]
        public void ResetPasswordTest1()
        {
            User target = ClientMembershipService.GetUser("timm");
            string passwordAnswer = "Blue";
            string actual = target.ResetPassword(passwordAnswer);
            Assert.AreNotEqual("Password!23", actual);

            // Change it back so the other tests don't fail
            target.ChangePassword(actual, "Password!23");
        }

        /// <summary>
        ///A test for ResetPassword
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(NotSupportedException))]
        public void ResetPasswordTest()
        {
            User target = ClientMembershipService.GetUser("timm");
            string actual = target.ResetPassword();
            Assert.AreNotEqual("Password!23", actual);

            // Change it back so the other tests don't fail
            target.ChangePassword(actual, "Password!23");
        }

        /// <summary>
        ///A test for GetPassword
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(NotSupportedException))]
        public void GetPasswordTest1()
        {
            User target = ClientMembershipService.GetUser("timm");
            string expected = "Password!23";
            string actual = target.GetPassword();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetPassword
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(NotSupportedException))]
        public void GetPasswordTest()
        {
            User target = ClientMembershipService.GetUser("timm");
            string passwordAnswer = "Blue";
            string actual = target.GetPassword(passwordAnswer);
            Assert.AreNotEqual("Password!23", actual);
        }

        /// <summary>
        ///A test for CheckPasswordRetrieval
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(NotSupportedException))]
        public void CheckPasswordRetrievalTest()
        {
            User.CheckPasswordRetrieval();
        }

        /// <summary>
        ///A test for CheckPasswordReset
        ///</summary>
        [TestMethod()]
        public void CheckPasswordResetTest()
        {
            string passwordAnswer = "Blue";
            User.CheckPasswordReset(passwordAnswer);
        }

        /// <summary>
        ///A test for ChangePasswordQuestionAndAnswer
        ///</summary>
        [TestMethod()]
        public void ChangePasswordQuestionAndAnswerTest()
        {
            User target = ClientMembershipService.GetUser("timm");
            string password = "Password!23";
            string newPasswordQuestion = "What color is the sky now?";
            string newPasswordAnswer = "Grey";
            target.ChangePasswordQuestionAndAnswer(password, 
                newPasswordQuestion, newPasswordAnswer);

            // Change it back so the other tests don't fail
            target.ChangePasswordQuestionAndAnswer(password, 
                "What color is the sky?", "Blue");
        }

        /// <summary>
        ///A test for ChangePassword
        ///</summary>
        [TestMethod()]
        public void ChangePasswordTest()
        {
            User target = ClientMembershipService.GetUser("timm");
            string oldPassword = "Password!23";
            string newPassword = "Password!24";
            target.ChangePassword(oldPassword, newPassword);
            ClientMembershipService.ValidateUser(target.UserName, newPassword);

            // Change it back so the other tests don't fail
            target.ChangePassword(newPassword, oldPassword);
        }
    }
}
