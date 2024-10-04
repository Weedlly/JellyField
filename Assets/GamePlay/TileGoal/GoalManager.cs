using Common.Scripts;
using Common.Scripts.Data.DataAsset;
using Common.Scripts.Navigator;
using GamePlay.Board;
using GamePlay.LevelDesign;
using GamePlay.TileData;
using SuperMaxim.Messaging;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GamePlay.TileGoal
{
    public class GoalManager : SingletonBase<GoalManager>,IGameSystemCommand
    {
        [SerializeField] private List<TileGoalView> _tileGoalViews;
        [SerializeField] private TextMeshProUGUI _textMeshPro;
        private List<TileGoalView> _activeGoalView;
        [SerializeField] private LevelDesignDataConfig _levelDesignDataConfig;
        [SerializeField] private TileDataConfig _tileDataConfig;
        [SerializeField] private UserDataAsset _userDataAsset;
        [SerializeField] private Button _buttonResetLevel;
        public Dictionary<int, int> CurTileGoalDict;
        private List<TileGoalConfig> _tileGoalConfigs;
        private bool _isWin;
        private void Start()
        {
            _isWin = false;
            SetupView();
            _buttonResetLevel.onClick.AddListener(OnResetLevel);
            
            Messenger.Default.Subscribe<ResetGamePayload>(OnResetGame);
            Messenger.Default.Subscribe<LevelUpPayload>(OnLevelUpPayload);
        }
        private void OnResetLevel()
        {
            Messenger.Default.Publish(new ResetGamePayload());
        }
        private void OnDisable()
        {
            Messenger.Default.Subscribe<ResetGamePayload>(OnResetGame);
            Messenger.Default.Subscribe<LevelUpPayload>(OnLevelUpPayload);
        }
        private void ShowLevelUpModal()
        {
            NavigatorController.MainModalContainer.Push(ResourceKey.Prefabs.LevelUpModal,false);
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

            if (!_isWin && IsWinGoal())
            {
                _isWin = true;
                ShowLevelUpModal();
            }
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
                _activeGoalView[viewIdx++].UpdateTileAmount(tileGoal.Value);
            }
        }
        private void SetupView()
        {
            _activeGoalView = new List<TileGoalView>();
            _tileGoalConfigs  = _levelDesignDataConfig.GeConfigByKey(_userDataAsset.CurLevel).TileGoalConfigs;
            _textMeshPro.text = $"LEVEL {_userDataAsset.CurLevel}";
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
        public void OnResetGame(ResetGamePayload resetGamePayload)
        {
            _isWin = false;
            SetupView();
        }
        public void OnLevelUpPayload(LevelUpPayload levelUpPayload)
        {
            _isWin = false;
            SetupView();
        }
    }
}
