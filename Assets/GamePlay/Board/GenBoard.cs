using Common.Scripts.Data.DataAsset;
using GamePlay.LevelDesign;
using GamePlay.TileData;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GamePlay.Board
{
    public class GenBoard : MonoBehaviour
    {
        [SerializeField] private GridLayoutGroup _gridLayoutGroup;
        [SerializeField] private List<SingleBlock> _defaultSingleBlocks;
        [SerializeField] private LevelDesignDataConfig _levelDesignConfig;
        [SerializeField] private UserDataAsset _userDataAsset;
        private List<SingleBlock> _activeBlocks;
        private SingleBlock[,] _2dSingleBlocks;
        private SingleTile[,] _2dSingleTiles;

        private SingeLevelDesign _curLevelDesign;
        private int _maxVirtualBlock;
        private void Start()
        {
            _curLevelDesign = _levelDesignConfig.GeConfigByKey(_userDataAsset.CurLevel);
            _activeBlocks = new List<SingleBlock>();
            OnGenBoard();
        }
        private void OnGenBoard()
        {
            _gridLayoutGroup.constraintCount = _curLevelDesign.MaxCol;
            _maxVirtualBlock = _curLevelDesign.MaxCol * _curLevelDesign.MaxRow;

            _2dSingleBlocks = new SingleBlock[_curLevelDesign.MaxCol, _curLevelDesign.MaxRow];
            _2dSingleTiles = new SingleTile[_curLevelDesign.MaxCol * 2, _curLevelDesign.MaxRow * 2];

            RenderBlocksByColAndRow();
            RenderTileData();
            BoardManager.Instance.ActiveBlocks = _activeBlocks;
            BoardManager.Instance.TwoDSingleBlocks = _2dSingleBlocks;
            BoardManager.Instance.TwoDSingleTiles = _2dSingleTiles;

            BoardManager.Instance.MaxBlockCol = _curLevelDesign.MaxCol;
            BoardManager.Instance.MaxBlockRow = _curLevelDesign.MaxRow;
        }
        private void RenderBlocksByColAndRow()
        {
            for (int i = 0; i < _defaultSingleBlocks.Count; i++)
            {
                bool isShowing = i < _maxVirtualBlock;
                _defaultSingleBlocks[i].gameObject.SetActive(isShowing);
            }
        }
        private void RenderTileData()
        {
            List<Block> blocks = _curLevelDesign.GetAllBlock();
            int x = -1;
            int y = 0;
            for (int i = 0; i < _maxVirtualBlock; i++)
            {
                if (x < _curLevelDesign.MaxCol)
                {
                    x++;
                }
                if (x >= _curLevelDesign.MaxCol)
                {
                    x = 0;
                    y += 1;
                }

                if (i < blocks.Count)
                {

                    Block block = blocks[i];
                    if (block.NotExist)
                    {
                        _2dSingleTiles[x, y * 2] = null;
                        _2dSingleTiles[x + 1, y * 2] = null;
                        _2dSingleTiles[x, y * 2 + 1] = null;
                        _2dSingleTiles[x + 1, y * 2 + 1] = null;
                        _2dSingleBlocks[x, y] = null;
                        _defaultSingleBlocks[i].SetShowing(false);
                        continue;
                    }
                    _activeBlocks.Add(_defaultSingleBlocks[i]);
                    _defaultSingleBlocks[i].SetShowing(true);
                    _defaultSingleBlocks[i].SetBlockData(block.TopLeft, block.TopRight, block.BottomLeft, block.BottomRight);
                }
                else
                {
                    _activeBlocks.Add(_defaultSingleBlocks[i]);
                    _defaultSingleBlocks[i].ResetBlock();
                }
                
                _2dSingleBlocks[x, y] = _defaultSingleBlocks[i];
                _2dSingleBlocks[x, y].SetBlockId( new[] { x, y });
                
                _2dSingleTiles[x * 2, y * 2] = _defaultSingleBlocks[i].LeftTopSingleTile;
                _2dSingleTiles[x * 2, y * 2].TileIdx = new[] { x * 2, y * 2 };
                
                _2dSingleTiles[x * 2 + 1, y * 2] = _defaultSingleBlocks[i].RightTopSingleTile;
                _2dSingleTiles[x * 2 + 1, y * 2].TileIdx = new[] {x * 2 + 1, y * 2 };
                
                _2dSingleTiles[x * 2, y * 2 + 1] = _defaultSingleBlocks[i].LeftBottmSingleTile;
                _2dSingleTiles[x * 2, y * 2 + 1].TileIdx = new[] {x * 2, y * 2 + 1};
                
                _2dSingleTiles[x * 2 + 1, y * 2 + 1] = _defaultSingleBlocks[i].RightBottomSingleTile;
                _2dSingleTiles[x * 2 + 1, y * 2 + 1].TileIdx = new[] {x * 2 + 1, y * 2 + 1};
            }
        }
    }
}
