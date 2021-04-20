using System;
using System.Collections.Generic;
using Ruoran.Roguelike.Rand;
using UnityEngine;

namespace Ruoran.Roguelike.Dungeon
{
    public abstract class AbstractLevelDescription
    {
        // 难度设置
        public double DifficltyRate { get; set; } = 1;
        public double Difficulty { get; set; } = 10;

        // 地图区块数横纵限制
        public int MaxChunkX { get; set; } = 32;
        public int MaxChunkY { get; set; } = 32;

        // 房间结构类型与权重记录列表
        public List<DefaultWeightInfo> RoomWeightList { get; set; } = new List<DefaultWeightInfo>();
        // 房间最大大小
        public int MaxRoomSize { get; set; } = 5;

        // 路口结构类型与权重记录列表组
        public Dictionary<Tuple<bool, bool, bool, bool>, List<DefaultWeightInfo>> CrossWeightLists = new Dictionary<Tuple<bool, bool, bool, bool>, List<DefaultWeightInfo>>();

        // 障碍结构类型与权重记录列表
        public List<DefaultWeightInfo> ObstacleWeightList { get; set; } = new List<DefaultWeightInfo>();


        // 关键结构生成欲望
        public double RoomRate = 1;
        public double DoorRate = 1;
        public double CrossRate = 1;
        public double ObstacleRate = 1;

        public AbstractLevelDescription()
        {
            // 路口结构类型与权重记录列表组初始化
            for (int a = 0; a < 2; a++)
            {
                for (int b = 0; b < 2; b++)
                {
                    for (int c = 0; c < 2; c++)
                    {
                        for (int d = 0; d < 2; d++)
                        {
                            CrossWeightLists.Add(new Tuple<bool, bool, bool, bool>(a == 0, b == 0, c == 0, d == 0), new List<DefaultWeightInfo>());
                        }
                    }
                }
            }
        }

        public void AddRoom(int x, int y, string t, double w)
        {
            double TotalRoomWeight;
            if (RoomWeightList.Count > 0)
            {
                TotalRoomWeight = RoomWeightList[RoomWeightList.Count - 1].HitR;
            }
            else
            {
                TotalRoomWeight = 0;
            }
            RoomWeightList.Add(new RoomChunkWeightInfo(x, y, t, w, TotalRoomWeight, TotalRoomWeight + w));
        }

        public DefaultWeightInfo RandRoom()
        {
            return WeightListUtils.HitWeightListByBinary(RoomWeightList);
        }

        public void AddCross(bool up, bool down, bool left, bool right, string crossName, double weight)
        {
            var crossType = new Tuple<bool, bool, bool, bool>(up, down, left, right);
            WeightListUtils.Add(crossName, weight, CrossWeightLists[crossType]);
        }

        public DefaultWeightInfo RandCross(bool up, bool down, bool left, bool right)
        {
            var crossType = new Tuple<bool, bool, bool, bool>(up, down, left, right);


            return WeightListUtils.HitWeightListByBinary(CrossWeightLists[crossType]);
        }

        public BlockInfo[,] RandCrossBlockInfo(bool up, bool down, bool left, bool right)
        {
            var crossName = RandCross(up, down, left, right).Type;
            var subBlockMap = TemplateChunkCharToBlockInfoTransformer.Transform(TemplateDictionary.Dic[crossName].Chunkchar);

            return subBlockMap;
        }

        public BlockInfo[,] RandCrossBlockInfoByChunk(ChunkInfoCross crossChunk)
        {
            var up = crossChunk.Up;
            var down = crossChunk.Down;
            var left = crossChunk.Left;
            var right = crossChunk.Right;

            return RandCrossBlockInfo(up, down, left, right);
        }

        public void AddObstacle(string obstacleName, double weight)
        {
            WeightListUtils.Add(obstacleName, weight, ObstacleWeightList);
        }

        public DefaultWeightInfo RandObstacle()
        {
            return WeightListUtils.HitWeightListByBinary(ObstacleWeightList);
        }

        public BlockInfo[,] RandObstacleBlockInfoByChunk(ChunkInfoObstacle obstacleChunk)
        {
            var obstacleName = RandObstacle().Type;
            var subBlockMap = TemplateChunkCharToBlockInfoTransformer.Transform(TemplateDictionary.Dic[obstacleName].Chunkchar);

            return subBlockMap;
        }
    }
}
