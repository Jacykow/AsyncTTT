namespace Assets.Scripts.Api.Config
{
    public static class ApiConfig
    {
        public static class Endpoints
        {
            public static string AZURE = "https://atttgame.azurewebsites.net/api";
            public static string AzureUser => AZURE + "/user";
            public static string AzureCredentials => AZURE + "/credentials";
            public static string AzureBoard => AZURE + "/board";
            public static string AzureBoardId => AZURE + "/boardid";
            public static string AzureFriends => AZURE + "/friends";
            public static string AzureFriendsInvitation => AZURE + "/friendsinvitation";
        }
    }
}
