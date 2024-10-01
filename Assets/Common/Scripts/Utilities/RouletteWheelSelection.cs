using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.Scripts.Utilities
{
    public static class RouletteWheelSelection<T>{
        public static T Selection(List<T> items, Func<T, double> getWeight )
        {
            double totalWeight = items.Sum(getWeight);

            Random random = new Random();
            double randomValue = random.NextDouble() * totalWeight;
            
            double currentWeight = 0;
            foreach (T item in items)
            {
                currentWeight += getWeight(item);
                if (randomValue <= currentWeight)
                {
                    return item;
                }
            }
            return default;
        }
    }
}
