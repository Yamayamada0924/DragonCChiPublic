using System.Collections.Generic;
using UnityEngine;

namespace yydlib
{
    public abstract class ObjectListPool<TGameObject, TType> : MonoBehaviour where TGameObject : MonoBehaviour
    {
        [System.SerializableAttribute]
        private class PrefabParam
        {
            public TGameObject prefab;
            public int poolNum;
        }
        
        [SerializeField]
        private List<PrefabParam> prefabParams = new List<PrefabParam>();

        public Transform PoolTransform { get; private set; }
        
        protected  abstract bool Extend { get; }
        
        private readonly List<List<TGameObject>> _gameObjectPool = new List<List<TGameObject>>();

        public void Init()
        {
            PoolTransform = this.transform;
            for (int iPrefabParam = 0; iPrefabParam < prefabParams.Count; iPrefabParam++)
            {
                _gameObjectPool.Add(new List<TGameObject>(prefabParams[iPrefabParam].poolNum));
                for(int iPrefabNum = 0; iPrefabNum < prefabParams[iPrefabParam].poolNum; iPrefabNum++)
                {
                    var obj = Instantiate(prefabParams[iPrefabParam].prefab, PoolTransform);
                    OnCreateGameObject(obj);
                    _gameObjectPool[iPrefabParam].Add(obj);
                }
            }
        }
        
        public TGameObject GetGameObject(TType type)
        {
            foreach (var poolGameObject in _gameObjectPool[GetIndex(type)])
            {
                if (!IsActive(poolGameObject))
                {
                    SetActive(poolGameObject, type);
                    return poolGameObject;
                }
            }

            if (Extend)
            {
                var obj = Instantiate(prefabParams[GetIndex(type)].prefab, this.transform);
                OnCreateGameObject(obj);
                _gameObjectPool[GetIndex(type)].Add(obj);
                SetActive(obj, type);
                GameUtil.LogWarning($"{type.ToString()} extended!");
                return obj;
            }
            else
            {
                GameUtil.Assert(false);
            }
            return null;
        }

        public void RestoreGameObject(TGameObject poolGameObject)
        {
            GameUtil.Assert(IsActive(poolGameObject));
            SetInactive(poolGameObject);
        }

        protected abstract void OnCreateGameObject(TGameObject poolGameObject);

        protected abstract bool IsActive(TGameObject poolGameObject);

        protected abstract void SetActive(TGameObject poolGameObject, TType buildingViewParam);

        protected abstract void SetInactive(TGameObject poolGameObject);

        protected abstract int GetIndex(TType type);
    }

}