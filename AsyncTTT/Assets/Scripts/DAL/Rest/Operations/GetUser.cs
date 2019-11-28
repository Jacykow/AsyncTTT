using Assets.Scripts.DAL.Rest.Config;
using Gulib.Networking;
using Gulib.Abstraction;
using System;
using System.Net.Http;
using UniRx;

public class GetUser : ObservableOperation<User>
{
    public override IObservable<User> ExecuteAsObservable()
    {
        return new UnityWebRequestBuilder()
        {
            Url = ApiConfig.Endpoints.AzureUser,
            HttpMethod = HttpMethod.Get
        }.Select(_ => new User());
    }
}
