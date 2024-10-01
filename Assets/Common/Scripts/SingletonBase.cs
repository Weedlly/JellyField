using UnityEngine;

namespace Common.Scripts
{
    public class SingletonBase<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static bool _applicationIsQuitting;
        private static T _instance;
        public static T Instance
        {
            get
            {

                // // When OnDestroy is calling, so we need to prevent create the second singleton instance
                // if (_applicationIsQuitting)
                //     return null;

                // If instance is exist
                if (_instance != null)
                    return _instance;

                // If instance is not exist, Try to find it
                _instance = FindObjectOfType<T>();
                if (_instance != null)
                    return _instance;

                // If instance is not exist, and can't found it on scene, Create the new one
                GameObject obj = new GameObject
                {
                    name = typeof(T).Name,
                };
                _instance = obj.AddComponent<T>();

                return _instance;
            }
        }
        protected virtual void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        protected virtual void OnDestroy()
        {
            // Debug.Log(name + "OnDestroy");
            // _applicationIsQuitting = true;
        }
        public static bool IsAlive()
        {
            return _instance && !_applicationIsQuitting;
        }
    }
}
