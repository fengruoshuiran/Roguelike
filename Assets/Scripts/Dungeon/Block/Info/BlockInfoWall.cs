namespace Ruoran.Roguelike.Dungeon
{
    public class BlockInfoWall : BlockInfoUnpass
    {
        public BlockInfoWall(int _x, int _y) : base(_x, _y)
        {
            BlockType = "Wall";
        }
    }
}