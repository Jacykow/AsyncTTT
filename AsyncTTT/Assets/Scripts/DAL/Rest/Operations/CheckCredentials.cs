using Assets.Scripts.DAL.Rest.Config;
using Assets.Scripts.DAL.Rest.Models;
using Gulib.Networking;
using Gulib.UniRx;
using System;

namespace Assets.Scripts.DAL.Rest.Operations
{
    public class CheckCredentials : IOperation<LoginResponse>
    {
        private readonly string _login;
        private readonly string _password;

        public CheckCredentials(string login, string password)
        {
            _login = login;
            _password = password;
        }

        public IObservable<LoginResponse> Execute()
        {
            return new UnityWebRequestBuilder()
            {
                Url = ApiConfig.Endpoints.AzureLogin
            }.Execute().SelectModel<LoginResponse>();
        }
    }
}
