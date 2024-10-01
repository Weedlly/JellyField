using UnityScreenNavigator.Runtime.Core.Modal;
using UnityScreenNavigator.Runtime.Core.Page;

namespace Common.Scripts.Navigator
{
    public static class NavigatorController
    {
        private static PageContainer _mainPageContainer;
        private static ModalContainer _mainModalContainer;
        public static PageContainer MainPageContainer
        {
            get
            {
                if (_mainPageContainer == null)
                    _mainPageContainer = PageContainer.Find("MainPageContainer");
                return _mainPageContainer;
            }
        }
        public static ModalContainer MainModalContainer
        {
            get
            {
                if (_mainModalContainer == null)
                    _mainModalContainer = ModalContainer.Find("MainModalContainer");
                return _mainModalContainer;
            }
        }

        public static void PopPage()
        {
            MainPageContainer.Pop(true);
        }
        public static void PopModal()
        {
            MainModalContainer.Pop(true);
        }
        public static void PopAllModal()
        {
            MainModalContainer.Pop(true, MainModalContainer.OrderedModalIds.Count);
        }
    }
}
