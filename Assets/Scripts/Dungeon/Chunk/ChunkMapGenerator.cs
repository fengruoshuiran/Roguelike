using NUnit.Framework;
using Ruoran.Roguelike.Rand;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ruoran.Roguelike.Dungeon
{
    public static class ChunkMapGenerator
    {
        // 当前层使用的生成描述
        public static AbstractLevelDescription LevelDescription;
        // 当前层使用的区块地图
        public static ChunkInfo[,] ChunkMap;

        public static ChunkInfo[, ] BuildChunkMap(AbstractLevelDescription _levelDescription)
        {
            // 初始化
            LevelDescription = _levelDescription;

            // 构造空图
            ChunkMap = new ChunkInfo[LevelDescription.MaxChunkX, LevelDescription.MaxChunkY];
            for (int i = 0; i < LevelDescription.MaxChunkX; i++)
            {
                for (int j = 0; j < LevelDescription.MaxChunkY; j++)
                {
                    ChunkMap[i, j] = new ChunkInfo(i, j);
                    if ((i < Constant.OverBorderChunkSize || i >= LevelDescription.MaxChunkX - Constant.OverBorderChunkSize) ||
                        (j < Constant.OverBorderChunkSize || j >= LevelDescription.MaxChunkY - Constant.OverBorderChunkSize))
                    {
                        ChunkMap[i, j].ChunkType = "OverNone";
                    }
                }
            }

            // 生成房间
            BuildInitRoom();
            // 生成路口
            BuildInitCross();
            // 连接路口
            ConnectCross();

            // 生成完毕，刷新道路毗邻数据
            RefreshCross();

            // 打印测试
            PrintChunkMap();

            return ChunkMap;
        }

        private static void BuildInitRoom()
        {
            // 根据关卡描述的生成欲望尝试生成次数，公式如下
            var tryBuildRoomTime = (int)(LevelDescription.MaxChunkX * LevelDescription.MaxChunkY / 10.0 * LevelDescription.RoomRate);
            var tryBuildDoorTime = (int)(RNG.RandInt(1, 2) * LevelDescription.DoorRate);

            // 尝试生成房间
            for (int i = 0; i < tryBuildRoomTime; i++)
            {
                // 禁止在边界一圈区块生成房间，因此排除边界点。房间向右上生长，因此右上保留空间
                var centerX = RNG.RandInt(1 + Constant.OverBorderChunkSize, LevelDescription.MaxChunkX - 2 - LevelDescription.MaxRoomSize - Constant.OverBorderChunkSize);
                var centerY = RNG.RandInt(1 + Constant.OverBorderChunkSize, LevelDescription.MaxChunkY - 2 - LevelDescription.MaxRoomSize - Constant.OverBorderChunkSize);
                var roomInfo = WeightListUtils.HitWeightListByBinary(LevelDescription.RoomWeightList) as RoomChunkWeightInfo;

                // 保证目标区域为空
                var flag = true;
                for (int x = centerX; x < centerX + roomInfo.X; x++)
                {
                    for (int y = centerY; y < centerY + roomInfo.Y; y++)
                    {
                        if (ChunkMap[x, y].ChunkType != "None") flag = false;
                    }
                }

                if (flag)
                {
                    // 填充目标区域为房间区块
                    for (int x = centerX; x < centerX + roomInfo.X; x++)
                    {
                        for (int y = centerY; y < centerY + roomInfo.Y; y++)
                        {
                            ChunkMap[x, y] = new ChunkInfoRoom();
                        }
                    }

                    for (int j = 0; j < tryBuildDoorTime; j++)
                    {
                        var randDir = RNG.RandInt(1, 4);
                        var randPos = 0;
                        switch (randDir)
                        {
                            case 1:
                                randPos = RNG.RandInt(0, roomInfo.X - 1);
                                (ChunkMap[centerX + randPos, centerY + roomInfo.Y - 1] as ChunkInfoRoom).Dir = 1;
                                // 保证邻接房间下也会生成对应的门
                                if (ChunkMap[centerX + randPos, centerY + roomInfo.Y - 1 + 1] is ChunkInfoRoom)
                                {
                                    (ChunkMap[centerX + randPos, centerY + roomInfo.Y - 1 + 1] as ChunkInfoRoom).Dir = 2;
                                }
                                // 否则在门口生成路口点
                                else
                                {
                                    ChunkMap[centerX + randPos, centerY + roomInfo.Y - 1 + 1] = new ChunkInfoCross(centerX + randPos, centerY + roomInfo.Y - 1 + 1);
                                }
                                break;
                            case 2:
                                randPos = RNG.RandInt(0, roomInfo.X - 1);
                                (ChunkMap[centerX + randPos, centerY] as ChunkInfoRoom).Dir = 2;
                                // 保证邻接房间下也会生成对应的门
                                if (ChunkMap[centerX + randPos, centerY - 1] is ChunkInfoRoom)
                                {
                                    (ChunkMap[centerX + randPos, centerY - 1] as ChunkInfoRoom).Dir = 1;
                                }
                                // 否则在门口生成路口点
                                else
                                {
                                    ChunkMap[centerX + randPos, centerY - 1] = new ChunkInfoCross(centerX + randPos, centerY - 1);
                                }
                                break;
                            case 3:
                                randPos = RNG.RandInt(0, roomInfo.Y - 1);
                                (ChunkMap[centerX, centerY + randPos] as ChunkInfoRoom).Dir = 3;
                                // 保证邻接房间下也会生成对应的门
                                if (ChunkMap[centerX - 1, centerY + randPos] is ChunkInfoRoom)
                                {
                                    (ChunkMap[centerX - 1, centerY + randPos] as ChunkInfoRoom).Dir = 4;
                                }
                                // 否则在门口生成路口点
                                else
                                {
                                    ChunkMap[centerX - 1, centerY + randPos] = new ChunkInfoCross(centerX - 1, centerY + randPos);
                                }
                                break;
                            case 4:
                                randPos = RNG.RandInt(0, roomInfo.Y - 1);
                                (ChunkMap[centerX + roomInfo.X - 1, centerY + randPos] as ChunkInfoRoom).Dir = 4;
                                // 保证邻接房间下也会生成对应的门
                                if (ChunkMap[centerX + roomInfo.X - 1 + 1, centerY + randPos] is ChunkInfoRoom)
                                {
                                    (ChunkMap[centerX + roomInfo.X - 1 + 1, centerY + randPos] as ChunkInfoRoom).Dir = 3;
                                }
                                // 否则在门口生成路口点
                                else
                                {
                                    ChunkMap[centerX + roomInfo.X - 1 + 1, centerY + randPos] = new ChunkInfoCross(centerX + roomInfo.X - 1 + 1, centerY + randPos);
                                }
                                break;
                        }
                    }

                }
            }
        }

        private static void BuildInitCross()
        {
            // 根据关卡描述的生成欲望尝试生成次数，公式如下
            var tryBuildCrossTime = (LevelDescription.MaxChunkX * LevelDescription.MaxChunkY / 200.0 * LevelDescription.CrossRate);

            // 尝试生成路口，用来提供更平滑的道路连接
            for (int i = 0; i < tryBuildCrossTime; i++)
            {
                var centerX = RNG.RandInt(Constant.OverBorderChunkSize, LevelDescription.MaxChunkX - 1 - LevelDescription.MaxRoomSize / 2 - Constant.OverBorderChunkSize);
                var centerY = RNG.RandInt(Constant.OverBorderChunkSize, LevelDescription.MaxChunkY - 1 - LevelDescription.MaxRoomSize / 2 - Constant.OverBorderChunkSize);

                // 检测是否被占据，如果否直接添加路口
                if (ChunkMap[centerX, centerY].ChunkType == "None")
                {
                    ChunkMap[centerX, centerY] = new ChunkInfoCross(centerX, centerY);
                }
            }
        }

        private static void ConnectCross()
        {
            var obstacleBuildTimeTotal = LevelDescription.ObstacleRate * 10;

            for (int buildTime = 0; buildTime <= obstacleBuildTimeTotal; buildTime++)
            {
                // 一种简易的填充方式，遍历所有相邻四点，若均为道路或空则随机生成障碍，合法的新障碍不能分割连接性
                for (int i = Constant.OverBorderChunkSize; i < LevelDescription.MaxChunkX - Constant.OverBorderChunkSize; i++)
                {
                    for (int j = Constant.OverBorderChunkSize; j < LevelDescription.MaxChunkY - Constant.OverBorderChunkSize; j++)
                    {
                        // 检查是否为空旷四区块
                        var isOpenSpace = true;

                        for (int deltaI = 0; deltaI < 2; deltaI++)
                        {
                            for (int deltaJ = 0; deltaJ < 2; deltaJ++)
                            {
                                var chunkType = ChunkMap[i + deltaI, j + deltaJ].ChunkType;
                                if (chunkType == "Room" || chunkType == "Obstacle" || chunkType == "OverNone")
                                {
                                    isOpenSpace = false;
                                }
                            }
                        }

                        if (isOpenSpace)
                        {
                            // 随机选择一个区块
                            var delta = RNG.RandInt(0, 3);
                            var deltaI = delta / 2;
                            var deltaJ = delta % 2;
                            // 检测新障碍是否合法，只要毗邻的8格子之存在一个连接体
                            var PosOffsetOfPoint = new Tuple<int, int>[9];
                            PosOffsetOfPoint[0] = new Tuple<int, int>(-1, -1);
                            PosOffsetOfPoint[1] = new Tuple<int, int>(-1, 0);
                            PosOffsetOfPoint[2] = new Tuple<int, int>(-1, 1);
                            PosOffsetOfPoint[3] = new Tuple<int, int>(0, 1);
                            PosOffsetOfPoint[4] = new Tuple<int, int>(1, 1);
                            PosOffsetOfPoint[5] = new Tuple<int, int>(1, 0);
                            PosOffsetOfPoint[6] = new Tuple<int, int>(1, -1);
                            PosOffsetOfPoint[7] = new Tuple<int, int>(0, -1);
                            PosOffsetOfPoint[8] = new Tuple<int, int>(-1, -1);

                            var crossToObstacleCount = 0;
                            for (int p = 0; p < 8; p++)
                            {
                                var nowX = i + deltaI + PosOffsetOfPoint[p].Item1;
                                var nowY = j + deltaJ + PosOffsetOfPoint[p].Item2;
                                var nowChunk = ChunkMap[nowX, nowY];
                                var nowChunkType = nowChunk.ChunkType;

                                var nextX = i + deltaI + PosOffsetOfPoint[p + 1].Item1;
                                var nextY = j + deltaJ + PosOffsetOfPoint[p + 1].Item2;
                                var nextChunkType = ChunkMap[nextX, nextY].ChunkType;

                                if ((nowChunkType == "None" || nowChunkType == "Cross") && (nextChunkType == "Obstacle" || nextChunkType == "Room" || nextChunkType == "OverNone"))
                                {
                                    crossToObstacleCount++;
                                }
                                // 禁止在门附近生成障碍
                                if ((nowChunkType == "Room" && (nowChunk as ChunkInfoRoom).Dir != 0))
                                {
                                    crossToObstacleCount += 10;
                                }
                            }

                            if (crossToObstacleCount < 2)
                            {
                                ChunkMap[i + deltaI, j + deltaJ] = new ChunkInfoObstacle(i + deltaI, j + deltaJ);
                            }

                        }
                    }
                }
            }

            // 将剩余空白用道路填充
            for (int i = Constant.OverBorderChunkSize; i < LevelDescription.MaxChunkX - Constant.OverBorderChunkSize; i++)
            {
                for (int j = Constant.OverBorderChunkSize; j < LevelDescription.MaxChunkY - Constant.OverBorderChunkSize; j++)
                {
                    if (ChunkMap[i, j].ChunkType == "None")
                    {
                        ChunkMap[i, j] = new ChunkInfoCross(i, j);
                    }
                }
            }
        }

        private static void RefreshCross()
        {
            for (int i = Constant.OverBorderChunkSize; i < LevelDescription.MaxChunkX - Constant.OverBorderChunkSize; i++)
            {
                for (int j = Constant.OverBorderChunkSize; j < LevelDescription.MaxChunkY - Constant.OverBorderChunkSize; j++)
                {
                    var nowVis = ChunkMap[i, j];
                    if (nowVis.ChunkType == "Cross")
                    {
                        var up = ChunkMap[i, j + 1];
                        var down = ChunkMap[i, j - 1];
                        var left = ChunkMap[i - 1, j];
                        var right = ChunkMap[i + 1, j];

                        if (up.ChunkType == "Cross" || (up.ChunkType == "Room" && (up as ChunkInfoRoom).Dir == 2))
                        {
                            (nowVis as ChunkInfoCross).Up = true;
                        }
                        if (down.ChunkType == "Cross" || (down.ChunkType == "Room" && (down as ChunkInfoRoom).Dir == 1))
                        {
                            (nowVis as ChunkInfoCross).Down = true;
                        }
                        if (left.ChunkType == "Cross" || (left.ChunkType == "Room" && (left as ChunkInfoRoom).Dir == 4))
                        {
                            (nowVis as ChunkInfoCross).Left = true;
                        }
                        if (right.ChunkType == "Cross" || (right.ChunkType == "Room" && (right as ChunkInfoRoom).Dir == 3))
                        {
                            (nowVis as ChunkInfoCross).Right = true;
                        }
                    }
                }
            }
        }

        private static void PrintChunkMap()
        {
            string buffer = "";
            for (int i = 0; i < LevelDescription.MaxChunkX; i++)
            {
                for (int j = 0; j < LevelDescription.MaxChunkY; j++)
                {
                    if (ChunkMap[i, j].ChunkType == "Cross") buffer += "+ ";
                    else if (ChunkMap[i, j].ChunkType == "Room")
                    {
                        if ((ChunkMap[i, j] as ChunkInfoRoom).Dir == 0) buffer += "# ";
                        else buffer += "D ";
                    }
                    else if (ChunkMap[i, j].ChunkType == "Obstacle") buffer += "O ";
                    else if (ChunkMap[i, j].ChunkType == "None") buffer += ". ";
                    else buffer += "X ";
                }
                buffer += "\n";
            }
            Debug.Log(buffer);
        }
    }
}
