using UnityEngine;

namespace GlobalGameJam.Gameplay
{
    public interface IThrowable
    {
        void Throw(Vector3 direction, float force, float angle);
    }
}