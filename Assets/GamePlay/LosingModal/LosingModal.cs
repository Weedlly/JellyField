using Common.Scripts.Data.DataAsset;
using Common.Scripts.Navigator;
using GamePlay.Board;
using SuperMaxim.Messaging;
using UnityEngine;
using UnityEngine.UI;
using UnityScreenNavigator.Runtime.Core.Modal;

namespace GamePlay.LosingModal
{
    public class LosingModal : Modal
    {
        [SerializeField] private Button _button;
        private void Start()
        {
            _button.onClick.AddListener(ResetLevel);
        }
        private void ResetLevel()
        {
            Messenger.Default.Publish(new ResetGamePayload());
            NavigatorController.MainModalContainer.Pop(false);
        }
    }
}
