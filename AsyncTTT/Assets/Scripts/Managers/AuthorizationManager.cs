namespace Assets.Scripts.Managers
{
    public class AuthorizationManager
    {
        private static AuthorizationManager _manager;

        public static AuthorizationManager Main
        {
            get
            {
                if (_manager == null)
                {
                    _manager = new AuthorizationManager();
                }
                return _manager;
            }
        }

        public string Login { get; set; }
        public string Password { get; set; }
    }
}
