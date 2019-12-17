using System;
using UnityEngine.Networking;

namespace Gulib.Networking
{
    public class WebRequestException : Exception
    {
        public UnityWebRequest Request { get; }

        public WebRequestException(UnityWebRequest request = null) : base()
        {
            Request = request;
        }

        public WebRequestException(string message, UnityWebRequest request = null) : base(message)
        {
            Request = request;
        }

        public WebRequestException(string message, Exception innerException, UnityWebRequest request = null) : base(message, innerException)
        {
            Request = request;
        }
    }
}
