using Common.Scripts;
using GamePlay.TileData;
using System.Collections.Generic;
using UnityEngine;

namespace GamePlay.Board
{
    public class BoardManager : SingletonBase<BoardManager>
    {
        public List<SingleBlock> ActiveBlocks;
        [SerializeField] private float _minShowHover;
        public SingleBlock PreNearestBlock;
        public SingleBlock[,] TwoDSingleBlocks;
        public SingleTile[,] TwoDSingleTiles;
        public int MaxBlockCol;
        public int MaxBlockRow;

        [SerializeField] private BlockMatching _blockMatching;
        public void ResetHover()
        {
            if (PreNearestBlock)
            {
                PreNearestBlock.SetHover(false);
                PreNearestBlock = null;
            }
        }
        public bool IsPutOnBoard()
        {
            if (PreNearestBlock)
            {
                return true;
            }
            return false;
        }
        public void PutOnBoard(SingleBlock singleBlock)
        {
            PreNearestBlock.SetHover(false);
            PreNearestBlock.SetShowing(true);
            // replace tile data
            PreNearestBlock.SetBlockData(
                singleBlock.LeftTopSingleTile.CurTileVal,
                singleBlock.RightTopSingleTile.CurTileVal,
                singleBlock.LeftBottmSingleTile.CurTileVal,
                singleBlock.RightBottomSingleTile.CurTileVal);
            
            _blockMatching = new BlockMatching(TwoDSingleBlocks,TwoDSingleTiles,MaxBlockCol,MaxBlockCol);
            singleBlock.LeftTopSingleTile.TileIdx = PreNearestBlock.LeftTopSingleTile.TileIdx;
            singleBlock.RightTopSingleTile.TileIdx = PreNearestBlock.RightTopSingleTile.TileIdx;
            singleBlock.LeftBottmSingleTile.TileIdx = PreNearestBlock.LeftBottmSingleTile.TileIdx;
            singleBlock.RightBottomSingleTile.TileIdx = PreNearestBlock.LeftBottmSingleTile.TileIdx;
            singleBlock.SetBlockId( PreNearestBlock.idx);
            _blockMatching.RunBfsAlgorithm(singleBlock);
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
                if (PreNearestBlock)
                {
                    PreNearestBlock.SetHover(false);
                    PreNearestBlock = null;
                }
            }
            else if (nearestBlock != PreNearestBlock)
            {
                if (PreNearestBlock)
                    PreNearestBlock.SetHover(false);
                PreNearestBlock = nearestBlock;
                nearestBlock.SetHover(true);
            }
        }
        
    }
}
