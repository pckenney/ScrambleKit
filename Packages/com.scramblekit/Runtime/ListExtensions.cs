using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace ScrambleKit
{
    public static class ListExtensions
    {
        public static T DrawFromAtRandom<T>(this IList<T> list)
        {
            if (list.Count == 0)
                return default;

            int idx = Random.Range(0, list.Count);
            return list[idx];
        }

        public static T RemoveFromAtRandom<T>(this IList<T> list)
        {
            T item = list.DrawFromAtRandom();
            list.Remove(item);
            return item;
        }

        public static T DrawFromAtRandomByWeight<T>(this IEnumerable<T> sequence, System.Func<T, float> weightFunction)
        {
            float totalWeight = sequence.Sum(weightFunction);

            float chosenWeightIndex = Random.Range(0f, totalWeight);
            float currentWeightIndex = 0;

            foreach (var item in from weightedItem in sequence select new { Value = weightedItem, Weight = weightFunction(weightedItem) })
            {
                currentWeightIndex += item.Weight;

                if (currentWeightIndex >= chosenWeightIndex)
                {
                    return item.Value;
                }
            }

            return default;
        }
    }
}
