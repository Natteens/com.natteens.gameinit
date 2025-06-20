using UnityEngine;

namespace GameInit.Utils
{
    public class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        private static T instance;
        public static T Instance => instance;
        protected virtual void Awake()
        {
            if (instance != null && gameObject != null)
                Destroy(gameObject);
            else
                instance = (T)this;
            
            if (!transform.parent)
                DontDestroyOnLoad(gameObject);
        }
    }
}