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
        private List<TileGoalView> _activeGoalView;
        [SerializeField] private LevelDesignDataConfig _levelDesignDataConfig;
        [SerializeField] private TileDataConfig _tileDataConfig;
        [SerializeField] private UserDataAsset _userDataAsset;
        public Dictionary<int, int> CurTileGoalDict;
        private List<TileGoalConfig> _tileGoalConfigs;

        private void Start()
        {
            SetupView();
        }
        public void Scoring(int tileVal, int matchCount)
        {
            if (CurTileGoalDict.ContainsKey(tileVal))
            {
                CurTileGoalDict[tileVal] -= matchCount;
                if (CurTileGoalDict[tileVal] < 0)
                {
                    CurTileGoalDict[tileVal] = 0;
                }
            }
           
            UpdateView();
            
            IsWinGoal();
        }
        public bool IsWinGoal()
        {
            foreach (var goal in CurTileGoalDict)
            {
                if (goal.Value != 0)
                    return false;
            }
            return true;
        }
        private void UpdateView()
        {
            int viewIdx = 0;
            foreach (var tileGoal in CurTileGoalDict)
            {
                _activeGoalView[viewIdx].UpdateTileAmount(tileGoal.Value);
            }
        }
        private void SetupView()
        {
            _activeGoalView = new List<TileGoalView>();
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
                _activeGoalView.Add(_tileGoalViews[i]);
            }
            int viewIdx = 0;
            foreach (var tileGoal in CurTileGoalDict)
            {
                TileData.TileData tileData = _tileDataConfig.GeConfigByKey(tileGoal.Key);
                _activeGoalView[viewIdx++].Setup(tileData.Color,tileGoal.Value);
            }
        }
    }
}
