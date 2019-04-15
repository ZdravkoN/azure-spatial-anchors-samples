using System;
using UnityEngine;
#if UNITY_UWP || UNITY_WSA
//using Newtonsoft.Json;
#endif

namespace SharingService.Client.Util
{
    public static class Deserializer
    {
        public static T[] DeserializeArray<T>(string json)
        {
            string newJson = "{ \"array\": " + json + "}";
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(newJson);
            return wrapper.array;
#if UNITY_UWP || UNITY_WSA
            //return JsonConvert.DeserializeObject<T[]>(json);
#else
            //string newJson = "{ \"array\": " + json + "}";
            //Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(newJson);
            //return wrapper.array;
#endif
        }

        public static T Deserialize<T>(string json)
        {
            return JsonUtility.FromJson<T>(json);
#if UNITY_UWP || UNITY_WSA
            //return JsonConvert.DeserializeObject<T>(json);
#else
            //return JsonUtility.FromJson<T>(json);
#endif
        }
    }

    [Serializable]
    public class Wrapper<T>
    {
        public T[] array;
    }
}
