using UnityEngine;

namespace GlobalGameJam
{
    /// <summary>
    /// Provides utility methods for time-related calculations.
    /// </summary>
    public static class TimeUtility
    {
        /// <summary>
        /// Gets the number of minutes from a given value in seconds.
        /// </summary>
        /// <param name="value">The value in seconds.</param>
        /// <returns>The number of minutes.</returns>
        public static int GetMinutes(float value)
        {
            if (value <= 0)
            {
                return 0;
            }
            
            return Mathf.FloorToInt(value / 60);
        }

        /// <summary>
        /// Gets the number of seconds from a given value in seconds.
        /// </summary>
        /// <param name="value">The value in seconds.</param>
        /// <returns>The number of seconds.</returns>
        public static int GetSeconds(float value)
        {
            if (value <= 0)
            {
                return 0;
            }
            
            return Mathf.FloorToInt(value % 60);
        }

        /// <summary>
        /// Converts a given value in seconds to a string in the format "MM:SS".
        /// </summary>
        /// <param name="value">The value in seconds.</param>
        /// <returns>A string representing the time in "MM:SS" format.</returns>
        public static string ToString(float value)
        {
            var minutes = GetMinutes(value);
            var seconds = GetSeconds(value);

            return $"{minutes:#00}:{seconds:00}";
        }
    }
}