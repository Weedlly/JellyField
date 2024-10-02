using Common.Scripts.Data.DataConfig;
using UnityEngine;

namespace GamePlay.TileData
{
    public enum ETileId
    {
        Blue = 1,
        Green = 2,
        Purple = 3,
        Red = 4,
        Yellow = 5,
    }
    [CreateAssetMenu(fileName = "TileDataConfig",menuName = "ScriptableObject/DataConfig/TileDataConfig")]
    public class TileDataConfig : DataConfigBase<int,TileData>
    {
        
    }
}
