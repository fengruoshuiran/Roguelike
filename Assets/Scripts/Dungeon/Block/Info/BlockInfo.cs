namespace Ruoran.Roguelike.Dungeon
{
    public class BlockInfo
    {
        public string BlockType { get; set; } = "None";
        public int X { get; set; } = 0;
        public int Y { get; set; } = 0;
        public bool CanPass { get; set; } = true;
        
        public BlockInfo(int _x, int _y)
        {
            X = _x;
            Y = _y;
        }
    }
}
