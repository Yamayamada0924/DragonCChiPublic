using UnityEngine;
using System;

namespace yydlib
{
    [DisallowMultipleComponent]
    public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        /// <summary>
        /// DontDestroyOnLoadするかどうか
        /// </summary>
        protected new abstract bool DontDestroyOnLoad { get; }
        
        private static T _instance;
        public static T Instance
        {
            get
            {
                if (!_instance)
                {
                    Type t = typeof(T);

                    _instance = (T)FindObjectOfType(t);
                    if (!_instance)
                    {
                        GameUtil.LogError(t + " is nothing.");
                    }
                }

                return _instance;
            }
        }
        
        private void Awake()
        {
            if (this != Instance)
            {
                Destroy(gameObject);
                return;
            }
            if (DontDestroyOnLoad)
            {
                DontDestroyOnLoad(this.gameObject);
            }

            Init();
        }
        
        /// <summary>
        /// Awakeの代わり
        /// </summary>
        protected abstract void Init();
    }

}