using CustomInspector;
using UnityEngine;

namespace Common.Scripts.Camera
{
    public class ScreenCameraOrthographicHandle : MonoBehaviour
    {
        [Button(nameof(Start))]
        public SpriteRenderer rink;

        // Use this for initialization
        private void Start()
        {
            UnityEngine.Camera.main.orthographicSize = rink.bounds.size.x / (Screen.width / (float)Screen.height) / 2;
        }
    }
}
