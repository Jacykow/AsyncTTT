using Assets.Scripts.DAL.Rest.Config;
using Assets.Scripts.DAL.Rest.Models;
using Gulib.Networking;
using Gulib.UniRx;
using System;
using UniRx;
using UnityEngine;

namespace Assets.Scripts.DAL.Rest.Operations
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
            .CatchIgnore((Exception e) => Debug.LogException(e))
            .Select(response =>
            {
                return response;
            })
            .SelectModel<DefaultResponse>();
        }
    }
}
