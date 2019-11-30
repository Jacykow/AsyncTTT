using Assets.Scripts.DAL.Rest.Config;
using Gulib.Abstraction;
using Gulib.Networking;
using System;
using System.Collections.Generic;
using System.Net.Http;

public class GetUsers : ObservableOperation<List<User>>
{
    public override IObservable<List<User>> ExecuteAsObservable()
    {
        return new UnityWebRequestBuilder()
        {
            Url = ApiConfig.Endpoints.AzureUser,
            HttpMethod = HttpMethod.Get
        }.SelectModel<List<User>>();
    }
}
