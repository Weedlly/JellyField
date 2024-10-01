using System.Collections.Generic;
using UnityEngine;

namespace Common.Scripts.Utilities
{
    public struct RandomItem<T>
    {
        public T Type;
        public float WeightUnit;
    }

    public static class RandomMachineUtility<T>
    {
        public static bool IsTrue(float trueWeight, float falseWeight)
        {
            float weightRanVal = Random.Range(0f, trueWeight + falseWeight);
            return weightRanVal < trueWeight;
        }
        public static RandomItem<T> GetRandomItem(List<RandomItem<T>> itemList)
        {
            float maxWeight = 0;
            itemList.ForEach(item => maxWeight += item.WeightUnit);
            
            float weightRanVal = Random.Range(0f, maxWeight);
    
            float curWeight = 0;
            foreach (RandomItem<T> item in itemList)
            {
                if (curWeight < weightRanVal &&  curWeight + item.WeightUnit > weightRanVal)
                {
                    return item;
                }
                curWeight += item.WeightUnit;
            }
            return new RandomItem<T>();
        }
    }
}
