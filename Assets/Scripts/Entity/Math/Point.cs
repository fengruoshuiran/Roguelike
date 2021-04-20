using NUnit.Framework.Internal.Filters;
using UnityEditor.UI;
using UnityEngine;

namespace Ruoran.Roguelike.Entity
{
    // 提供简单点数管理，用于生命，施法资源等点数管理。已重载运算符
    public class Point : MonoBehaviour
    {
        // 数值的最大上限，一般而言不会动态变化
        public double Max { get; set; } = 100;
        // 楼层、区域内因受到攻击而惩罚降低后的上限，一般来说只有突破区域才可以回复。始终低于Max，适用于生命上限、施法资源上限等
        public double Full { get; set; } = 100;
        // 实际的点数值
        public double Curr { get; set; } = 100;
        // 点数的自然恢复量
        public double AutoHeal { get; set; } = 0;

        public Point(double _max = 100, double _full = 100, double _curr = 100, double _autoHeal = 100)
        {
            Max = _max;
            Full = _full;
            Curr = _curr;
            AutoHeal = _autoHeal;
        }

        public static Point operator+ (Point a, double b)
        {
            a.Curr += b;
            if (a.Curr >= a.Full)
            {
                a.Curr = a.Full;
            }
            return a;
        }

        public static Point operator- (Point a, double b)
        {
            a.Curr -= b;
            return a;
        }

        public void Heal(double h)
        {
            Curr += h;
            if (Curr >= Full)
            {
                Curr = Full;
            }
        }

        private void FixedUpdate()
        {
            // 一秒更新十次，满状态不更新
            if (Time.frameCount % (Constant.FPS / 10) == 0 || Curr >= Full) return;

            Heal(AutoHeal);
        }
    }

}