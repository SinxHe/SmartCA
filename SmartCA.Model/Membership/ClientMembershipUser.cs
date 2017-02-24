using System;
using SmartCA.Model.Membership;

namespace SmartCA.Model.Membership
{
    public class ClientMembershipUser : User
    {
        #region Private Fields

        private string passwordSalt;
        private PasswordFormat passwordFormat;
        private int failedPasswordAttemptCount;
        private DateTime failedPasswordAttemptWindowStart;
        private int failedPasswordAnswerAttemptCount;
        private DateTime failedPasswordAnswerAttemptWindowStart;

        #endregion

        #region Constructor

        public ClientMembershipUser(User user, string passwordSalt, 
            PasswordFormat passwordFormat, int failedPasswordAttemptCount,
            DateTime failedPasswordAttemptWindowStart,
            int failedPasswordAnswerAttemptCount,
            DateTime failedPasswordAnswerAttemptWindowStart) 
            : base(user.UserKey, 
            user.UserName, user.Email, user.PasswordQuestion, 
            user.IsLockedOut, user.CreationDate, user.LastLoginDate, 
            user.LastActivityDate, user.LastPasswordChangedDate, 
            user.LastLockoutDate)
        {
            if (passwordSalt != null)
            {
                passwordSalt = passwordSalt.Trim();
            }
            this.passwordSalt = passwordSalt;
            this.passwordFormat = passwordFormat;
            this.failedPasswordAttemptCount = failedPasswordAttemptCount;
            this.failedPasswordAttemptWindowStart 
                = failedPasswordAttemptWindowStart;
            this.failedPasswordAnswerAttemptCount 
                = failedPasswordAnswerAttemptCount;
            this.failedPasswordAnswerAttemptWindowStart 
                = failedPasswordAnswerAttemptWindowStart;
        }

        #endregion

        #region Properties

        public string PasswordSalt
        {
            get { return this.passwordSalt; }
        }

        public PasswordFormat PasswordFormat
        {
            get { return this.passwordFormat; }
        }

        public int FailedPasswordAttemptCount
        {
            get { return this.failedPasswordAttemptCount; }
        }

        public DateTime FailedPasswordAttemptWindowStart
        {
            get { return this.failedPasswordAttemptWindowStart; }
        }

        public int FailedPasswordAnswerAttemptCount
        {
            get { return this.failedPasswordAnswerAttemptCount; }
        }

        public DateTime FailedPasswordAnswerAttemptWindowStart
        {
            get { return this.failedPasswordAnswerAttemptWindowStart; }
        }

        #endregion

        #region Methods

        public void PasswordAttemptFailed()
        {
            DateTime currentDateTime = DateTime.Now.ToUniversalTime();

            if (currentDateTime >
                this.failedPasswordAttemptWindowStart.AddMinutes(
                ClientMembershipService.Application.PasswordAttemptWindow))
            {
                this.failedPasswordAttemptWindowStart = currentDateTime;
                this.failedPasswordAttemptCount = 1;
            }
            else
            {
                this.failedPasswordAttemptCount++;
            }

            if (this.failedPasswordAttemptCount > 
                ClientMembershipService.Application.MaxInvalidPasswordAttempts)
            {
                this.IsLockedOut = true;
                this.LastLockoutDate = currentDateTime;
            }

            this.LastActivityDate = currentDateTime;
        }

        public void PasswordAttemptSucceeded()
        {
            DateTime currentDateTime = DateTime.Now.ToUniversalTime();
            this.failedPasswordAttemptWindowStart = DateTime.MinValue;
            this.failedPasswordAttemptCount = 0;
            this.IsLockedOut = false;
            this.LastLockoutDate = DateTime.MinValue;
            this.LastActivityDate = currentDateTime;
        }

        public void PasswordAnswerAttemptFailed()
        {
            DateTime currentDateTime = DateTime.Now.ToUniversalTime();

            if (currentDateTime >
                this.failedPasswordAnswerAttemptWindowStart.AddMinutes(
                ClientMembershipService.Application.PasswordAttemptWindow))
            {
                this.failedPasswordAnswerAttemptWindowStart = currentDateTime;
                this.failedPasswordAnswerAttemptCount = 1;
            }
            else
            {
                this.failedPasswordAnswerAttemptCount++;
            }

            if (this.failedPasswordAnswerAttemptCount >
                ClientMembershipService.Application.MaxInvalidPasswordAttempts)
            {
                this.IsLockedOut = true;
                this.LastLockoutDate = currentDateTime;
            }

            this.LastActivityDate = currentDateTime;
        }

        public void PasswordAnswerAttemptSucceeded()
        {
            DateTime currentDateTime = DateTime.Now.ToUniversalTime();
            this.failedPasswordAnswerAttemptWindowStart = DateTime.MinValue;
            this.failedPasswordAnswerAttemptCount = 0;
            this.IsLockedOut = false;
            this.LastLockoutDate = DateTime.MinValue;
            this.LastActivityDate = currentDateTime;
        }

        #endregion
    }
}
