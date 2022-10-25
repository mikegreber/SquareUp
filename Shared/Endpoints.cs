namespace SquareUp.Shared;
public static class ControllerEndpoints
{
    public static class Groups
    {
        public const string BaseUri = "api/Groups/";
        public const string GetAllUri = "";
        public const string GetAllInfoUri = "info";
        public const string GetGroupByIdUri = "{id}";
        public const string GetGroupsByUserIdUri = "user/{id}";
        public const string GetGroupsInfoByUserIdUri = "info/user/{id}";
        public const string PostAddGroupUri = "add-group";
        public const string PostAddParticipantUri = "add-participant";
        public const string PostInviteParticipantUri = "invite";
        public const string PostAddUserUri = "add-user";
        public const string DeleteGroupUri = "delete/{id}";
        public const string PutEditGroupUri = "update";
    }

    public static class Users
    {
        public const string BaseUri = "api/Users/";
        public const string GetAllUri = "";
        public const string GetUserByIdUri = "{id}";
        public const string PostRegisterUri = "register";
        public const string PostLoginUri = "login";
    }

    public static class Transactions
    {
        public const string BaseUri = "api/Transaction/";
        public const string PostAddTransactionUri = "add";
        public const string PutEditTransactionUri = "edit";
        public const string DeleteTransactionUri = "delete/{TransactionId}";
    }
}



public static class Endpoints
{
    public static string Format(this string str)
    {
        var result = string.Empty;
        var index = 0;
        for (var i = 0; i < str.Length; ++i)
        {
            result += str[i];
            if (str[i] != '{') continue;

            result += index++ + "}";
            while (str[i] != '}') ++i;
        }

        return result;
    }


    public static class Groups
    {
        private static string BaseUri => ControllerEndpoints.Groups.BaseUri;
        public static string GetAllGroupsUri => BaseUri + ControllerEndpoints.Groups.GetAllUri;
        public static string GetAllGroupsInfoUri => BaseUri + ControllerEndpoints.Groups.GetAllInfoUri;
        public static string GetGroupByIdUri(int id) => $"{BaseUri + string.Format(ControllerEndpoints.Groups.GetGroupByIdUri.Format(), id)}";
        //public static string GetGroupByIdUri(int id) => $"{Base + $"{id}"}";
        //public static string GetGroupsByUserIdUri(int id) => $"{Base + $"user/{id}"}";
        public static string GetGroupsByUserIdUri(int id) => $"{BaseUri + string.Format(ControllerEndpoints.Groups.GetGroupsByUserIdUri.Format(), id)}";
        public static string GetGroupsInfoByUserIdUri(int id) => $"{BaseUri + string.Format(ControllerEndpoints.Groups.GetGroupsInfoByUserIdUri.Format(), id)}";
        public static string PostAddGroupUri => BaseUri + ControllerEndpoints.Groups.PostAddGroupUri;
        public static string PostAddParticipantUri => BaseUri + ControllerEndpoints.Groups.PostAddParticipantUri;
        public static string PostInviteParticipantUri => BaseUri + ControllerEndpoints.Groups.PostInviteParticipantUri;
        public static string PostAddUserUri => BaseUri + ControllerEndpoints.Groups.PostAddUserUri;
        public static string DeleteGroupUri(int id) => $"{BaseUri + string.Format(ControllerEndpoints.Groups.DeleteGroupUri.Format(), id)}";
        public static string PutEditGroupUri => BaseUri + ControllerEndpoints.Groups.PutEditGroupUri;
    }

    public static class Transactions
    {
        private static string BaseUri => ControllerEndpoints.Transactions.BaseUri;
        public static string PostAddTransactionUri => BaseUri + ControllerEndpoints.Transactions.PostAddTransactionUri;
        public static string PutEditTransactionUri => BaseUri + ControllerEndpoints.Transactions.PutEditTransactionUri;
        public static string DeleteTransactionUri(int transactionId) => $"{BaseUri + string.Format(ControllerEndpoints.Transactions.DeleteTransactionUri.Format(), transactionId)}";
    }

    public static class Users
    {
        private static string BaseUri => ControllerEndpoints.Users.BaseUri;
        public static string GetAllUsersUri => BaseUri + ControllerEndpoints.Users.GetAllUri;
        public static string GetUserByIdUri(int id) => $"{BaseUri + $"{id}"}";
        public static string PostRegisterUri => BaseUri + ControllerEndpoints.Users.PostRegisterUri;
        public static string PostLoginUri => BaseUri + ControllerEndpoints.Users.PostLoginUri;
    }
}
