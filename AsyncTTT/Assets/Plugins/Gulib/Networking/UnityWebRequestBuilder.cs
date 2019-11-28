using Gulib.Abstraction;
using System;
using System.Net.Http;
using UniRx;
using UnityEngine.Networking;

namespace Gulib.Networking
{
    public class UnityWebRequestBuilder : ObservableOperation<UnityWebRequest>
    {
        public UnityWebRequest Request { get; }
        public string Url { set => Request.url = value; }
        public HttpMethod HttpMethod { set => Request.method = value.ToString(); }

        public UnityWebRequestBuilder(UnityWebRequest unityWebRequest = null)
        {
            Request = unityWebRequest ?? new UnityWebRequest();
        }

        public override IObservable<UnityWebRequest> ExecuteAsObservable()
        {
            return Request.SendWebRequest().AsObservable().Select(_ => Request);
        }
    }
}
