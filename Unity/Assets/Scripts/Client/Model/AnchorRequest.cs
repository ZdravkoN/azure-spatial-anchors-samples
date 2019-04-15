using System;

namespace SharingService.Client.Model
{
    [Serializable]
    public class AnchorRequest
    {
        public string name;
        public string key;
        public float latitude;
        public float longitude;
    }
}