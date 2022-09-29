namespace SquareUp.Shared;
public static class ControllerEndpoints
{
    public static class Groups
    {
        public const string Base = "api/Groups/";
        public const string GetAllPath = "";
        public const string GetGroupById = "{id}";
        public const string PostAddGroup = "add-group";
        public const string PostAddExpense = "add-expense";
        public const string PostAddUser = "add-user";
    }

    public static class Users
    {
        public const string Base = "api/Users/";
        public const string GetAll = "";
        public const string GetUserById = "{id}";
        public const string PostRegister = "register";
        public const string PostLogin = "login";
    }

}

public static class Endpoints
{
    public static class Groups
    {
        private static string Base => ControllerEndpoints.Groups.Base;
        public static string GetAllGroupsUri => Base + ControllerEndpoints.Groups.GetAllPath;
        public static string GetGroupByIdUri(int id) => $"{Base + $"{id}"}";
        public static string PostAddGroupUri => Base + ControllerEndpoints.Groups.PostAddGroup;
        public static string PostAddExpenseUri => Base + ControllerEndpoints.Groups.PostAddExpense;
        public static string PostAddUserUri => Base + ControllerEndpoints.Groups.PostAddUser;
    }

    public static class Users
    {
        private static string Base => ControllerEndpoints.Users.Base;
        public static string GetAllUsersUri => Base + ControllerEndpoints.Users.GetAll;
        public static string GetUserByIdUri(int id) => $"{Base + $"{id}"}";
        public static string PostRegisterUri => Base + ControllerEndpoints.Users.PostRegister;
        public static string PostLoginUri => Base + ControllerEndpoints.Users.PostLogin;
    }
}
