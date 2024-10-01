using UnityEngine;

namespace Common.Scripts.EventAggregator
{
    public abstract class MonoBehaviourEventAggregator : MonoBehaviour, IEventAggregator
    {
        protected virtual void Awake() => OnSubscribeEvents();
        protected virtual void OnDestroy() => OnUnSubscribeEvents();

        public abstract void OnSubscribeEvents();
        public abstract void OnUnSubscribeEvents();
    }
}
