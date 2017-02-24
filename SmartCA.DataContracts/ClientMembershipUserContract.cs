using System;
using System.Collections.Generic;

namespace SmartCA.DataContracts
{
    [Serializable]
    public class ClientMembershipUserContract : ContractBase
    {
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
        private string passwordSalt;
        private PasswordFormatContract passwordFormat;
        private int failedPasswordAttemptCount;
        private DateTime failedPasswordAttemptWindowStart;
        private int failedPasswordAnswerAttemptCount;
        private DateTime failedPasswordAnswerAttemptWindowStart;
        private List<RoleContract> roles;

        public ClientMembershipUserContract()
        {
            this.userKey = Guid.Empty;
            this.creationDate = DateTime.MinValue;
            this.email = string.Empty;
            this.isLockedOut = false;
            this.lastActivityDate = DateTime.MinValue;
            this.lastLockoutDate = DateTime.MinValue;
            this.lastLoginDate = DateTime.MinValue;
            this.lastPasswordChangedDate = DateTime.MinValue;
            this.passwordQuestion = string.Empty;
            this.userName = string.Empty;
            this.passwordSalt = string.Empty;
            this.passwordFormat = PasswordFormatContract.Hashed;
            this.failedPasswordAttemptCount = 0;
            this.failedPasswordAttemptWindowStart = DateTime.MinValue;
            this.failedPasswordAnswerAttemptCount = 0;
            this.failedPasswordAnswerAttemptWindowStart = DateTime.MinValue;
            this.roles = new List<RoleContract>();
        }

        public Guid UserKey
        {
            get { return this.userKey; }
            set { this.userKey = value; }
        }        

        public DateTime CreationDate
        {
            get { return this.creationDate; }
            set { this.creationDate = value; }
        }        

        public string Email
        {
            get { return this.email; }
            set { this.email = value; }
        }        

        public bool IsLockedOut
        {
            get { return this.isLockedOut; }
            set { this.isLockedOut = value; }
        }        

        public DateTime LastActivityDate
        {
            get { return this.lastActivityDate; }
            set { this.lastActivityDate = value; }
        }        

        public DateTime LastLockoutDate
        {
            get { return this.lastLockoutDate; }
            set { this.lastLockoutDate = value; }
        }        

        public DateTime LastLoginDate
        {
            get { return this.lastLoginDate; }
            set { this.lastLoginDate = value; }
        }        

        public DateTime LastPasswordChangedDate
        {
            get { return this.lastPasswordChangedDate; }
            set { this.lastPasswordChangedDate = value; }
        }        

        public string PasswordQuestion
        {
            get { return this.passwordQuestion; }
            set { this.passwordQuestion = value; }
        }        

        public string UserName
        {
            get { return this.userName; }
            set { this.userName = value; }
        }        

        public string PasswordSalt
        {
            get { return this.passwordSalt; }
            set { this.passwordSalt = value; }
        }        

        public PasswordFormatContract PasswordFormat
        {
            get { return this.passwordFormat; }
            set { this.passwordFormat = value; }
        }        

        public int FailedPasswordAttemptCount
        {
            get { return this.failedPasswordAttemptCount; }
            set { this.failedPasswordAttemptCount = value; }
        }        

        public DateTime FailedPasswordAttemptWindowStart
        {
            get { return this.failedPasswordAttemptWindowStart; }
            set { this.failedPasswordAttemptWindowStart = value; }
        }        

        public int FailedPasswordAnswerAttemptCount
        {
            get { return this.failedPasswordAnswerAttemptCount; }
            set { this.failedPasswordAnswerAttemptCount = value; }
        }        

        public DateTime FailedPasswordAnswerAttemptWindowStart
        {
            get { return this.failedPasswordAnswerAttemptWindowStart; }
            set { this.failedPasswordAnswerAttemptWindowStart = value; }
        }
        
        public IList<RoleContract> Roles
        {
            get { return this.roles; }
        }
    }
}
