using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Extensions
{
    public static class GeometryExtensions
    {
        /// <summary>
        /// Calculates cartesian coordinates from polar coordinates
        /// and an offset.
        /// </summary>
        /// <param name="origin">Origin of the polar coordinate system</param>
        /// <param name="angle">Angle to x axis in degrees</param>
        /// <param name="distance">Distance from origin</param>
        /// <returns></returns>
        public static Vector2 Polar2CartesianDeg(Vector2 origin, float angle, float distance)
        {
            return origin + Polar2Cartesian(angle * Mathf.Deg2Rad, distance);
        }


        /// <summary>
        /// Calculates cartesian coordinates from polar coordinates
        /// and an offset.
        /// </summary>
        /// <param name="origin">Origin of the polar coordinate system</param>
        /// <param name="angle">Angle to x axis in radians</param>
        /// <param name="distance">Distance from origin</param>
        /// <returns></returns>
        public static Vector2 Polar2CartesianRad(Vector2 origin, float angle, float distance)
        {
            return origin + Polar2Cartesian(angle, distance);
        }

        /// <summary>
        /// Calculates cartesian coordinates from polar coordinates.
        /// </summary>
        /// <param name="angle">Angle to x axis in radians</param>
        /// <param name="distance">Distance from (0,0)</param>
        /// <returns></returns>
        private static Vector2 Polar2Cartesian(float angle, float distance)
        {
            return new Vector2(
                Mathf.Cos(angle) * distance,
                Mathf.Sin(angle) * distance
                );
        }

        public static Vector2 Vector2Half(Vector2 v1, Vector2 v2)
        {
            return (v1 + v2) / 2;
        }
    }
}
