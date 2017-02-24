using System;
using System.Data;
using SmartCA.Infrastructure.EntityFactoryFramework;
using SmartCA.Model.Membership;
using SmartCA.Infrastructure.Helpers;

namespace SmartCA.Infrastructure.Membership.Providers
{
    public class UserFactory : IEntityFactory<User>
    {
        #region IEntityFactory<User> Members

        internal static class FieldNames
        {
            public const string UserId = "UserId";
            public const string UserName = "UserName";
            public const string Password = "Password";
            public const string MobileAlias = "MobileAlias";
            public const string MobilePin = "MobilePIN";
            public const string Email = "Email";
            public const string PasswordQuestion = "PasswordQuestion";
            public const string PasswordAnswer = "PasswordAnswer";
            public const string IsApproved = "IsApproved";
            public const string IsLockedOut = "IsLockedOut";
            public const string CreateDate = "CreateDate";
            public const string LastLoginDate = "LastLoginDate";
            public const string LastPasswordChangedDate = "LastPasswordChangedDate";
            public const string LastLockoutDate = "LastLockoutDate";
            public const string LastActivityDate = "LastActivityDate";
            public const string FailedPasswordAttemptCount = "FailedPasswordAttemptCount";
            public const string FailedPasswordAttemptWindowStart = "FailedPasswordAttemptWindowStart";
            public const string FailedPasswordAnswerAttemptCount = "FailedPasswordAnswerAttemptCount";
            public const string FailedPasswordAnswerAttemptWindowStart = "FailedPasswordAnswerAttemptWindowStart";
            public const string Comment = "Comment";
        }

        public User BuildEntity(IDataReader reader)
        {
            return new User(DataHelper.GetGuid(reader[FieldNames.UserId]),
                DataHelper.GetString(reader[FieldNames.UserName]),
                DataHelper.GetString(reader[FieldNames.Email]),
                DataHelper.GetString(reader[FieldNames.PasswordQuestion]),
                DataHelper.GetBoolean(reader[FieldNames.IsLockedOut]),
                DataHelper.GetDateTime(reader[FieldNames.CreateDate]),
                DataHelper.GetDateTime(reader[FieldNames.LastLoginDate]),
                DataHelper.GetDateTime(reader[FieldNames.LastActivityDate]),
                DataHelper.GetDateTime(reader[FieldNames.LastPasswordChangedDate]),
                DataHelper.GetDateTime(reader[FieldNames.LastLockoutDate]));
        }

        #endregion
    }
}
