namespace Ruoran.Roguelike.Dungeon
{
    public class ChunkInfoCross : ChunkInfo
    {
        public bool Up { get; set; } = false;
        public bool Down { get; set; } = false;
        public bool Left { get; set; } = false;
        public bool Right { get; set; } = false;

        public ChunkInfoCross(int _x = 0, int _y = 0) : base(_x, _y)
        {
            ChunkType = "Cross";
        }
    }
}
