using Assets.Scripts.Api.Config;
using Gulib.Networking;
using Gulib.UniRx;
using System;
using UniRx;

namespace Assets.Scripts.Api.Operations
{
    public class CheckCredentials : IOperation<Unit>
    {
        private readonly string _login;
        private readonly string _password;

        public CheckCredentials(string login, string password)
        {
            _login = login;
            _password = password;
        }

        public IObservable<Unit> Execute()
        {
            return new UnityWebRequestBuilder()
            {
                Url = ApiConfig.Endpoints.AzureUser
            }
            .AddBasicAuthHeader(_login, _password)
            .Execute()
            .Select(response =>
            {
                return response;
            })
            .SelectModel<Unit>();
        }
    }
}
