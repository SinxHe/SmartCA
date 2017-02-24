using System.Data;
using SmartCA.Infrastructure.Helpers;
using SmartCA.Model.Membership;

namespace SmartCA.Infrastructure.Membership.Providers
{
    public class ClientMembershipUserFactory
    {
        internal static class FieldNames
        {
            public const string PasswordFormat = "PasswordFormat";
            public const string PasswordSalt = "PasswordSalt";
            public const string FailedPasswordAttemptCount = "FailedPasswordAttemptCount";
            public const string FailedPasswordAttemptWindowStart = "FailedPasswordAttemptWindowStart";
            public const string FailedPasswordAnswerAttemptCount = "FailedPasswordAnswerAttemptCount";
            public const string FailedPasswordAnswerAttemptWindowStart = "FailedPasswordAnswerAttemptWindowStart";
        }

        public static ClientMembershipUser BuildClientMembershipUser(User user, IDataReader reader)
        {
            return new ClientMembershipUser(user,
                DataHelper.GetString(reader[FieldNames.PasswordSalt]),
                DataHelper.GetEnumValue<PasswordFormat>(reader[FieldNames.PasswordFormat]),
                DataHelper.GetInteger(reader[FieldNames.FailedPasswordAttemptCount]),
                DataHelper.GetDateTime(reader[FieldNames.FailedPasswordAttemptWindowStart]),
                DataHelper.GetInteger(reader[FieldNames.FailedPasswordAnswerAttemptCount]),
                DataHelper.GetDateTime(reader[FieldNames.FailedPasswordAnswerAttemptWindowStart]));
        }
    }
}
