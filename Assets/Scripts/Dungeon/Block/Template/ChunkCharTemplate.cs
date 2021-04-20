using System;

namespace Ruoran.Roguelike.Dungeon
{
    public class ChunkCharTemplate : AbstractChunkCharTemplate
    {
        public ChunkCharTemplate(char[,] _chunkChar)
        {
            if (_chunkChar.GetLength(0) == Constant.ChunkSize && _chunkChar.GetLength(1) == Constant.ChunkSize)
            {
                Chunkchar = _chunkChar;
            }
        }
    }
}