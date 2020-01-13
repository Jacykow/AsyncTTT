using AsyncTTT_Backend.Models.Internal;
using Microsoft.AspNetCore.Http;
using System;
using System.Text;

namespace AsyncTTT_Backend.Utility
{
    public static class ControllerUtility
    {
        public static Credentials GetCredentials(IHeaderDictionary headers)
        {
            string authorization = headers["Authorization"].ToString();
            if (string.IsNullOrEmpty(authorization))
            {
                return new Credentials
                {
                    login = "No Authorization Header",
                    password = null
                };
            }
            string[] credentialValues = Encoding.ASCII.GetString(Convert.FromBase64String(authorization.Substring(6))).Split(':');
            return new Credentials
            {
                login = credentialValues[0],
                password = credentialValues[1]
            };
        }
    }
}
