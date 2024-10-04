using GamePlay.TileData;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GamePlay.Board
{
    [Serializable]
    public class BlockMatching
    {
        public SingleBlock[,] TwoDSingleBlocks;
        public SingleTile[,] TwoDSingleTileVales;
        public int MaxCol;
        public int MaxRow;
        public BlockMatching(SingleBlock[,] twoDSingleBlocks, SingleTile[,] twoDSingleTileVales, int maxCol, int maxRow)
        {
            TwoDSingleBlocks = twoDSingleBlocks;
            TwoDSingleTileVales = twoDSingleTileVales;
            MaxCol = maxCol;
            MaxRow = maxRow;
        }
        private bool IsAvailableIndex(int x, int y, int maxCol, int maxRow)
        {
            return x >= 0 && x < maxCol && y >= 0 && y < maxRow;
        }

        public void AddToMatchingDict(int tileVal, List<EDirection> eDirectionMatches)
        {
            if (_dictionaryMatching.ContainsKey(tileVal))
            {
                foreach (var eDirection in eDirectionMatches)
                {
                    if (!_dictionaryMatching[tileVal].Contains(eDirection))
                    {
                        _dictionaryMatching[tileVal].Add(eDirection);
                    }
                }
            }
            else
            {
                _dictionaryMatching[tileVal] = eDirectionMatches;
            }
        }
        public void RunBfsAlgorithm(SingleBlock singleBlock)
        {
            _affectedTiles = new List<SingleTile>();
            _dictionaryMatching = new Dictionary<int, List<EDirection>>();
            
            FindSingleTileMatching(singleBlock.LeftTopSingleTile, singleBlock.idx);
            FindSingleTileMatching(singleBlock.RightTopSingleTile, singleBlock.idx);
            FindSingleTileMatching(singleBlock.LeftBottmSingleTile, singleBlock.idx);
            FindSingleTileMatching(singleBlock.RightBottomSingleTile, singleBlock.idx);

            SetTileDataAfterMatching();
            Debug.Log("_affectedTiles " + _affectedTiles.Count);
            Debug.Log("_dictionaryMatching" + _dictionaryMatching.Count);
        }
        private void SetTileDataAfterMatching()
        {
            foreach (var affectedTile in _affectedTiles)
            {
                affectedTile.SetTileData(0);
            }
        }
        private void PlayMatching()
        {
            
            foreach (var affectedTile in _affectedTiles)
            {
                
            }
        }
        private Dictionary<int, List<EDirection>> _dictionaryMatching;
        private List<SingleTile> _affectedTiles;
        public void FindSingleTileMatching(SingleTile searchingTile, int[] searchingBlockId)
        {
            int[] idX = { 1, 0, -1, 0 };
            int[] idY = { 0, 1, 0, -1 };
            bool[,] visited2DArr = new bool[MaxCol * 2, MaxRow * 2];
            List<SingleTile> affectTile = new List<SingleTile>();
            Queue<SingleTile> queue = new Queue<SingleTile>();
            Dictionary<EDirection, bool> matchingDict = new Dictionary<EDirection, bool>
            {
                { EDirection.Left, false },
                { EDirection.Top, false },
                { EDirection.Right, false },
                { EDirection.Bottom, false },
            };
            affectTile.Add(searchingTile);
            queue.Enqueue(searchingTile);
            while (queue.Count != 0)
            {
                SingleTile singleTile = queue.Peek();
                queue.Dequeue();
                int[] curTileIdx = singleTile.TileIdx;
                int curVal = singleTile.CurTileVal;
                for (int i = 0; i < 4; i++)
                {
                    int visitedIdxX = curTileIdx[0] + idX[i];
                    int visitedIdxY = curTileIdx[1] + idY[i];
                    if (!IsAvailableIndex(visitedIdxX, visitedIdxY, MaxCol * 2, MaxRow * 2))
                    {
                        continue;
                    }
                    SingleTile visitedSingleTile = TwoDSingleTileVales[visitedIdxX, visitedIdxY];
                    if (!visitedSingleTile || visited2DArr[visitedIdxX, visitedIdxY]
                                           || curVal != visitedSingleTile.CurTileVal)
                        continue;
                    if (IsTileBelongAdjustBlock(searchingBlockId, visitedSingleTile.BlockIdx))
                    {
                        EDirection eDirection = FindBlockDirection(searchingBlockId, visitedSingleTile.BlockIdx);
                        matchingDict[eDirection] = true;
                    }
                    if (!affectTile.Contains(visitedSingleTile))
                        affectTile.Add(visitedSingleTile);
                    queue.Enqueue(visitedSingleTile);
                    visited2DArr[visitedIdxX, visitedIdxY] = true;
                }
            }
            List<EDirection> matchingList = new List<EDirection>();
            
            foreach (var isMatch in matchingDict)
            {
                if (isMatch.Value)
                {
                    matchingList.Add(isMatch.Key);
                }
            }

            if (matchingList.Count != 0)
            {
                foreach (var tile in affectTile)
                {
                    _affectedTiles.Add(tile);
                }
                AddToMatchingDict(searchingTile.CurTileVal, matchingList);
            }
        }
        public enum EDirection
        {
            Left = 0,
            Top = 1,
            Right = 2,
            Bottom = 3,
        }

        private EDirection FindBlockDirection(int[] searchingBlockIdx, int[] curBlockIdx)
        {
            if (searchingBlockIdx[0] + 1 == curBlockIdx[0] && searchingBlockIdx[1] == curBlockIdx[1])
            {
                return EDirection.Left;
            }
            if (searchingBlockIdx[0] == curBlockIdx[0] && searchingBlockIdx[1] + 1 == curBlockIdx[1])
            {
                return EDirection.Top;
            }
            if (searchingBlockIdx[0] - 1 == curBlockIdx[0] && searchingBlockIdx[1] == curBlockIdx[1])
            {
                return EDirection.Right;
            }
            return EDirection.Bottom;
        }
        private bool IsTileBelongAdjustBlock(int[] searchingBlockIdx, int[] curBlockIdx)
        {
            return searchingBlockIdx[0] != curBlockIdx[0] || searchingBlockIdx[1] != curBlockIdx[1];
        }
    }
}
