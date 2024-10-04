using System;
using UnityEngine;

namespace Common.Scripts.Data.DataAsset
{
    
    [Serializable]
    public struct UserDataModel : IDefaultDataModel
    {
        public int CurLevel;
        public bool IsEmpty()
        {
            return false;
        }
        public void SetDefault()
        {
            CurLevel = 1;
        }
    }

    [CreateAssetMenu(fileName = "UserDataAsset", menuName = "ScriptableObject/DataAsset/UserDataAsset")]
    public class UserDataAsset : LocalDataAsset<UserDataModel>
    {
        public int CurLevel
        {
            get
            {
                return _model.CurLevel;
            }
            set
            {
                _model.CurLevel = value;
            }
        }
        
    }
}
