using Newtonsoft.Json;
using System;
using System.Text;
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

        public static void AddBasicAuthHeader(this UnityWebRequestBuilder requestBuilder, string login, string password)
        {
            string encodedCredentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(login + ":" + password));
            requestBuilder.AddHeader("Authorization", "Basic " + encodedCredentials);
        }
    }
}
