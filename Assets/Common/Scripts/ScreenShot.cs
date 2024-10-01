using UnityEngine;

#if UNITY_EDITOR
namespace Common.Scripts
{
    public class ScreenShot : MonoBehaviour
    {
        public KeyCode ScreenShotButton;
        private int _index;
        private void Update()
        {
            if (Input.GetKeyDown(ScreenShotButton))
            {
                ScreenCapture.CaptureScreenshot($"screenshot{_index++}.png");
                Debug.Log("A screenshot was taken!");
            }
        }
    }
}
#endif
