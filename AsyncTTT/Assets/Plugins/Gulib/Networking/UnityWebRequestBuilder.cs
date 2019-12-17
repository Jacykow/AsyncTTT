using Gulib.UniRx;
using System;
using System.Net.Http;
using UniRx;
using UnityEngine;
using UnityEngine.Networking;

namespace Gulib.Networking
{
    public class UnityWebRequestBuilder : IOperation<UnityWebRequest>
    {
        public UnityWebRequest Request { get; }
        public string Url { get => Request.url; set => Request.url = value; }
        public HttpMethod HttpMethod { set => Request.method = value.ToString(); }
        public DownloadHandler DownloadHandler { get => Request.downloadHandler; set => Request.downloadHandler = value; }

        public UnityWebRequestBuilder(UnityWebRequest unityWebRequest = null)
        {
            Request = unityWebRequest ?? new UnityWebRequest();
        }

        public IObservable<UnityWebRequest> Execute()
        {
            DownloadHandler = DownloadHandler ?? new DownloadHandlerBuffer();
            return Request.SendWebRequest()
                .AsObservable()
                .CatchIgnore((Exception e) => Debug.LogException(e))
                .Select(_ => Request);
        }

        public UnityWebRequestBuilder AddHeader(string key, string value)
        {
            Request.SetRequestHeader(key, value);
            return this;
        }
    }
}
