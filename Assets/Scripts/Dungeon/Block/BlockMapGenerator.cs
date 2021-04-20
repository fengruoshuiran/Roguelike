using NUnit.Framework;
using Ruoran.Roguelike.Rand;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ruoran.Roguelike.Dungeon
{
    public static class BlockMapGenerator
    {
        // 当前层使用的生成描述
        public static AbstractLevelDescription LevelDescription;
        // 当前层使用的区块地图
        public static ChunkInfo[,] ChunkMap;
        // 全局方块地图
        public static BlockInfo[,] BlockMap;

        public static BlockInfo[,] BuildBlockMap(AbstractLevelDescription _levelDescription, ChunkInfo[,] _chunkMap)
        {
            // 初始化数据，存入类中缓存
            LevelDescription = _levelDescription;
            ChunkMap = _chunkMap;

            // 初始化方块地图
            BlockMap = new BlockInfo[LevelDescription.MaxChunkX * Constant.ChunkSize, LevelDescription.MaxChunkY * Constant.ChunkSize];
            for (int i = 0; i < BlockMap.GetLength(0); i++)
            {
                for (int j = 0; j < BlockMap.GetLength(1); j++)
                {
                    BlockMap[i, j] = new BlockInfo(i, j);
                }
            }

            // 使用房间构造器生成房间方块地图
            BuildbyRoomGenerator();
            // 使用模板生成器填充通常区块
            BuildbyTemplateChunk();

            return BlockMap;
        }

        public static void BuildbyRoomGenerator()
        {
        }

        public static void BuildbyTemplateChunk()
        {
            for (int i = Constant.OverBorderChunkSize; i < LevelDescription.MaxChunkX - Constant.OverBorderChunkSize; i++)
            {
                for (int j = Constant.OverBorderChunkSize; j < LevelDescription.MaxChunkY - Constant.OverBorderChunkSize; j++)
                {
                    if (ChunkMap[i, j].ChunkType == "Obstacle")
                    {

                    }
                    else if (ChunkMap[i, j].ChunkType == "Cross")
                    {
                        var subBlockInfo = LevelDescription.RandCrossBlockInfoByChunk(ChunkMap[i, j] as ChunkInfoCross);

                        var fullMapStartX = i * Constant.ChunkSize;
                        var fullMapStartY = j * Constant.ChunkSize;

                        for (int p = 0; p < Constant.ChunkSize; p++)
                        {
                            for (int q = 0; q < Constant.ChunkSize; q++)
                            {
                                BlockMap[fullMapStartX + p, fullMapStartY + q] = subBlockInfo[p, q];
                            }
                        }
                    }
                }
            }
        }

        public static void PrintSubBlockMap(int sx, int ex, int sy, int ey)
        {
            string buffer = "";
            for (int i = sx; i < ex; i++)
            {
                for (int j = sy; j < ey; j++)
                {
                    if (BlockMap[i, j].BlockType == "Road") buffer += "- ";
                    else if (BlockMap[i, j].BlockType == "Cross") buffer += "+ ";
                    else if (BlockMap[i, j].BlockType == "Obstacle") buffer += "O ";
                    else buffer += "X ";
                }
                buffer += "\n";
            }
            Debug.Log(buffer);
        }
    }
}
