using System.Collections.Generic;
using UnityEngine;

namespace Common.Scripts.Pooler
{
    public class Pool<T> : MonoBehaviour where T : MonoBehaviour
    {
        public GameObject parent;
        public int initNumber;
        public T prefab;
        public List<T> poolObjects;
        public void InitPool()
        {
            poolObjects = new List<T>();
            for (int i = 0; i < initNumber; i++)
            {
                T instance = Instantiate(prefab);
                poolObjects.Add(instance);
                instance.gameObject.transform.SetParent(parent.transform);
                instance.gameObject.SetActive(false);
            }
        }
        public T GetInstance()
        {
            foreach (T i in poolObjects)
            {
                if (i.gameObject.activeSelf == false)
                {
                    return i;
                }
            }
            return null;
        }
        public void ReturnPool(T gameObject)
        {
            gameObject.gameObject.SetActive(false);
        }
    }
}
