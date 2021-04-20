namespace Ruoran.Roguelike.Dungeon
{
    public class BlockInfoObstacle : BlockInfoUnpass
    {
        public BlockInfoObstacle(int _x, int _y) : base(_x, _y)
        {
            BlockType = "Obstacle";
        }
    }
}
