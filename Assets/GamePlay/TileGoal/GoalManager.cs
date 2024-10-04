using Common.Scripts;
using Common.Scripts.Data.DataAsset;
using GamePlay.LevelDesign;
using GamePlay.TileData;
using System.Collections.Generic;
using UnityEngine;

namespace GamePlay.TileGoal
{
    public class GoalManager : SingletonBase<GoalManager>
    {
        [SerializeField] private List<TileGoalView> _tileGoalViews;
        [SerializeField] private LevelDesignDataConfig _levelDesignDataConfig;
        [SerializeField] private TileDataConfig _tileDataConfig;
        [SerializeField] private UserDataAsset _userDataAsset;
        public Dictionary<int, int> CurTileGoalDict;
        private List<TileGoalConfig> _tileGoalConfigs;

        private void Start()
        {
            SetupView();
        }
        private void SetupView()
        {
            _tileGoalConfigs  =_levelDesignDataConfig.GeConfigByKey(_userDataAsset.CurLevel).TileGoalConfigs;
            CurTileGoalDict = new Dictionary<int, int>();
            foreach (var tileGoal in _tileGoalConfigs)
            {
                CurTileGoalDict.Add(tileGoal.TileId,tileGoal.Number);
            }
            for (int i = 0; i < _tileGoalViews.Count; i++)
            {
                bool isShowView = i < _tileGoalConfigs.Count;
                _tileGoalViews[i].gameObject.SetActive(isShowView);

                if (isShowView)
                {
                    TileData.TileData tileData = _tileDataConfig.GeConfigByKey(_tileGoalConfigs[i].TileId);
                    _tileGoalViews[i].Setup(tileData.Color,_tileGoalConfigs[i].Number);
                }
            }
        }
    }
}
