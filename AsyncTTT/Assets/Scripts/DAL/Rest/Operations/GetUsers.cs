using Assets.Scripts.DAL.Rest.Config;
using Assets.Scripts.DAL.Rest.Models;
using Gulib.Abstraction;
using Gulib.Networking;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Assets.Scripts.DAL.Rest.Operations
{
    public class GetUsers : ObservableOperation<List<User>>
    {
        protected override IObservable<List<User>> ExecuteOnce()
        {
            return new UnityWebRequestBuilder()
            {
                Url = ApiConfig.Endpoints.AzureUser,
                HttpMethod = HttpMethod.Get
            }.Execute().SelectModel<List<User>>();
        }
    }
}
