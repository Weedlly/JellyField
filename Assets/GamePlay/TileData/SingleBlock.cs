using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GamePlay.TileData
{
    public class SingleBlock : MonoBehaviour
    {
        [SerializeField] private Image _blockBg;
        public SingleTile LeftTopSingleTile;
        public SingleTile RightTopSingleTile;
        public SingleTile LeftBottmSingleTile;
        public SingleTile RightBottomSingleTile;

        public void SetBlockData(int lt, int rt, int lb, int rb)
        {
            LeftTopSingleTile.SetTileData(lt);
            RightTopSingleTile.SetTileData(rt);
            LeftBottmSingleTile.SetTileData(lb);
            RightBottomSingleTile.SetTileData(rb);
        }
        public void SetShowing(bool isShowing)
        {
            _blockBg.enabled = isShowing;
        }
        public void ResetBlock()
        {
            LeftTopSingleTile.SetTileData(0);
            RightTopSingleTile.SetTileData(0);
            LeftBottmSingleTile.SetTileData(0);
            RightBottomSingleTile.SetTileData(0);
        }
    }
}
