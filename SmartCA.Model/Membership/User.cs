using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using SmartCA.Infrastructure;
using SmartCA.Infrastructure.DomainBase;

namespace SmartCA.Model.Membership
{
    public class User : IEntity
    {
        #region Private Fields

        private Guid userKey;
        private DateTime creationDate;
        private string email;
        private bool isLockedOut;
        private DateTime lastActivityDate;
        private DateTime lastLockoutDate;
        private DateTime lastLoginDate;
        private DateTime lastPasswordChangedDate;
        private string passwordQuestion;
        private string userName;
        private List<Role> roles;
        
        #endregion

        #region Constructors

        public User(Guid userKey, string name, string email, 
            string passwordQuestion, bool isLockedOut, 
            DateTime creationDate, DateTime lastLoginDate, 
            DateTime lastActivityDate, DateTime lastPasswordChangedDate, 
            DateTime lastLockoutDate)
        {
            this.userKey = userKey;
            if (name != null)
            {
                name = name.Trim();
            }
            if (email != null)
            {
                email = email.Trim();
            }
            if (passwordQuestion != null)
            {
                passwordQuestion = passwordQuestion.Trim();
            }
            this.userName = name;
            this.email = email;
            this.passwordQuestion = passwordQuestion;
            this.isLockedOut = isLockedOut;
            this.creationDate = creationDate.ToUniversalTime();
            this.lastLoginDate = lastLoginDate.ToUniversalTime();
            this.lastActivityDate = lastActivityDate.ToUniversalTime();
            this.lastPasswordChangedDate 
                = lastPasswordChangedDate.ToUniversalTime();
            this.lastLockoutDate = lastLockoutDate.ToUniversalTime();
            this.roles = new List<Role>();
            this.ValidateInitialization();
        }

        #endregion

        #region Methods

        public void ChangePassword(string oldPassword, string newPassword)
        {
            SecurityHelper.CheckPasswordParameter(oldPassword, 0, "oldPassword");
            SecurityHelper.CheckPasswordParameter(newPassword, 0, "newPassword");
            User.ValidatePassword(newPassword);
            ClientMembershipService.ChangePassword(this.UserName, 
                oldPassword, newPassword);
        }

        public void ChangePasswordQuestionAndAnswer(string password, 
            string newPasswordQuestion, string newPasswordAnswer)
        {
            SecurityHelper.CheckPasswordParameter(password, 0, "password");
            SecurityHelper.CheckParameter(newPasswordQuestion, false, true, 
                false, 0, "newPasswordQuestion");
            SecurityHelper.CheckParameter(newPasswordAnswer, false, true, 
                false, 0, "newPasswordAnswer");
            ClientMembershipService.ChangePasswordQuestionAndAnswer(this.UserName, 
                password, newPasswordQuestion, newPasswordAnswer);
            this.passwordQuestion = newPasswordQuestion;
        }

        public string GetPassword()
        {
            User.CheckPasswordRetrieval();
            return ClientMembershipService.GetPassword(this.UserName);
        }

        public string GetPassword(string passwordAnswer)
        {
            User.CheckPasswordRetrieval();
            return ClientMembershipService.GetPassword(this.UserName, passwordAnswer);
        }

        public string ResetPassword()
        {
            User.CheckPasswordReset(null);
            return ClientMembershipService.ResetPassword(this.UserName);
        }

        public string ResetPassword(string passwordAnswer)
        {
            User.CheckPasswordReset(passwordAnswer);
            return ClientMembershipService.ResetPassword(
                this.UserName, passwordAnswer);
        }

        public override string ToString()
        {
            return this.UserName;
        }

        protected virtual void ValidateInitialization()
        {
            SecurityHelper.CheckParameter(this.userName, true, true, true,
                ClientMembershipService.Application.MaxUsernameSize, "username");
            SecurityHelper.CheckParameter(this.email, true, true, true,
                0, "email");
            SecurityHelper.CheckParameter(this.passwordQuestion, true, true, true,
                ClientMembershipService.Application.MaxPasswordQuestionSize, 
                "passwordQuestion");
        }

        public static void ValidatePassword(string password)
        {
            SecurityHelper.CheckParameter(password, true, false, false,
                0, "password");

            Application application = ClientMembershipService.Application;

            if (password.Length < application.MinRequiredPasswordLength)
            {
                throw new ArgumentException("Password too short",
                              "password");
            }

            int count = 0;

            for (int i = 0; i < password.Length; i++)
            {
                if (!char.IsLetterOrDigit(password, i))
                {
                    count++;
                }
            }

            if (count < application.MinRequiredNonAlphanumericCharacters)
            {
                throw new ArgumentException
                    ("Password needs more non alphanumeric chars",
                    "password");
            }

            if (application.PasswordStrengthRegularExpression.Length > 0)
            {
                if (!Regex.IsMatch(password, 
                    application.PasswordStrengthRegularExpression))
                {
                    throw new ArgumentException
                        ("Password does not match regular expression",
                        "password");
                }
            }

            if (password.Length > application.MaxPasswordSize)
            {
                throw new ArgumentException("Password too long", 
                    "password");
            }
        }

        public static void CheckPasswordRetrieval()
        {
            if (!ClientMembershipService.Application.EnablePasswordRetrieval)
            {
                throw new NotSupportedException("Membership password retrieval not supported");
            }
        }

        public static void CheckPasswordReset(string passwordAnswer)
        {
            Application application = ClientMembershipService.Application;

            if (!application.EnablePasswordReset)
            {
                throw new NotSupportedException
                    ("Not configured to support password resets");
            }

            if (string.IsNullOrEmpty(passwordAnswer))
            {
                if (application.RequiresQuestionAndAnswer)
                {
                    throw new NotSupportedException
                        ("Must reset password with a password answer");
                }
            }
        }

        #endregion

        #region Properties

        public Guid UserKey
        {
            get { return this.userKey; }
        }

        public DateTime CreationDate
        {
            get { return this.creationDate.ToLocalTime(); }
        }

        public string Email
        {
            get { return this.email; }
            set { this.email = value; }
        }

        public bool IsLockedOut
        {
            get { return this.isLockedOut; }
            protected set { this.isLockedOut = value; }
        }

        public DateTime LastActivityDate
        {
            get { return this.lastActivityDate.ToLocalTime(); }
            set { this.lastActivityDate = value.ToUniversalTime(); }
        }

        public DateTime LastLockoutDate
        {
            get { return this.lastLockoutDate.ToLocalTime(); }
            protected set { this.lastLockoutDate = value.ToUniversalTime(); }
        }

        public DateTime LastLoginDate
        {
            get { return this.lastLoginDate.ToLocalTime(); }
            set { this.lastLoginDate = value.ToUniversalTime(); }
        }

        public DateTime LastPasswordChangedDate
        {
            get { return this.lastPasswordChangedDate.ToLocalTime(); }
        }

        public string PasswordQuestion
        {
            get { return this.passwordQuestion; }
        }

        public string UserName
        {
            get { return this.userName; }
        }

        public IList<Role> Roles
        {
            get { return this.roles; }
        }

        #endregion

        #region IEntity Members

        public object Key
        {
            get { return this.userKey; }
        }

        #endregion
    }
}
