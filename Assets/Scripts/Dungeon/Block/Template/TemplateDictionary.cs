using System.Collections.Generic;

namespace Ruoran.Roguelike.Dungeon
{
    public static class TemplateDictionary
    {
        public static Dictionary<string, AbstractChunkCharTemplate> Dic = new Dictionary<string, AbstractChunkCharTemplate>();

        public static void Add(string name, char[,] chunkChar)
        {
            Dic.Add(name, new ChunkCharTemplate(chunkChar));
        }
    }
}