namespace Ruoran.Roguelike.Dungeon
{
    public class ChunkInfoRoom : ChunkInfo
    {
        // 表示连通路口的方向与有无，0表示无路口连接，1、2、3、4分别代表上、下、左、右
        public int Dir { get; set; } = 0;

        public ChunkInfoRoom(int _x = 0, int _y = 0) : base(_x, _y)
        {
            ChunkType = "Room";
        }
    }
}
