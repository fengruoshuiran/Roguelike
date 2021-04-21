namespace Ruoran.Roguelike.Dungeon
{
    public class BlockInfoDoor : BlockInfo
    {
        public BlockInfoDoor(int _x, int _y) : base(_x, _y)
        {
            BlockType = "Door";
        }
    }
}