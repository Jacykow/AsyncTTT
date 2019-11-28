namespace Assets.Scripts.DAL.Rest.Config
{
    public static class ApiConfig
    {
        public static class Endpoints
        {
            public static string AZURE = "https://asyncttt.azurewebsites.net/api";
            public static string AzureUser => AZURE + "/user";
        }
    }
}
