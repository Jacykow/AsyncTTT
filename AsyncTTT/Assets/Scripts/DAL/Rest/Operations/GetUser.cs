using Assets.Scripts.Utility;
using System;
using UniRx;
using UnityEngine.Networking;

public class GetUser : IOperation<User>
{
    public IObservable<User> Execute()
    {
        return new UnityWebRequest("https://asyncttt.azurewebsites.net/api/user", "GET").Execute().Select(_ => new User());
    }
}
