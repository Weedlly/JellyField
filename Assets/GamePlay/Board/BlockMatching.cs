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

        // public void AddMatchingCount(Dictionary<int, TileData> matchingCounts, int key, )
        // {
        //     if (matchingCounts.ContainsKey(key))
        //         matchingCounts[key] = new TileData
        //         {
        //             IdxBlock = matchingCounts[key].IdxBlock,
        //             Val = matchingCounts[key].Val + 1,
        //             IdxTile = matchingCounts[key].IdxTile,
        //         };
        //     else
        //         matchingCounts.Add(key, new TileData
        //         {
        //             IdxBlock = matchingCounts[key].IdxBlock,
        //             Val = 1,
        //             IdxTile = matchingCounts[key].IdxTile,
        //         });
        // }
        private List<SingleTile> _affectedTiles;
        public void RunBfsAlgorithm(SingleBlock singleBlock)
        {
            _affectedTiles = new List<SingleTile>();
            
            int[] idX = { 1, 0, -1, 0 };
            int[] idY = { 0, 1, 0, -1 };
            bool[,] visited2DArr = new bool[MaxCol * 2, MaxRow * 2];

            Queue<SingleTile> queue = new Queue<SingleTile>();

            _affectedTiles.Add(singleBlock.LeftTopSingleTile);
            queue.Enqueue(singleBlock.LeftTopSingleTile);

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
                    if (visitedSingleTile && visited2DArr[visitedIdxX, visitedIdxY] == false
                        && curVal == visitedSingleTile.CurTileVal)
                    {
                        _affectedTiles.Add(visitedSingleTile);
                        queue.Enqueue(visitedSingleTile);
                        visited2DArr[visitedIdxX, visitedIdxY] = true;
                    }
                }
            }
            
            Debug.Log("_affectedTiles.Count "  +_affectedTiles.Count);
        }
    }
}
