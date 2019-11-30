using Newtonsoft.Json;
using System;
using UniRx;
using UnityEngine.Networking;

namespace Gulib.Networking
{
    public static class UnityWebRequestExtensions
    {
        public static IObservable<TModel> SelectModel<TModel>(this IObservable<UnityWebRequest> observableRequest, JsonSerializerSettings jsonSettings = null)
        {
            jsonSettings = jsonSettings ?? new JsonSerializerSettings();
            return observableRequest.Select(request =>
            {
                return JsonConvert.DeserializeObject<TModel>(request.downloadHandler.text, jsonSettings);
            });
        }
    }
}
