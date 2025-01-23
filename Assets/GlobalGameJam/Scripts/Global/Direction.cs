using System;
using UnityEngine;

namespace GlobalGameJam
{
    public enum Direction
    {
        Left,
        Right,
        Forward,
        Back
    }

    public static class DirectionExtensions
    {
        public static Vector3 ToVector(this Direction direction)
        {
            return direction switch
            {
                Direction.Left => Vector3.left,
                Direction.Right => Vector3.right,
                Direction.Forward => Vector3.forward,
                Direction.Back => Vector3.back,
                _ => Vector3.zero
            };
        }
    }
}