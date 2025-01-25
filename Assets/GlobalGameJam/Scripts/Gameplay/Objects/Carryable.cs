using UnityEngine;

namespace GlobalGameJam.Gameplay
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class Carryable : MonoBehaviour
    {
        [SerializeField] protected SpriteRenderer spriteRenderer;
        [SerializeField] protected SphereCollider sphereCollider;
        [SerializeField] protected TrailRenderer trailRenderer;
        
        public Rigidbody AttachedRigidbody { get; private set; }

#region Lifecycle Events

        protected virtual void Awake()
        {
            AttachedRigidbody = GetComponent<Rigidbody>();
        }

        protected virtual void OnDisable()
        {
            trailRenderer.Clear();
        }

#endregion

#region Methods

        public abstract void Despawn();

#endregion
    }
}