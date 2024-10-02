using System;
using System.Collections.Generic;

namespace GamePlay.LevelDesign
{
    [Serializable]
    public struct RowBlockArr
    {
        public Block[] Blocks;
    }
    [Serializable]
    public class Block
    {
        public bool IsEmpty;
        public bool NotExist;
        public int TopLeft;
        public int TopRight;
        public int BottomLeft;
        public int BottomRight;

        public Block(int topLeft, int topRight, int bottomLeft, int bottomRight)
        {
            TopLeft = topLeft;
            TopRight = topRight;
            BottomLeft = bottomLeft;
            BottomRight = bottomRight;
        }
    }
    [Serializable]
    public class SingeLevelDesign
    {
        public int MaxRow;
        public int MaxCol;
        public List<RowBlockArr> BlockArrConfig = new List<RowBlockArr>();
        public List<Block> GetAllBlock()
        {
            List<Block> blocks = new List<Block>();
            foreach (RowBlockArr rowBlockArr in BlockArrConfig)
            {
                foreach (Block block in rowBlockArr.Blocks)
                {
                    blocks.Add(block);
                }
            }
            return blocks;
        }
    }
}
