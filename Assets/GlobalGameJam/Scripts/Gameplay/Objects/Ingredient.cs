using System;
using GlobalGameJam.Data;
using UnityEngine;

namespace GlobalGameJam.Gameplay
{
    [RequireComponent(typeof(Rigidbody))]
    public class Ingredient : MonoBehaviour, IUsable
    {
        private const float ExpandColliderRadius = 0.1f;
        
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private SphereCollider sphereCollider;
        [SerializeField] private TrailRenderer trailRenderer;
        
        private Rigidbody attachedRigidbody;
        public IngredientData Data { get; private set; }

#region Lifecycle Events

        private void Awake()
        {
            attachedRigidbody = GetComponent<Rigidbody>();
        }

        private void OnDisable()
        {
            trailRenderer.Clear();
        }

#endregion

#region Methods

        public void Clear()
        {
            Data = null;
        }

        public void Despawn()
        {
            var ingredientManager = Singleton.GetOrCreateMonoBehaviour<IngredientManager>();
            ingredientManager.Release(this);
        }
        
        public void Launch(Vector3 direction, float force, float angle)
        {
            var angleRad = angle * Mathf.Deg2Rad;
            
            var velocity = new Vector3(
                direction.x * force * Mathf.Cos(angleRad),
                force * Mathf.Sin(angleRad),
                direction.z * force * Mathf.Cos(angleRad)
            );

            attachedRigidbody.linearVelocity = velocity;
        }

        public void SetData(PickableObjectData objectData)
        {
            if (objectData is null)
            {
                Clear();
                return;
            }
            
            Data = (IngredientData)objectData;
            spriteRenderer.sprite = Data.Sprite;

            var spriteSize = spriteRenderer.bounds.size;
            var maxSize = Mathf.Max(spriteSize.x, spriteSize.y);
            sphereCollider.radius = maxSize * 0.5f + ExpandColliderRadius;
        }

#endregion

#region IUsable Implementation
        
        /// <inheritdoc />
        public void Use(PlayerContext playerContext)
        {
            playerContext.Bag.Carry(Data);
        }

#endregion
    }
}