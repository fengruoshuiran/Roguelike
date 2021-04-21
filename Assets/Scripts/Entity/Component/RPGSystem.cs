using UnityEngine;
using System.Collections.Generic;

namespace Ruoran.Roguelike.Entity
{
    public class RPGSystem : MonoBehaviour
    {
        // 生命值
        public Point HP;
        // 各种施法资源的集合，默认情况下是地火水风四元素
        public Dictionary<string, Point> MP;

        // 攻击力，取决于当前火元素上限
        public Attack
    }
}
