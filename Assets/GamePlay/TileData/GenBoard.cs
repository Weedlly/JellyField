using GamePlay.LevelDesign;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GamePlay.TileData
{
    public class GenBoard : MonoBehaviour
    {
        [SerializeField] private GridLayoutGroup _gridLayoutGroup;
        [SerializeField] private List<SingleBlock> _defaultSingleBlocks;
        [SerializeField] private List<SingleBlock> _activeBlock;
        [SerializeField] private int _curLevel;
        [SerializeField] private LevelDesignDataConfig _levelDesignConfig;

        private SingeLevelDesign _curLevelDesign;
        private int _maxVirtualBlock;
        private void Start()
        {
            _curLevelDesign = _levelDesignConfig.GeConfigByKey(_curLevel);

            OnGenBoard();
        }
        private void OnGenBoard()
        {
            _gridLayoutGroup.constraintCount = _curLevelDesign.MaxCol;
            _maxVirtualBlock = _curLevelDesign.MaxCol * _curLevelDesign.MaxRow;

            RenderBlocksByColAndRow();
            RenderTileData();
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
            int curBlock = 0;
            List<Block> blocks = _curLevelDesign.GetAllBlock();
            for (int i = 0; i < _maxVirtualBlock; i++)
            {
                if (i < blocks.Count)
                {

                    Block block = blocks[i];
                    if (block.NotExist)
                    {
                        _defaultSingleBlocks[i].ResetBlock();
                        _defaultSingleBlocks[i].SetShowing(false);
                        continue;
                    }
                    _defaultSingleBlocks[i].SetShowing(true);
                    _defaultSingleBlocks[i].SetBlockData(block.TopLeft, block.TopRight, block.BottomLeft, block.BottomRight);
                }
                else
                {
                    _defaultSingleBlocks[i].ResetBlock();
                }
            }
        }
    }
}
