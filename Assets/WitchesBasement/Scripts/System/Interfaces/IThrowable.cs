using UnityEngine;

namespace WitchesBasement.System
{
    internal interface IThrowable
    {
        void Throw(Vector3 direction, float force, float angle);
    }
}