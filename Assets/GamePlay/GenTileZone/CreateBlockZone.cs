using Common.Scripts;
using Common.Scripts.Utilities;
using GamePlay.TileData;
using System.Collections.Generic;
using UnityEngine;

namespace GamePlay.GenTileZone
{
    public class CreateBlockZone : SingletonBase<CreateBlockZone>
    {
        [SerializeField] private DragBlock _leftDragBlock;
        [SerializeField] private DragBlock _rightDragBlock;
        
        [SerializeField] private SingleBlock _leftBlock;
        [SerializeField] private SingleBlock _rightBlock;

        private List<int> _genRandomTiles;
        private void Start()
        {
            _genRandomTiles = new List<int>
            {
                (int)ETileId.Blue,
                (int)ETileId.Green,
                (int)ETileId.Yellow,
                (int)ETileId.Purple,
                (int)ETileId.Red,
            };
            _leftDragBlock.OnPutOnBoard += OnPutOnBoard;
            _rightDragBlock.OnPutOnBoard += OnPutOnBoard;

            _leftBlock.IsEmpty = true;
            _rightBlock.IsEmpty = true;
            GenBlocks();
        }
        protected void OnDestroy()
        {
            base.OnDestroy();
            _leftDragBlock.OnPutOnBoard -= OnPutOnBoard;
            _rightDragBlock.OnPutOnBoard -= OnPutOnBoard;
        }
        private double GetWeight(int i)
        {
            return 1;
        }
        private void OnPutOnBoard()
        {
            GenBlocks();
        }
        public void GenBlocks()
        {
            if (_leftBlock.IsEmpty)
            {
                _leftBlock.SetBlockData(3,1,3,1);
                // _leftBlock.SetBlockData(
                //     RouletteWheelSelection<int>.Selection(_genRandomTiles, GetWeight),
                //     RouletteWheelSelection<int>.Selection(_genRandomTiles, GetWeight),
                //     RouletteWheelSelection<int>.Selection(_genRandomTiles, GetWeight),
                //     RouletteWheelSelection<int>.Selection(_genRandomTiles, GetWeight));
                _leftDragBlock.SingleBlock = _leftBlock;
            }
            if (_rightBlock.IsEmpty)
            {
                _rightBlock.SetBlockData(
                    RouletteWheelSelection<int>.Selection(_genRandomTiles, GetWeight),
                    RouletteWheelSelection<int>.Selection(_genRandomTiles, GetWeight),
                    RouletteWheelSelection<int>.Selection(_genRandomTiles, GetWeight),
                    RouletteWheelSelection<int>.Selection(_genRandomTiles, GetWeight));
                _rightDragBlock.SingleBlock = _rightBlock;
            }
        }
    }
}
