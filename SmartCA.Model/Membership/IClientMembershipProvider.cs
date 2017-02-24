
namespace SmartCA.Model.Membership
{
    public interface IClientMembershipProvider
    {
        #region Methods

        User GetUser(object userKey);
        User GetUser(string username);
        string GetPassword(string username);
        string GetPassword(string username, string answer);
        bool ChangePassword(string username, string oldPassword, 
            string newPassword);
        bool ChangePasswordQuestionAndAnswer(string username, string password,
            string newPasswordQuestion, string newPasswordAnswer);
        string ResetPassword(string username);
        string ResetPassword(string username, string answer);
        void UpdateUser(User user);
        bool ValidateUser(string username, string password);

        #endregion

        #region Properties

        Application Application { get; }

        #endregion
    }
}
