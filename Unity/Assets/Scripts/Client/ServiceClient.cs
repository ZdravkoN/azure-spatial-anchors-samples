using SharingService.Client;
using SharingService.Client.Model;
using System;
using UnityEngine;

namespace SharingService
{
    public class ServiceClient : MonoBehaviour
    {
        [SerializeField]
        private string _host = string.Empty;

        public static ServiceClient Instance { get; private set; }

        private HttpClient _client;

        protected HttpClient Client
        {
            get
            {
                if (_client == null)
                {
                    var settings = new ClientSettings
                    {
                        Host = _host
                    };
                    _client = new HttpClient(settings);
                }
                return _client;
            }
        }

        void Awake()
        {
            Instance = this;
        }

        public void GetAllAnchors(Action<AnchorResponse[]> result, Action<string> error)
        {
            Client.GetResponseForUrl<AnchorResponse[]>(
                "/api/anchors",
                result,
                error,
                responseBody => {
                    return SharingService.Client.Util.Deserializer.DeserializeArray<AnchorResponse>(responseBody);
                });
        }

        public void UploadAnchor(AnchorRequest request, Action<AnchorResponse> result, Action<string> error)
        {
            Client.PostToForUrl<AnchorResponse>(
                "/api/anchors",
                JsonUtility.ToJson(request, false),
                result,
                error,
                responseBody => {
                    return SharingService.Client.Util.Deserializer.Deserialize<AnchorResponse>(responseBody);
                });
        }
    }
}
