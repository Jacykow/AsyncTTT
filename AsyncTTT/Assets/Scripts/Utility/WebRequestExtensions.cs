using System;
using UniRx;
using UnityEngine.Networking;

namespace Assets.Scripts.Utility
{
    public static class WebRequestExtensions
    {
        public static IObservable<TRequest> Execute<TRequest>(this TRequest request)
            where TRequest : UnityWebRequest
        {
            return request.SendWebRequest().AsObservable().Select(_ => request);
        }
    }
}
