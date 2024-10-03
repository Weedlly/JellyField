using Common.Scripts;
using GamePlay.TileData;
using System.Collections.Generic;
using UnityEngine;

namespace GamePlay.GenTileZone
{
    public class BoardManager : SingletonBase<BoardManager>
    {
        public List<SingleBlock> ActiveBlocks;
        [SerializeField] private float _minShowHover;
        private SingleBlock _preNearestBlock;

        public void ResetHover()
        {
            if (_preNearestBlock)
                _preNearestBlock.SetHover(false);
        }
        
        public void CheckHover(Vector2 CurPointerPos)
        {
            SingleBlock nearestBlock = null;
            float nearstDis = float.MaxValue;
            foreach (SingleBlock singleBlock in ActiveBlocks)
            {
                if (!singleBlock.IsEmpty)
                    continue;
                
                Vector2 blockPos = singleBlock.transform.position;
                float curDis = Vector2.Distance(CurPointerPos, blockPos);
                Debug.Log("curDis " + curDis);
                if (curDis < _minShowHover)
                {
                    nearestBlock = singleBlock;
                    break;
                }
            }
            if (!nearestBlock)
            {
                if (_preNearestBlock)
                    _preNearestBlock.SetHover(false);
            }
            else if (nearestBlock != _preNearestBlock)
            {
                if (_preNearestBlock)
                    _preNearestBlock.SetHover(false);
                _preNearestBlock = nearestBlock;
                nearestBlock.SetHover(true);
            }
        }
        
    }
}
