using System.Collections.Generic;
using UnityEngine;

namespace Common.Scripts.Pooler
{
    public class PoolingBase : MonoBehaviour
    {
        private int _initNumber;
        private GameObject _parent;
        private List<GameObject> _poolObjects;
        private GameObject _prefab;
        public void InitPoolWithParam(int initNumber, GameObject prefab, GameObject parent)
        {
            _parent = parent;
            _initNumber = initNumber;
            _prefab = prefab;
            InitPool();
        }

        private void InitPool()
        {
            _poolObjects = new List<GameObject>();
            for (int i = 0; i < _initNumber; i++)
            {
                _poolObjects.Add(InitObjectInstance());
            }
        }
        private GameObject InitObjectInstance()
        {
            GameObject instance = Instantiate(_prefab);
            instance.transform.SetParent(_parent.transform);
            instance.SetActive(false);
            return instance;
        }
        public GameObject GetInstance()
        {
            foreach (GameObject i in _poolObjects)
                if (i.gameObject.activeSelf == false)
                    return i;
            GameObject go = InitObjectInstance();
            _poolObjects.Add(go);
            return go;
        }
        public void ReturnPool(GameObject gameObject)
        {
            gameObject.gameObject.SetActive(false);
        }
    }
}
