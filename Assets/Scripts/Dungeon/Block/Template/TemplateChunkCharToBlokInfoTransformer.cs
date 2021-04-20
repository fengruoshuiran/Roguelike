namespace Ruoran.Roguelike.Dungeon
{
    // 将模板字符区块转换为BlockInfo格式
    public static class TemplateChunkCharToBlockInfoTransformer
    {
        public static BlockInfo[,] Transform(char[,] templateChunkChar)
        {
            // 坐标转换，数组xy相比于地图xy旋转了90度
            var sizeX = templateChunkChar.GetLength(1);
            var sizeY = templateChunkChar.GetLength(0);

            var ChunkBlockInfo = new BlockInfo[sizeX, sizeY];

            for (int i = 0; i < sizeX; i++)
            {
                for (int j = 0; j < sizeY; j++)
                {
                    // 坐标转换，数组xy相比于地图xy旋转了90度
                    var charVecI = sizeY - 1 - j;
                    var charVecJ = i;
                    switch (templateChunkChar[charVecI, charVecJ])
                    {
                        case '-':
                        case '|':
                            ChunkBlockInfo[i, j] = new BlockInfoRoad(i, j);
                            break;
                        case '+':
                            ChunkBlockInfo[i, j] = new BlockInfoCross(i, j);
                            break;

                        case 'x':
                            ChunkBlockInfo[i, j] = new BlockInfoWall(i, j);
                            break;
                        case 'O':
                        default:
                            ChunkBlockInfo[i, j] = new BlockInfoObstacle(i, j);
                            break;
                    }
                }
            }

            return ChunkBlockInfo;
        }
    }
}
