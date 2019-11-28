using System;
using UniRx;
using UnityEngine.Networking;

namespace Gulib.Networking
{
    public class GenericRequestBuilder<TRequest> : IOperation<TRequest>
        where TRequest : UnityWebRequest, new()
    {
        public TRequest Request { get; }

        public GenericRequestBuilder()
        {
            Request = new TRequest();
        }

        public IObservable<TRequest> Execute()
        {
            return Request.SendWebRequest().AsObservable().Select(_ => Request);
        }
    }
}
