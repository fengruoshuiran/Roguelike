using NUnit.Framework;
using Ruoran.Roguelike.Rand;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ruoran.Roguelike.Dungeon
{
    public static class DungeonGenerator
    {
        // 当前地牢使用的种子
        public static int seed;
        // 当前层使用的生成描述
        public static AbstractLevelDescription LevelDescription;
        // 当前层使用的区块地图
        public static ChunkInfo[,] ChunkMap;
        // 当前层使用的方块地图
        public static BlockInfo[,] BlockMap;


        public static void Build()
        {
            seed = Time.frameCount;

            // 重置种子
            RNG.Reseed(seed);

            // TODO: 这是测试的，临时的
            BuildLevel(new SimpleLevelDescription());
        }
        public static void BuildLevel(AbstractLevelDescription _levelDescription)
        {
            LevelDescription = _levelDescription;

            // 生成区块地图
            ChunkMap = ChunkMapGenerator.BuildChunkMap(LevelDescription);
            // 生成方块地图
            BlockMap = BlockMapGenerator.BuildBlockMap(LevelDescription, ChunkMap);
            // 实例化地图
            RealMapGenerator.PrintMap(BlockMap);
        }
    }
}
