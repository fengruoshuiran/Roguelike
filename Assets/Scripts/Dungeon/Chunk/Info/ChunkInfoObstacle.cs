namespace Ruoran.Roguelike.Dungeon
{
    public class ChunkInfoObstacle : ChunkInfo
    {
        public ChunkInfoObstacle(int _x = 0, int _y = 0) : base(_x, _y)
        {
            ChunkType = "Obstacle";
        }
    }
}
