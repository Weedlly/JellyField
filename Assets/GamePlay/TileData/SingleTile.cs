using System.Collections.Generic;
using UnityEngine;

namespace GamePlay.TileData
{
    public class SingleTile : MonoBehaviour
    {
        public int[] BlockIdx;
        public int[] TileIdx;
        public GameObject Tile;
        public TileDataConfig _tileDataConfig;
        public MeshRenderer MeshRenderer;
        public int CurTileVal;
        public void SetTileData(int tileVal)
        {
            CurTileVal = tileVal;
            if (CurTileVal == 0)
            {
                Tile.SetActive(false);
                return;
            }
            Tile.SetActive(true);
            MeshRenderer.SetMaterials(new List<Material>
            {
                _tileDataConfig.GeConfigByKey(tileVal).Material,
            });
        }
    }
}
