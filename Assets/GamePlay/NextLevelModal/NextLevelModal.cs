using Common.Scripts.Data.DataAsset;
using Common.Scripts.Navigator;
using GamePlay.Board;
using GamePlay.TileGoal;
using SuperMaxim.Messaging;
using UnityEngine;
using UnityEngine.UI;
using UnityScreenNavigator.Runtime.Core.Modal;

namespace GamePlay.NextLevelModal
{
    public class NextLevelModal : Modal
    {
        [SerializeField] private UserDataAsset _userDataAsset;
        [SerializeField] private Button _button;
        private void Start()
        {
            _button.onClick.AddListener(NextLevel);
        }
        private void NextLevel()
        {
            _userDataAsset.CurLevel++;
            if (_userDataAsset.CurLevel > 3)
            {
                _userDataAsset.CurLevel = 1;
            }
            Messenger.Default.Publish(new LevelUpPayload());
            NavigatorController.MainModalContainer.Pop(false);
        }
    }
}
