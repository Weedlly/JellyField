using Common.Scripts.EventAggregator;
using UnityEngine;

namespace Common.Scripts.MVVM
{
    public abstract class ViewModelBase : MonoBehaviour, IEventAggregator
    {
        protected virtual void Awake()
        {
            OnSubscribeEvents();
        }
        protected virtual void OnDestroy()
        {
            OnUnSubscribeEvents();
        }

        public abstract void OnSubscribeEvents();
        public abstract void OnUnSubscribeEvents();
    }
}
