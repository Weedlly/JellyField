namespace Common.Scripts.EventAggregator
{
    public interface IEventAggregator
    {
        public void OnSubscribeEvents();
        public void OnUnSubscribeEvents();
    }
}