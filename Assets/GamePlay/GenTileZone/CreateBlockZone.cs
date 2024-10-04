using Common.Scripts;
using Common.Scripts.Utilities;
using GamePlay.Board;
using GamePlay.TileData;
using System.Collections.Generic;
using UnityEngine;

namespace GamePlay.GenTileZone
{
    public class CreateBlockZone : SingletonBase<CreateBlockZone>, IGameSystemCommand
    {
        [SerializeField] private DragBlock _leftDragBlock;
        [SerializeField] private DragBlock _rightDragBlock;
        
        [SerializeField] private SingleBlock _leftBlock;
        [SerializeField] private SingleBlock _rightBlock;

        private List<int> _genRandomTiles;
        private void Start()
        {
            _leftDragBlock.OnPutOnBoard += OnPutOnBoard;
            _rightDragBlock.OnPutOnBoard += OnPutOnBoard;

            OnSetup();
        }
        public void OnSetup()
        {
            _genRandomTiles = new List<int>
            {
                (int)ETileId.Blue,
                (int)ETileId.Green,
                (int)ETileId.Yellow,
                (int)ETileId.Purple,
                (int)ETileId.Red,
            };

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
        public void HideBlocks(bool isHiding)
        {
            _leftBlock.gameObject.SetActive(!isHiding);
            _rightBlock.gameObject.SetActive(!isHiding);
        }
        public void OnResetGame(ResetGamePayload resetGamePayload)
        {
            HideBlocks(false);
            OnSetup();
        }
        public void OnLevelUpPayload(LevelUpPayload levelUpPayload)
        {
            HideBlocks(false);
            OnSetup();
        }
    }
}
