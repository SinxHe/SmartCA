using SmartCA.Infrastructure.Membership;

namespace SmartCA.Model.Membership
{
    public static class ClientMembershipService
    {
        private static IClientMembershipProvider provider;

        static ClientMembershipService()
        {
            ClientMembershipService.provider =
                ProviderFactory.GetProvider<IClientMembershipProvider>() 
                as IClientMembershipProvider;
        }

        public static User GetUser(object userKey)
        {
            return ClientMembershipService.provider.GetUser(userKey);
        }

        public static User GetUser(string username)
        {
            return ClientMembershipService.provider.GetUser(username);
        }

        public static string GetPassword(string username, 
            string passwordAnswer)
        {
            return ClientMembershipService.provider.GetPassword(username, 
                passwordAnswer);
        }

        public static string GetPassword(string username)
        {
            return ClientMembershipService.provider.GetPassword(username);
        }

        public static bool ChangePassword(string username, string oldPassword,
            string newPassword)
        {
            return ClientMembershipService.provider.ChangePassword(username, 
                oldPassword, newPassword);
        }

        public static bool ChangePasswordQuestionAndAnswer(string username, 
            string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            return ClientMembershipService.provider.ChangePasswordQuestionAndAnswer(
                username, password, newPasswordQuestion, newPasswordAnswer);
        }

        public static string ResetPassword(string username)
        {
            return ClientMembershipService.provider.ResetPassword(username);
        }

        public static string ResetPassword(string username, string passwordAnswer)
        {
            return ClientMembershipService.provider.ResetPassword(username, 
                passwordAnswer);
        }

        public static void UpdateUser(User user)
        {
            ClientMembershipService.provider.UpdateUser(user);
        }

        public static Application Application
        {
            get { return ClientMembershipService.provider.Application; }
        }

        public static bool ValidateUser(string username, string password)
        {
            return ClientMembershipService.provider.ValidateUser(username, 
                password);
        }
    }
}
