using Newtonsoft.Json;
using System;
using System.Text;
using UniRx;
using UnityEngine.Networking;

namespace Gulib.Networking
{
    public static class UnityWebRequestExtensions
    {
        public static IObservable<TModel> SelectModel<TModel>
            (this IObservable<UnityWebRequest> observableRequest, JsonSerializerSettings jsonSettings = null)
        {
            jsonSettings = jsonSettings ?? new JsonSerializerSettings();
            return observableRequest.Select(request =>
            {
                return JsonConvert.DeserializeObject<TModel>(request.downloadHandler.text, jsonSettings);
            });
        }

        public static UnityWebRequestBuilder AddBasicAuthHeader(this UnityWebRequestBuilder requestBuilder, string login, string password)
        {
            string encodedCredentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(login + ":" + password));
            return requestBuilder.AddHeader("Authorization", "Basic " + encodedCredentials);
        }

        public static UnityWebRequestBuilder SetJsonBody(this UnityWebRequestBuilder requestBuilder, object model, JsonSerializerSettings jsonSettings = null)
        {
            jsonSettings = jsonSettings ?? new JsonSerializerSettings();
            requestBuilder.Body = JsonConvert.SerializeObject(model, jsonSettings);
            requestBuilder.AddHeader("Content-Type", "application/json");
            return requestBuilder;
        }
    }
}
