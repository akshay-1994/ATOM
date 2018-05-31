using System;
using System.Linq;

namespace Aurigo.Atom.Generator.Core.Helpers
{
    internal class RandomValueHelper
    {
        private static Random _random = new Random();

        /// <summary>
        /// Generates the string.
        /// </summary>
        /// <param name="length">The length.</param>
        /// <returns></returns>
        public static string GenerateRandomString(int length = -1)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            if (length == -1)
                length = _random.Next(50);

            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[_random.Next(s.Length)]).ToArray());
        }

        /// <summary>
        /// Generates the random integer.
        /// </summary>
        /// <param name="minValue">The minimum value.</param>
        /// <param name="maxValue">The maximum value.</param>
        /// <returns></returns>
        public static int GenerateRandomInteger(int minValue = 1, int maxValue = 9999)
        {
            return _random.Next(minValue, maxValue);
        }

        /// <summary>
        /// Generates the random double.
        /// </summary>
        /// <param name="minValue">The minimum value.</param>
        /// <param name="maxValue">The maximum value.</param>
        /// <returns></returns>
        public static double GenerateRandomDouble(double minValue = 1.00, double maxValue = 9999.99)
        {
            return _random.NextDouble() * (maxValue - minValue) + minValue;
        }

        public static decimal GenerateRandomDecimal(decimal minValue = 1.00m, decimal maxValue = 9999.99m)
        {
            return (Convert.ToDecimal(_random.NextDouble()) * (maxValue - minValue) + minValue);
        }
    }
}