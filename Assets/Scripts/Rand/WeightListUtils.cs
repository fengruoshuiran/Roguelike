using System.Collections.Generic;
using UnityEngine;

namespace Ruoran.Roguelike.Rand
{
    public static class WeightListUtils
    {
        public static DefaultWeightInfo HitWeightListByBinary(List<DefaultWeightInfo> weightInfoList)
        {
            double TotalWeight = weightInfoList[weightInfoList.Count - 1].HitR;
            double rd = RNG.RandDouble(r: TotalWeight);

            // 二分，左闭右开
            int l = 0;
            int r = weightInfoList.Count;
            int mid = (l + r) / 2;

            while (l != mid)
            {
                if (weightInfoList[mid].HitL > rd)
                {
                    r = mid;
                    mid = (l + r) / 2;
                }
                else
                {
                    l = mid;
                    mid = (l + r) / 2;
                }
            }

            return weightInfoList[mid];
        }

        // 为权重列表添加新项，返回更新后的总权重值
        public static void Add(string someType, double weight, List<DefaultWeightInfo> WeightInfoList)
        {
            double TotalWeight;
            
            if (WeightInfoList.Count > 0)
            {
                TotalWeight = WeightInfoList[WeightInfoList.Count - 1].HitR;
            }
            else
            {
                TotalWeight = 0;
            }

            WeightInfoList.Add(new DefaultWeightInfo(someType, weight, TotalWeight, TotalWeight + weight));
        }
    }
}