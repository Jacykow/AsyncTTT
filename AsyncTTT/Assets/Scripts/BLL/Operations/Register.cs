using Assets.Scripts.Api.Config;
using Assets.Scripts.Api.Operations;
using Gulib.UniRx;
using System;
using System.Net.Http;
using UniRx;

namespace Assets.Scripts.BLL.Operations
{
    public class Register : IOperation<Unit>
    {
        public IObservable<Unit> Execute()
        {
            return new AzureApiRequest(ApiConfig.Endpoints.AzureCredentials, HttpMethod.Post).Execute();
        }
    }
}
