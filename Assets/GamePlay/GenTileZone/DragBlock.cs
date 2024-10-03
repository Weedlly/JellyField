using GamePlay.Board;
using GamePlay.TileData;
using System;
using UnityEngine;

namespace GamePlay.GenTileZone
{
    public class DragBlock : MonoBehaviour
    {
        [SerializeField] private Transform _originTf;
        [SerializeField] private GameObject _goBlock;
        public SingleBlock SingleBlock;
        public Action OnPutOnBoard;
        
        public Vector3 CurPointerPos;
        
        private bool _isOnDrag;
        private Camera _cam;
        
        void Start()
        {
            _cam = Camera.main;
        }
        private void OnMouseDrag()
        {
            Vector3 pos = _cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 20));
            Vector3 localPos = transform.InverseTransformDirection(pos);
            transform.position = new Vector3(localPos.x, localPos.y, transform.position.z);
            _goBlock.transform.position = pos;
            CurPointerPos = _goBlock.transform.position;

            BoardManager.Instance.CheckHover(new Vector2(localPos.x, localPos.y));
            _isOnDrag = true;
            Debug.Log("OnMouseDrag");
        }
        private void OnMouseUp()
        {
            
            if (_isOnDrag)
            {
                Debug.Log("OnMouseUp");
                if (BoardManager.Instance.IsPutOnBoard())
                {
                    BoardManager.Instance.PutOnBoard(SingleBlock);
                    SingleBlock.ResetBlock();
                    OnPutOnBoard?.Invoke();
                }
                transform.position = new Vector3(_originTf.position.x, _originTf.position.y, transform.position.z);
                _goBlock.transform.position = _originTf.position;
                CurPointerPos = _goBlock.transform.position;
               


                _isOnDrag = false;
                BoardManager.Instance.ResetHover();
            }
        }
    }
}
