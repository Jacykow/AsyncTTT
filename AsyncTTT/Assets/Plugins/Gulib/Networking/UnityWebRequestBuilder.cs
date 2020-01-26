using Gulib.UniRx;
using System;
using System.Net.Http;
using System.Text;
using UniRx;
using UnityEngine.Networking;

namespace Gulib.Networking
{
    public class UnityWebRequestBuilder : IOperation<UnityWebRequest>
    {
        public UnityWebRequest Request { get; }
        public string Url { get => Request.url; set => Request.url = value; }
        public HttpMethod HttpMethod { set => Request.method = value.ToString(); }
        public DownloadHandler DownloadHandler { get => Request.downloadHandler; set => Request.downloadHandler = value; }
        public string Body
        {
            get => Encoding.UTF8.GetString(Request.uploadHandler?.data);
            set => Request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(value));
        }

        public UnityWebRequestBuilder(UnityWebRequest unityWebRequest = null)
        {
            Request = unityWebRequest ?? new UnityWebRequest();
        }

        public IObservable<UnityWebRequest> Execute()
        {
            DownloadHandler = DownloadHandler ?? new DownloadHandlerBuffer();
            return Request.SendWebRequest()
                .AsObservable()
                .Select(_ =>
                {
                    if (Request.responseCode < 200 || Request.responseCode >= 300)
                    {
                        throw new WebRequestException(Request);
                    }
                    return Request;
                });
        }

        public UnityWebRequestBuilder AddHeader(string key, string value)
        {
            Request.SetRequestHeader(key, value);
            return this;
        }
    }
}
