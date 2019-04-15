using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace SharingService.Client
{
    public class HttpClient
    {
        protected ClientSettings Settings;

        public HttpClient(ClientSettings settings)
        {
            Settings = settings;
        }

        public IEnumerator GetResponseForUrl<T>(string url, Action<T> result, Action<string> error, Func<string, T> parser)
        {
            var urlToUse = GetUrl(url);
            var authHeader = GetAuthorizationHeader();

            using (var request = UnityWebRequest.Get(urlToUse))
            {
                request.SetRequestHeader("Content-Type", "application/json");
                if (!string.IsNullOrWhiteSpace(authHeader))
                {
                    request.SetRequestHeader("Authorization", authHeader);
                }
                yield return request.SendWebRequest();

                if (request.isNetworkError || request.isHttpError)
                {
                    if (!string.IsNullOrEmpty(request.error))
                    {
                        error(request.error);
                        yield break;
                    }
                }
                if (request.responseCode == 200)
                {
                    while (!request.downloadHandler.isDone)
                    {
                        yield return null;
                    }
                    var responseAsString = request.downloadHandler.text;
                    var tempResult = parser(responseAsString);

                    if (result != null)
                    {
                        result(tempResult);
                    }
                }
                else
                {
                    if (error != null)
                    {
                        error(request.error);
                    }
                }
            }
        }

        public IEnumerator PostToForUrl<T>(string url, object objectToPost, Action<T> result, Action<string> error, Func<string, T> parser)
        {
            var urlToUse = GetUrl(url);
            var authHeader = GetAuthorizationHeader();

            var data = JsonUtility.ToJson(objectToPost);

            using (var request = UnityWebRequest.Post(urlToUse, data))
            {
                request.SetRequestHeader("Content-Type", "application/json");
                if (!string.IsNullOrWhiteSpace(authHeader))
                {
                    request.SetRequestHeader("Authorization", authHeader);
                }
                yield return request.SendWebRequest();

                if (request.isNetworkError || request.isHttpError)
                {
                    if (!string.IsNullOrEmpty(request.error))
                    {
                        error(request.error);
                        yield break;
                    }
                }
                if (request.responseCode == 200)
                {
                    while (!request.downloadHandler.isDone)
                    {
                        yield return null;
                    }
                    var responseAsString = request.downloadHandler.text;
                    var tempResult = parser(responseAsString);

                    if (result != null)
                    {
                        result(tempResult);
                    }
                }
                else
                {
                    if (error != null)
                    {
                        error(request.error);
                    }
                }
            }
        }

        protected string GetUrl(string relativeUrl)
        {
            var urlToUse = new Uri(new Uri(Settings.Host), relativeUrl);
            return urlToUse.ToString();
        }

        protected string GetAuthorizationHeader()
        {
            if (String.IsNullOrWhiteSpace(Settings.Username) || String.IsNullOrWhiteSpace(Settings.Password))
            {
                return null;
            }
            else
            {
                var bytes = Encoding.UTF8.GetBytes(string.Format("{0}:{1}", Settings.Username, Settings.Password));
                var base64 = Convert.ToBase64String(bytes);
                return string.Format("Basic {0}", base64);
            }
        }
    }
}
