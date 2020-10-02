using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

namespace Utils.Math
{
    public static class MathExtensions
    {
        /// <summary>
        ///
        /// <para>
        /// Compares two floating point values if they are similar.
        /// </para>
        ///
        /// </summary>
        /// <param name="v1"/><param name="v2"/>
        public static bool Approx(this float v1, float v2)
        {
            return Mathf.Approximately(v1, v2);
        }

        public static bool Approx(this Vector3 v1, Vector3 v2)
        {
            return Mathf.Approximately(v1.x, v2.x) && Mathf.Approximately(v1.z, v2.y) && Mathf.Approximately(v1.z, v2.z);
        }

        /// <summary>
        /// return s the max value between x, y and z
        /// </summary>
        /// <returns></returns>
        public static float Max(this Vector3 v)
        {
            return Mathf.Max(v.x, v.y, v.z);
        }

        public static List<int> RandomShuffleInt(this List<int> list)
        {
            var rand = new Random();
            var n = list.Last() - list.First();
            for (var i = n - 1; i > 0; i--)
            {
                Swap(list, i, rand.Next(i+1));
            }
            return list;
        }

        public static void Swap<T>(List<T> list, int first, int second)
        {
            var temp = list[first];
            list[first] = list[second];
            list[second] = temp;
        }
    }
}