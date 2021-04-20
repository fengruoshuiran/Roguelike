namespace Ruoran.Roguelike.Dungeon
{
    public class BlockInfoRoom : BlockInfo
    {
        public BlockInfoRoom(int _x, int _y) : base(_x, _y)
        {
            BlockType = "Room";
        }
    }
}
