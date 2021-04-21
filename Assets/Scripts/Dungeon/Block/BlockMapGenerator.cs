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
                    BlockMap[i, j].CanPass = false;
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
            for (int i = 0; i < RoomGenerator.RoomPosInfo.Count; i++)
            {
                int startX = RoomGenerator.RoomPosInfo[i].Item1;
                int startY = RoomGenerator.RoomPosInfo[i].Item2;
                int endX = RoomGenerator.RoomPosInfo[i].Item3;
                int endY = RoomGenerator.RoomPosInfo[i].Item4;

                // 生成墙体环绕房间
                for (int x = startX; x < endX; x++)
                {
                    for (int y = startY; y < endY; y++)
                    {
                        // TODO: 默认墙体厚度3，因为不可配置这是不当的
                        if ((x - startX) < 3 || (y - startY) < 3 || (endX - x) <= 3 || (endY - y) <= 3)
                        {
                            BlockMap[x, y] = new BlockInfoWall(x, y);
                        }
                        else
                        {
                            BlockMap[x, y] = new BlockInfoRoom(x, y);
                        }
                    }
                }

                int startChunkX = startX / Constant.ChunkSize;
                int startChunkY = startY / Constant.ChunkSize;
                int endChunkX = endX / Constant.ChunkSize;
                int endChunkY = endY / Constant.ChunkSize;

                // 读取区块地图中存储的门信息，生成门
                for (int x = startChunkX; x < endChunkX; x++)
                {
                    for (int y = startChunkY; y < endChunkY; y++)
                    {
                        var dir = (ChunkMap[x, y] as ChunkInfoRoom).Dir;
                        // TODO: 此处硬编码，不当
                        if (dir == 1)
                        {
                            for (int p = 6; p < 10; p++)
                            {
                                for (int q = 13; q < 16; q++)
                                {
                                    var posX = x * Constant.ChunkSize + p;
                                    var posY = y * Constant.ChunkSize + q;
                                    BlockMap[posX, posY] = new BlockInfoDoor(posX, posY);
                                }
                            }
                        }
                        else if (dir == 2)
                        {
                            for (int p = 6; p < 10; p++)
                            {
                                for (int q = 0; q < 3; q++)
                                {
                                    var posX = x * Constant.ChunkSize + p;
                                    var posY = y * Constant.ChunkSize + q;
                                    BlockMap[posX, posY] = new BlockInfoDoor(posX, posY);
                                }
                            }
                        }
                        else if (dir == 3)
                        {
                            for (int p = 0; p < 3; p++)
                            {
                                for (int q = 6; q < 10; q++)
                                {
                                    var posX = x * Constant.ChunkSize + p;
                                    var posY = y * Constant.ChunkSize + q;
                                    BlockMap[posX, posY] = new BlockInfoDoor(posX, posY);
                                }
                            }
                        }
                        else if (dir == 4)
                        {
                            for (int p = 13; p < 16; p++)
                            {
                                for (int q = 6; q < 10; q++)
                                {
                                    var posX = x * Constant.ChunkSize + p;
                                    var posY = y * Constant.ChunkSize + q;
                                    BlockMap[posX, posY] = new BlockInfoDoor(posX, posY);
                                }
                            }
                        }
                    }
                }

                RoomGenerator.RoomPosInfo[i] = new Tuple<int, int, int, int>(startX + 3, startY + 3, endX - 3, endY - 3);
            }
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

            PrintSubBlockMap(100, 200, 100, 200);
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
                    else if (BlockMap[i, j].BlockType == "Room") buffer += "* ";
                    else if (BlockMap[i, j].BlockType == "Door") buffer += "D ";
                    else buffer += "X ";
                }
                buffer += "\n";
            }
            Debug.Log(buffer);
        }
    }
}
