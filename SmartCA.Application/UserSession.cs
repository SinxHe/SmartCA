using System;
using System.Collections.Generic;
using System.Text;
using SmartCA.Model.Projects;
using SmartCA.Model;
using SmartCA.Model.Membership;

namespace SmartCA.Application
{
    public static class UserSession
    {
        private static Project currentProject;
        private static OfficeLocation currentOffice;
        private static DateTime? lastSynchronization;
        private static User currentUser;

        public static User CurrentUser
        {
            get { return UserSession.currentUser; }
            set { UserSession.currentUser = value; }
        }

        public static OfficeLocation CurrentOffice
        {
            get { return UserSession.currentOffice; }
            set { UserSession.currentOffice = value; }
        }

        public static Project CurrentProject
        {
            get { return UserSession.currentProject; }
            set { UserSession.currentProject = value; }
        }

        public static DateTime? LastSynchronization
        {
            get { return UserSession.lastSynchronization; }
            set { UserSession.lastSynchronization = value; }
        }
    }
}
