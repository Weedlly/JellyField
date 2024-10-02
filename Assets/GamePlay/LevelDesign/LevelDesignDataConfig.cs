using Common.Scripts.Data.DataConfig;
using UnityEngine;

namespace GamePlay.LevelDesign
{
    [CreateAssetMenu(menuName = "DataConfig/LevelDesignConfig", fileName = "ScriptableObject/LevelDesignConfig")]
    public class LevelDesignDataConfig : DataConfigBase<int, SingeLevelDesign>
    {
        
    }
}
