using Assets.Scripts.DAL.Rest.Config;
using Assets.Scripts.DAL.Rest.Models;
using Gulib.Abstraction;
using Gulib.Networking;
using System;

namespace Assets.Scripts.DAL.Rest.Operations
{
    public class CheckCredentials : ObservableOperation<LoginResponse>
    {
        private readonly string _login;
        private readonly string _password;

        public CheckCredentials(string login, string password)
        {
            _login = login;
            _password = password;
        }

        protected override IObservable<LoginResponse> ExecuteOnce()
        {
            return new UnityWebRequestBuilder()
            {
                Url = ApiConfig.Endpoints.AzureLogin
            }.Execute().SelectModel<LoginResponse>();
        }
    }
}
