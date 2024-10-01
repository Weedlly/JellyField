using AYellowpaper.SerializedCollections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Common.Scripts.Data.DataConfig
{
    public abstract class DataConfigBase<TKey, TVal> : ScriptableObject
    {
        [SerializeField] [SerializedDictionary("TKey", "TVal")]
        protected SerializedDictionary<TKey, TVal> _data;

        public TVal GeConfigByKey(TKey keyId)
        {
            _data.TryGetValue(keyId, out TVal unitDataComposite);
            return unitDataComposite;
        }
        public bool IsExist(TKey keyId)
        {
            return _data.ContainsKey(keyId);
        }
        public List<TVal> ToList()
        {
            return _data.Values.ToList();
        }
    }
}
