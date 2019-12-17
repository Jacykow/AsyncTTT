namespace Assets.Scripts.DAL.Rest.Config
{
    public static class ApiConfig
    {
        public static class Endpoints
        {
#if LOCAL_API
            public static string AZURE = "https://localhost:44388/api";
#else
            public static string AZURE = "https://asyncttt.azurewebsites.net/api";
#endif
            public static string AzureUser => AZURE + "/user";
        }
    }
}
