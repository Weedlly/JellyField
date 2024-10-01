using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using System;
using UnityEngine;
using UnityScreenNavigator.Runtime.Core.Shared;

namespace Common.Scripts.Navigator
{
    [CreateAssetMenu(menuName = "Screen Navigator/Simple Transition")]
    public class SimpleTransition : TransitionAnimationObject
    {
        [SerializeField] private float _delay;
        [SerializeField] private float _duration = 0.3f;
        [SerializeField] private bool _isJoin;

        [SerializeField] private Ease _easeScaleType = Ease.OutQuart;
        [SerializeField] private Ease _easeAlphaType = Ease.OutQuart;
        [SerializeField] private SheetAlignment _beforeAlignment = SheetAlignment.Center;
        [SerializeField] private SheetAlignment _afterAlignment = SheetAlignment.Center;
        [SerializeField] private Vector3 _beforeScale = Vector3.one;
        [SerializeField] private Vector3 _afterScale = Vector3.one;
        [SerializeField] private float _beforeAlpha = 1.0f;
        [SerializeField] private float _afterAlpha = 1.0f;

        [SerializeField] private AnimationCurve _animationScaleCurve;
        [SerializeField] private bool _isUseAnimationScaleCurve;
        [SerializeField] private bool _isUseAnimationAlphaCurve;
        [SerializeField] private AnimationCurve _animationAlphaCurve;

        private Vector3 _afterPosition;
        private Vector3 _beforePosition;
        private CanvasGroup _canvasGroup;

        private Sequence _sequence;

        public override float Duration { get; }

        public override void Setup()
        {
            _sequence = DOTween.Sequence();
            _beforePosition = SheetAlignmentToPosition(_beforeAlignment, RectTransform);
            _afterPosition = SheetAlignmentToPosition(_afterAlignment, RectTransform);
            if (!RectTransform.gameObject.TryGetComponent(out CanvasGroup canvasGroup))
            {
                canvasGroup = RectTransform.gameObject.AddComponent<CanvasGroup>();
            }

            _canvasGroup = canvasGroup;
        }

        public override async void SetTime(float time)
        {
            Debug.Log("SetTime");
            TweenerCore<Vector3, Vector3, VectorOptions> scaleTweener;
            if (_isUseAnimationScaleCurve)
            {
                Tween customScaleTween = DOVirtual.Float(0f, 1f, _duration, value =>
                {
                    RectTransform.localScale = new Vector3(value, value, value);
                }).SetEase(_animationScaleCurve);
                _ = _sequence.Join(customScaleTween);
            }
            else
            {
                Debug.Log("DOScale");
                scaleTweener = RectTransform.DOScale(_afterScale, _duration).SetDelay(_delay).SetEase(_easeScaleType)
                    .From(_beforeScale);
                _ = _sequence.Join(scaleTweener);
            }

            TweenerCore<float, float, FloatOptions> fadeTweener;
            if (_isUseAnimationAlphaCurve)
            {
                Tween customFadeTween = DOVirtual.Float(0f, 1f, _duration, value =>
                {
                    _canvasGroup.alpha = value;
                }).SetEase(_animationAlphaCurve);
                _ = _sequence.Join(customFadeTween);
            }
            else
            {
                Debug.Log("DOFade");
                fadeTweener = _canvasGroup.DOFade(_afterAlpha, _duration).SetDelay(_delay).SetEase(_easeAlphaType)
                    .From(_beforeAlpha);
                _ = _sequence.Join(fadeTweener);
            }

            if (!_isJoin)
            {
                await _sequence.AsyncWaitForCompletion();
            }
        }

        private static Vector3 SheetAlignmentToPosition(SheetAlignment self, RectTransform rectTransform)
        {
            Rect rect = rectTransform.rect;
            float width = rect.width;
            float height = rect.height;
            float z = rectTransform.localPosition.z;
            Vector3 position = self switch
            {
                SheetAlignment.Left => new Vector3(-width, 0, z),
                SheetAlignment.Top => new Vector3(0, height, z),
                SheetAlignment.Right => new Vector3(width, 0, z),
                SheetAlignment.Bottom => new Vector3(0, -height, z),
                SheetAlignment.Center => new Vector3(0, 0, z),
                _ => throw new ArgumentOutOfRangeException(nameof(self), self, null),
            };

            return position;
        }
    }
}
