namespace Ruoran.Roguelike.Dungeon
{
    public class ChunkInfo
    {
        public string ChunkType { get; set; } = "None";
        public int X { get; set; } = 0;
        public int Y { get; set; } = 0;
        
        public ChunkInfo(int _x, int _y)
        {
            X = _x;
            Y = _y;
        }
    }
}
