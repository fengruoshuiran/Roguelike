namespace Ruoran.Roguelike
{
    public static class Constant
    {
        // 逻辑帧率50/s
        public const int LogicalFramePerSec = 50;
        // 便于编写的简称，此处的F指代逻辑帧率
        public const int FPS = LogicalFramePerSec;

        // 区块大小
        public const int ChunkSize = 16;
        // 区块边境大小
        public const int OverBorderChunkSize = 2;
    }
}
