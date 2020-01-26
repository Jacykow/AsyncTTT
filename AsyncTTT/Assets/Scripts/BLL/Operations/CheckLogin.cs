using Assets.Scripts.Api.Config;
using Assets.Scripts.Api.Operations;
using Gulib.UniRx;
using System;
using System.Net.Http;
using UniRx;

namespace Assets.Scripts.BLL.Operations
{
    public class CheckLogin : IOperation<Unit>
    {
        public IObservable<Unit> Execute()
        {
            return new AzureApiQuery<bool>(ApiConfig.Endpoints.AzureCredentials, HttpMethod.Get).Execute().Select(success =>
            {
                if (success == false)
                {
                    throw new Exception("Niepoprawne dane.");
                }
                return Unit.Default;
            });
        }
    }
}
