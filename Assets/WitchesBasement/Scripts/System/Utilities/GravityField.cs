using UnityEngine;

namespace WitchesBasement.System
{
    internal class GravityField : MonoBehaviour
    {
        [SerializeField] private float radius = 2;
        [SerializeField] private float pullForce = 1;
        [SerializeField] private LayerMask layer;

#region Lifecycle Events

        private void FixedUpdate()
        {
            Collider[] results = new Collider[8];
            var numCollisions = Physics.OverlapSphereNonAlloc(transform.position, radius, results, layer.value);

            for (var i = 0; i < numCollisions; i++)
            {
                var result = results[i];
                var forceDirection = transform.position - result.transform.position;
                
                result.attachedRigidbody.AddForce(forceDirection.normalized * pullForce);
            }
        }

#if UNITY_EDITOR

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, radius);
        }

#endif

#endregion
    }
}