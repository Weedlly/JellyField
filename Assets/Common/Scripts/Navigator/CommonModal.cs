using UnityEngine;
using UnityEngine.UI;
using UnityScreenNavigator.Runtime.Core.Modal;

namespace Common.Scripts.Navigator
{
    public class CommonModal : Modal
    {
        [SerializeField] private Button[] _btnCloses;
        protected virtual void Awake()
        {
            foreach (Button btn in _btnCloses)
            {
                btn.onClick.AddListener(PopModal);
            }
        }
        protected virtual void PopModal()
        {
            NavigatorController.PopModal();
        }
    }
}
