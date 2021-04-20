using System;
using UnityEngine;

namespace Ruoran.Roguelike.Dungeon
{
    public class SimpleLevelDescription : AbstractLevelDescription
    {
        public SimpleLevelDescription()
        {
            // TODO: 配置应拆分
            // 房间设置
            AddRoom(2, 2, "2*2", 5.0);

            AddRoom(2, 3, "2*3", 3.75);
            AddRoom(3, 2, "3*2", 3.75);

            AddRoom(3, 3, "3*3", 10.0);

            AddRoom(2, 4, "2*4", 1.25);
            AddRoom(4, 2, "4*2", 1.25);

            AddRoom(3, 4, "3*4", 10.0);
            AddRoom(4, 3, "4*3", 10.0);

            AddRoom(4, 4, "4*4", 7.5);

            AddRoom(4, 5, "4*5", 2.5);
            AddRoom(5, 4, "5*4", 2.5);

            AddRoom(5, 5, "5*5", 1.0);

            // 路口样式、障碍样式初始化载入
            SimpleTemplateAdder.AddDefault();
            // 路口样式设置
            AddCross(true, true, true, true, "1111: Default", 10);

            AddCross(true, true, true, false, "1110: Default", 10);
            AddCross(true, true, false, true, "1101: Default", 10);
            AddCross(true, false, true, true, "1011: Default", 10);
            AddCross(false, true, true, true, "0111: Default", 10);

            AddCross(true, true, false, false, "1100: Default", 10);
            AddCross(true, false, true, false, "1010: Default", 10);
            AddCross(false, true, true, false, "0110: Default", 10);
            AddCross(true, false, false, true, "1001: Default", 10);
            AddCross(false, true, false, true, "0101: Default", 10);
            AddCross(false, false, true, true, "0011: Default", 10);

            AddCross(true, false, false, false, "1000: Default", 10);
            AddCross(false, true, false, false, "0100: Default", 10);
            AddCross(false, false, true, false, "0010: Default", 10);
            AddCross(false, false, false, true, "0001: Default", 10);

            // 障碍样式设置
            AddObstacle("Default", 10);

            // 贴图方案选用
            // 这是测试的黑白图
            TestTileSetting.Import();
        }
    }
}
