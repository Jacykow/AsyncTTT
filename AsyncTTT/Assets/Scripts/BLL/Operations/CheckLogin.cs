using Assets.Scripts.Api.Config;
using Assets.Scripts.Api.Models;
using Assets.Scripts.Api.Operations;
using Assets.Scripts.Managers;
using Gulib.UniRx;
using System;
using System.Collections.Generic;
using System.Net.Http;
using UniRx;

namespace Assets.Scripts.BLL.Operations
{
    public class CheckLogin : IOperation<Unit>
    {
        public IObservable<Unit> Execute()
        {
            return new AzureApiQuery<bool>(ApiConfig.Endpoints.AzureCredentials, HttpMethod.Get).Execute().SelectMany(success =>
            {
                if (success == false)
                {
                    throw new Exception("Niepoprawne dane.");
                }
                return new AzureApiQuery<List<User>>(ApiConfig.Endpoints.AzureUser + "/nick/" + AuthorizationManager.Main.Login, HttpMethod.Get).Execute();
            }).Select(userDetails =>
            {
                AuthorizationManager.Main.Id = userDetails[0].Id;
                return Unit.Default;
            });
        }
    }
}
