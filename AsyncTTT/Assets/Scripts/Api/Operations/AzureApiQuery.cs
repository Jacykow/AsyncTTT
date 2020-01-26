using Assets.Scripts.Managers;
using Gulib.Networking;
using Gulib.UniRx;
using System;
using System.Net.Http;

namespace Assets.Scripts.Api.Operations
{
    public class AzureApiQuery<TResponseModel> : IOperation<TResponseModel>
    {
        protected readonly string _url;
        protected readonly HttpMethod _httpMethod;

        protected object _model;

        public AzureApiQuery(string url, HttpMethod httpMethod)
        {
            _url = url;
            _httpMethod = httpMethod;
        }

        public AzureApiQuery<TResponseModel> SetBodyModel(object model)
        {
            _model = model;
            return this;
        }

        public virtual IObservable<TResponseModel> Execute()
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
            return request
                .Execute()
                .SelectModel<TResponseModel>();
        }
    }
}
