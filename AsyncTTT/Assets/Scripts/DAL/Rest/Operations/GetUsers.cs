using Assets.Scripts.DAL.Rest.Config;
using Assets.Scripts.DAL.Rest.Models;
using Gulib.Networking;
using Gulib.UniRx;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Assets.Scripts.DAL.Rest.Operations
{
    public class GetUsers : IOperation<List<User>>
    {
        public IObservable<List<User>> Execute()
        {
            return new UnityWebRequestBuilder()
            {
                Url = ApiConfig.Endpoints.AzureUser,
                HttpMethod = HttpMethod.Get
            }.Execute().SelectModel<List<User>>();
        }
    }
}
