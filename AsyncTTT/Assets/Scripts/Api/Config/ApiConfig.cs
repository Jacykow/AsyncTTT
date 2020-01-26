namespace Assets.Scripts.Api.Config
{
    public static class ApiConfig
    {
        public static class Endpoints
        {
#if LOCAL_API
            public static string AZURE = "https://localhost:44388/api";
#else
            public static string AZURE = "https://atttgame.azurewebsites.net/api";
#endif
            public static string AzureCredentials => AZURE + "/credentials";
            public static string AzureBoard => AZURE + "/board";
            public static string AzureBoardId => AZURE + "/boardid";
            public static string AzureFriends => AZURE + "/friends";
            public static string AzureFriendsInvitation => AZURE + "/friendsinvitation";
        }
    }
}
