using Assets.Scripts.DAL.Rest.Config;
using Gulib.Abstraction;
using Gulib.Networking;
using System;
using System.Collections.Generic;
using System.Net.Http;

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
