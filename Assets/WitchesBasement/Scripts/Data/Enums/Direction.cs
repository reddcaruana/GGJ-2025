using UnityEngine;

namespace WitchesBasement.Data
{
    public enum Direction
    {
        North,
        NorthEast,
        East,
        SouthEast,
        South,
        SouthWest,
        West,
        NorthWest
    }
    
    public static class DirectionExtensions
    {
        public static Vector3 ToVector(this Direction direction)
        {
            return direction switch
            {
                Direction.North => Vector3.forward,
                Direction.NorthEast => (Vector3.forward + Vector3.right).normalized,
                Direction.East => Vector3.right,
                Direction.SouthEast => (Vector3.back + Vector3.right).normalized,
                Direction.South => Vector3.back,
                Direction.SouthWest => (Vector3.back + Vector3.left).normalized,
                Direction.West => Vector3.left,
                Direction.NorthWest => (Vector3.forward + Vector3.left).normalized,
                _ => Vector3.zero
            };
        }
    }
}