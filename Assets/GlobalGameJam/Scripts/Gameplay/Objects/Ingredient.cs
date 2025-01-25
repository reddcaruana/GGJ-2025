using GlobalGameJam.Data;
using UnityEngine;

namespace GlobalGameJam.Gameplay
{
    public class Ingredient : Carryable, IUsable, IIngredientData, IThrowable
    {
        private const float ExpandColliderRadius = 0.1f;

#region Methods

        public void Clear()
        {
            Data = null;
        }

#endregion

#region Overrides of Carryable

        /// <inheritdoc />
        public override void Despawn()
        {
            var ingredientManager = Singleton.GetOrCreateMonoBehaviour<IngredientManager>();
            ingredientManager.Release(this);
        }

#endregion

#region Implementation of IUsable

        /// <inheritdoc />
        public void Use(PlayerContext playerContext)
        {
            playerContext.Bag.Carry(Data);
            
            if (AttachedRigidbody.isKinematic == false)
            {
                Despawn();
            }
        }

#endregion

#region Implementation of IIngredientData

        /// <inheritdoc />
        public IngredientData Data { get; private set; }

        /// <inheritdoc />
        public void SetData(IngredientData data)
        {
            if (data is null)
            {
                Clear();
                return;
            }

            Data = data;
            spriteRenderer.sprite = Data.Sprite;

            var spriteSize = spriteRenderer.bounds.size;
            var maxSize = Mathf.Max(spriteSize.x, spriteSize.y);
            sphereCollider.radius = maxSize * 0.5f + ExpandColliderRadius;
        }

#endregion

#region Implementation of IThrowable

        /// <inheritdoc />
        public void Throw(Vector3 direction, float force, float angle)
        {
            var angleRad = angle * Mathf.Deg2Rad;

            var velocity = new Vector3(
                direction.x * force * Mathf.Cos(angleRad),
                force * Mathf.Sin(angleRad),
                direction.z * force * Mathf.Cos(angleRad)
            );

            AttachedRigidbody.linearVelocity = velocity;
        }

#endregion
    }
}