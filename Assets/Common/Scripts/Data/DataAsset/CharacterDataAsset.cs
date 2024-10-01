using UnityEngine;

namespace Common.Scripts.Data.DataAsset
{
    public enum ECharacterId : byte
    {
        None = 0,
        Panda = 1,
        
    }
    public struct CharacterDataModel : IDefaultDataModel
    {
        public ECharacterId CurSelectedCharacterId;
        public bool IsEmpty()
        {
            return false;
        }
        public void SetDefault()
        {
            CurSelectedCharacterId = ECharacterId.None;
        }
    }
    [CreateAssetMenu(fileName = "CharacterDataAsset", menuName = "ScriptableObject/DataAsset/CharacterDataAsset")]
    public class CharacterDataAsset : LocalDataAsset<CharacterDataModel>
    {
        public ECharacterId CurSelectedCharacterId
        {
            get
            {
                return _model.CurSelectedCharacterId;
            }
            set
            {
                _model.CurSelectedCharacterId = value;
            }
        }
    }
}
