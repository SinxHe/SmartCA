using System.Data;
using System.Data.Common;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using SmartCA.Infrastructure.EntityFactoryFramework;
using SmartCA.Infrastructure.Helpers;
using SmartCA.Model.Membership;

namespace SmartCA.Infrastructure.Membership.Providers
{
    public class SqlCeClientMembershipProvider : ClientMembershipProvider
    {
        private Database database;
        private Application application;

        protected override void Initialize()
        {
            this.database = DatabaseFactory.CreateDatabase();
            string sql = "SELECT * FROM Application;";
            using (DbCommand command = this.database.GetSqlStringCommand(sql))
            {
                using (IDataReader reader = this.database.ExecuteReader(command))
                {
                    if (reader.Read())
                    {
                        this.application = 
                            ApplicationFactory.BuildApplication(reader);
                    }
                }
            }
        }

        protected override ClientMembershipUser GetUser(User user)
        {
            ClientMembershipUser membershipUser = null;
            string sql = string.Format(
                "SELECT * FROM [User] WHERE UserId = '{0}'", 
                user.UserKey.ToString());
            using (DbCommand command = this.database.GetSqlStringCommand(sql))
            {
                using (IDataReader reader = this.database.ExecuteReader(command))
                {
                    if (reader.Read())
                    {
                        membershipUser = 
                            ClientMembershipUserFactory.BuildClientMembershipUser(
                                user, reader);
                    }
                }
            }
            return membershipUser;
        }

        protected override void PersistChangedPassword(ClientMembershipUser user, 
            string newPassword)
        {
            string sql = string.Format("UPDATE [User] SET Password = '{0}' WHERE UserId = '{1}'",
                newPassword, user.UserKey.ToString());
            this.database.ExecuteNonQuery(
                    this.database.GetSqlStringCommand(sql));
        }

        protected override void PersistChangedPasswordQuestionAndAnswer(ClientMembershipUser user, 
            string password, string newPasswordAnswer)
        {
            string sql = string.Format("UPDATE [User] SET PasswordQuestion = '{0}', PasswordAnswer = '{1}' WHERE UserId = '{2}'",
                user.PasswordQuestion, newPasswordAnswer, user.UserKey.ToString());
            this.database.ExecuteNonQuery(
                    this.database.GetSqlStringCommand(sql));
        }

        protected override string GetPasswordAnswerFromPersistence(ClientMembershipUser user)
        {
            string sql = string.Format("SELECT PasswordAnswer FROM [User] WHERE UserId = '{0}'",
                user.UserKey.ToString());
            return this.database.ExecuteScalar(
                    this.database.GetSqlStringCommand(sql)).ToString();
        }

        protected override string GetPasswordFromPersistence(ClientMembershipUser user)
        {
            string sql = string.Format("SELECT Password FROM [User] WHERE UserId = '{0}'",
                user.UserKey.ToString());
            return this.database.ExecuteScalar(
                    this.database.GetSqlStringCommand(sql)).ToString();
        }

        protected override void PersistResetPassword(ClientMembershipUser user, 
            string encodedPasswordAnswer, string newEncodedPassword)
        {
            string sql = string.Format("UPDATE [User] SET Password = '{0}', PasswordAnswer = '{1}' WHERE UserId = '{2}'",
                newEncodedPassword, encodedPasswordAnswer, user.UserKey.ToString());
            this.database.ExecuteNonQuery(
                    this.database.GetSqlStringCommand(sql));
        }

        protected override void PersistResetPassword(ClientMembershipUser user, string newEncodedPassword)
        {
            string sql = string.Format("UPDATE [User] SET Password = '{0}' WHERE UserId = '{1}'",
                newEncodedPassword, user.UserKey.ToString());
            this.database.ExecuteNonQuery(
                    this.database.GetSqlStringCommand(sql));
        }

        protected override void PersistUser(ClientMembershipUser user)
        {
            StringBuilder builder = new StringBuilder(100);
            builder.Append("UPDATE [User] SET ");

            builder.Append(string.Format("{0}={1}",
                UserFactory.FieldNames.Email,
                DataHelper.GetSqlValue(user.Email)));

            builder.Append(string.Format(",{0}={1}",
                UserFactory.FieldNames.IsLockedOut,
                DataHelper.GetSqlValue(user.IsLockedOut)));

            builder.Append(string.Format(",{0}={1}",
                UserFactory.FieldNames.LastActivityDate,
                DataHelper.GetSqlValue(user.LastActivityDate)));

            builder.Append(string.Format(",{0}={1}",
                UserFactory.FieldNames.LastLockoutDate,
                DataHelper.GetSqlValue(user.LastLockoutDate)));

            builder.Append(string.Format(",{0}={1}",
                UserFactory.FieldNames.LastLoginDate,
                DataHelper.GetSqlValue(user.LastLoginDate)));

            builder.Append(string.Format(",{0}={1}",
                UserFactory.FieldNames.LastPasswordChangedDate,
                DataHelper.GetSqlValue(user.LastPasswordChangedDate)));

            builder.Append(string.Format(",{0}={1}",
                UserFactory.FieldNames.PasswordQuestion,
                DataHelper.GetSqlValue(user.PasswordQuestion)));

            builder.Append(string.Format(",{0}={1}",
                UserFactory.FieldNames.UserName,
                DataHelper.GetSqlValue(user.UserName)));

            builder.Append(string.Format(",{0}={1}",
                ClientMembershipUserFactory.FieldNames.PasswordSalt,
                DataHelper.GetSqlValue(user.PasswordSalt)));

            builder.Append(string.Format(",{0}={1}",
                ClientMembershipUserFactory.FieldNames.PasswordFormat,
                DataHelper.GetSqlValue((int)user.PasswordFormat)));

            builder.Append(" ");
            builder.Append(string.Format("WHERE UserId = '{0}'", 
                user.UserKey.ToString()));

            this.database.ExecuteNonQuery(
                this.database.GetSqlStringCommand(builder.ToString()));
        }

        public override User GetUser(object userKey)
        {
            return this.GetUserFromSql(string.Format
                ("SELECT * FROM [User] WHERE UserId = '{0}'", userKey));
        }

        private User GetUserFromSql(string sql)
        {
            User user = null;
            using (DbCommand command = this.database.GetSqlStringCommand(sql))
            {
                using (IDataReader reader = this.database.ExecuteReader(command))
                {
                    IEntityFactory<User> entityFactory =
                        EntityFactoryBuilder.BuildFactory<User>();
                    if (reader.Read())
                    {
                        user = entityFactory.BuildEntity(reader);
                    }
                }
            }
            return user;
        }

        public override User GetUser(string username)
        {
            return this.GetUserFromSql(string.Format
                ("SELECT * FROM [User] WHERE UserName = '{0}'", username));
        }

        public override Application Application
        {
            get { return this.application; }
        }
    }
}
