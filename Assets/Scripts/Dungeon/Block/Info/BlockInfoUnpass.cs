namespace Ruoran.Roguelike.Dungeon
{
    public class BlockInfoUnpass : BlockInfo
    {
        public BlockInfoUnpass(int _x, int _y) : base(_x, _y)
        {
            BlockType = "Unpass";
            CanPass = false;
        }
    }
}
