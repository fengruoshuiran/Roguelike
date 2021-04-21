using System;
using System.Collections.Generic;

namespace Ruoran.Roguelike.Dungeon
{
    public static class RoomGenerator
    {
        // 四元组startX, startY, endX, endY记录房间方块位置信息
        public static List<Tuple<int, int, int, int>> RoomPosInfo { get; set; } = new List<Tuple<int, int, int, int>>();

        public static void Reset()
        {
            RoomPosInfo = new List<Tuple<int, int, int, int>>();
        }

        public static void Add(int startX, int startY, int endX, int endY)
        {
            RoomPosInfo.Add(new Tuple<int, int, int, int>(startX, startY, endX, endY));
        }
    }
}
