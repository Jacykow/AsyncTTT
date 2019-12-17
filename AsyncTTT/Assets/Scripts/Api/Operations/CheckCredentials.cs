using Assets.Scripts.Api.Config;
using Assets.Scripts.Api.Models;
using Gulib.Networking;
using Gulib.UniRx;
using System;
using UniRx;

namespace Assets.Scripts.Api.Operations
{
    public class CheckCredentials : IOperation<DefaultResponse>
    {
        private readonly string _login;
        private readonly string _password;

        public CheckCredentials(string login, string password)
        {
            _login = login;
            _password = password;
        }

        public IObservable<DefaultResponse> Execute()
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
            .SelectModel<DefaultResponse>();
        }
    }
}
