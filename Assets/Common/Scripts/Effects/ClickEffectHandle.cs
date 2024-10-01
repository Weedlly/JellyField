using UnityEngine;

namespace Common.Scripts.Effects
{
    public class ClickEffectHandle : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _clickingEffect;
        private UnityEngine.Camera _camera;
        private bool _isOnDown;
        private void Update()
        {
            switch (Input.anyKeyDown)
            {
                case true when _isOnDown == false:
                    _isOnDown = true;
                    OnPlayClickEffect();
                    break;
                case false:
                    _isOnDown = false;
                    break;
            }
        }
        private void OnPlayClickEffect()
        {
            Vector3 screenPos = Input.mousePosition;
            if (_camera == null)
                _camera = UnityEngine.Camera.main;
            Vector3 worldPos = _camera.ScreenToWorldPoint(screenPos);
            _clickingEffect.transform.position = worldPos;
            _clickingEffect.Play();
        }
    }
}
