// using Cysharp.Threading.Tasks;
// using DG.Tweening;
// using System.Threading;
// using UnityEngine;
// using UnityScreenNavigator.Runtime.Core.Shared;
//
// [CreateAssetMenu(menuName = "Screen Navigator/Match Block Custom Transition")]
// public class CustomTransition : SimpleTransition
// {
//     [SerializeField] private bool _isLeftToRight;
//
//     [SerializeField] private bool _isSwipeIn;
//
//     public override async UniTask Play(CancellationToken cancellationToken) => await SetTime(cancellationToken);
//
//     public void SetupSwipeDirection(bool isLeftToRight) => _isLeftToRight = isLeftToRight;
//
//     public override void Setup()
//     {
//         // Setting up swipe left to right or right to left for alignment
//         SetUpSwipeDirectionAlignment(isSwipeIn: _isSwipeIn, isLeftToRight: _isLeftToRight);
//
//         _beforePosition = SheetAlignmentToPosition(_beforeAlignment, RectTransform);
//         _afterPosition = SheetAlignmentToPosition(_afterAlignment, RectTransform);
//
//         if (!RectTransform.gameObject.TryGetComponent<CanvasGroup>(out var canvasGroup))
//             canvasGroup = RectTransform.gameObject.AddComponent<CanvasGroup>();
//
//         _canvasGroup = canvasGroup;
//     }
//
//     private void SetUpSwipeDirectionAlignment(bool isSwipeIn, bool isLeftToRight)
//     {
//         if (isSwipeIn)
//         {
//             if (isLeftToRight)
//             {
//                 _beforeAlignment = SheetAlignment.Right;
//                 _afterAlignment = SheetAlignment.Center;
//             }
//             else
//             {
//                 _beforeAlignment = SheetAlignment.Left;
//                 _afterAlignment = SheetAlignment.Center;
//             }
//         }
//         else
//         {
//             if (isLeftToRight)
//             {
//                 _beforeAlignment = SheetAlignment.Center;
//                 _afterAlignment = SheetAlignment.Left;
//             }
//             else
//             {
//                 _beforeAlignment = SheetAlignment.Center;
//                 _afterAlignment = SheetAlignment.Right;
//             }
//         }
//     }
//     
//     protected override async UniTask SetTime(CancellationToken cancellationToken)
//     {
//         Vector3 startPos = _beforePosition;
//         Vector3 endPos = _afterPosition;
//
//         // Set the initial position to the starting position
//         RectTransform.anchoredPosition = startPos;
//
//         // Animate the position based on the swipe direction
//         var swipeTweener = RectTransform.DOAnchorPos(endPos, _duration)
//             .SetDelay(_delay)
//             .SetEase(_easeScaleType)
//             .From(startPos);
//
//         _ = _sequence.Join(swipeTweener);
//
//         if (!_isJoin) await _sequence.AwaitForComplete(cancellationToken: cancellationToken);
//     }
// }


