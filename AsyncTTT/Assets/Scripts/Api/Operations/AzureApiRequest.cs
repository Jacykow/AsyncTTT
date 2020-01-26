using Assets.Scripts.Managers;
using Gulib.Networking;
using System;
using System.Linq;
using System.Net.Http;
using UniRx;

namespace Assets.Scripts.Api.Operations
{
    public class AzureApiRequest : AzureApiQuery<Unit>
    {
        public AzureApiRequest(string url, HttpMethod httpMethod) : base(url, httpMethod) { }

        public override IObservable<Unit> Execute()
        {
            var request = new UnityWebRequestBuilder()
            {
                Url = _url,
                HttpMethod = _httpMethod
            }
            .AddBasicAuthHeader(AuthorizationManager.Main.Login,
                AuthorizationManager.Main.Password);
            if (_model != null)
            {
                request.SetJsonBody(_model);
            }
            return request.Execute().Select(_ => Unit.Default);
        }
    }
}
