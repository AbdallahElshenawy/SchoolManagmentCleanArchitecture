namespace SchoolManagment.Data.AppMetaData
{
    public static class Routing
    {
        public const string root = "Api/";
        public const string version = "V1/";
        public const string rule = root + version;

        public static class StudentRouting
        {
            public const string prefix = rule + "Students";
            public const string GetStudents = prefix + "/List";
            public const string GetStudentById = prefix + "/{id}";
            public const string Create = prefix + "/Create";
            public const string Edit = prefix + "/Edit";
            public const string Delete = prefix + "/{id}";
            public const string paginatedList = prefix + "/paginatedList";

        }
        public static class DepartmentRouting
        {
            public const string prefix = rule + "Departments";
            public const string GetDepartmentById = prefix + "/{id}";
        }
        public static class UserRouting
        {
            public const string prefix = rule + "Users";
            public const string Create = prefix + "/Create";
            public const string paginatedList = prefix + "/paginatedList";
            public const string GetUserById = prefix + "/{id}";
            public const string Edit = prefix + "/Edit";
            public const string ChangePassword = prefix + "/ChangePassword";
            public const string Delete = prefix + "/{id}";

        }
        public static class AuthRouting
        {
            public const string prefix = rule + "Authentication";
            public const string SignIn = prefix + "/SignIn";
            public const string RefreshToken = prefix + "/RefreshToken";
            public const string ValidateToken = prefix + "/ValidateToken";
        }
        public static class AuthorizationRouting
        {
            public const string Prefix = rule + "Authorization";
            public const string Roles = Prefix + "/Roles";
            public const string Claims = Prefix + "/Claims";
            public const string Create = Roles + "/Create";
            public const string Edit = Roles + "/Edit";
            public const string Delete = Roles + "/ Delete";
            public const string GetRolesList = Roles + "/GetRolesList";
            public const string GetRoleById = Roles + "/GetRoleById";
            public const string GetUserRoles = Roles + "/GetUserRoles";
            public const string EditUserRoles = Roles + "/EditUserRoles";
            public const string ManageUserClaims = Claims + "/ManageUserClaims";
        }
        public static class EmailsRoute
        {
            public const string prefix = rule + "EmailsRoute";
            public const string SendEmail = prefix + "/SendEmail";

        }
    }
}
