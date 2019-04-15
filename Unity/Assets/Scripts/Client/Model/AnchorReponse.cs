using System;

namespace SharingService.Client.Model
{
    [Serializable]
    public class AnchorResponse
    {
        public int id;
        public string name;
        public string key;
        public float latitude;
        public float longitude;
        public DateTime timestamp;
    }
}