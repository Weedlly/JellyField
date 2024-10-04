using UnityEngine;
using UnityEngine.UI;

namespace GamePlay.TileData
{
    public class SingleBlock : MonoBehaviour
    {
        [SerializeField] private Image _blockBg;
        [SerializeField] private Image _imgHover;
        public int[] idx;
        public void SetBlockId(int[] blockId)
        {
            idx = blockId;
            LeftTopSingleTile.BlockIdx = idx;
            RightTopSingleTile.BlockIdx = idx;
            LeftBottmSingleTile.BlockIdx = idx;
            RightBottomSingleTile.BlockIdx = idx;
        }
        public SingleTile LeftTopSingleTile;
        public SingleTile RightTopSingleTile;
        public SingleTile LeftBottmSingleTile;
        public SingleTile RightBottomSingleTile;
        // public int[] BlockTileVales;
        public bool IsEmpty; 
        public void SetBlockData(int lt, int rt, int lb, int rb)
        {
            IsEmpty = lt + rt + lb + rb == 0;
            // BlockTileVales = new[] { lt, rt, lb, rb };
            LeftTopSingleTile.SetTileData(lt);
            RightTopSingleTile.SetTileData(rt);
            LeftBottmSingleTile.SetTileData(lb);
            RightBottomSingleTile.SetTileData(rb);
        }
        public void ReCheckEmpty()
        {
            IsEmpty = LeftTopSingleTile.CurTileVal 
                      + RightTopSingleTile.CurTileVal
                      + LeftBottmSingleTile.CurTileVal
                      + RightBottomSingleTile.CurTileVal== 0; 
        }
        public void SetShowing(bool isShowing)
        {
            _blockBg.enabled = isShowing;
        }
        public void SetHover(bool isHover)
        {
            if (_imgHover)
                _imgHover.enabled = isHover;
        }
        public void ResetBlock()
        {
            // BlockTileVales = new[] { 0, 0, 0, 0};
            IsEmpty = true;
            LeftTopSingleTile.SetTileData(0);
            RightTopSingleTile.SetTileData(0);
            LeftBottmSingleTile.SetTileData(0);
            RightBottomSingleTile.SetTileData(0);
            SetHover(false);
        }
    }
}
