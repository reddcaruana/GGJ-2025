using GlobalGameJam.Data;
using GlobalGameJam.Events;
using UnityEngine;

namespace GlobalGameJam.Gameplay
{
    /// <summary>
    /// Represents a potion that can be carried, used, thrown, and shipped in the game.
    /// </summary>
    public class Potion : Carryable, IThrowable, IUsable, IPotionData, IShippable
    {
        private static PotionRegistry potionRegistry;
        
        /// <summary>
        /// The amount to expand the collider radius.
        /// </summary>
        private const float ExpandColliderRadius = 0.1f;

#region Methods

        /// <summary>
        /// Clears the potion data.
        /// </summary>
        public void Clear()
        {
            Data = null;
        }

#endregion

#region Overrides of Carryable

        /// <inheritdoc />
        protected override void Awake()
        {
            base.Awake();
            potionRegistry ??= Singleton.GetOrCreateScriptableObject<PotionRegistry>();
        }

        /// <inheritdoc />
        public override void Despawn()
        {
            var potionManager = Singleton.GetOrCreateMonoBehaviour<PotionPool>();
            potionManager.Release(this);
        }

        /// <inheritdoc />
        protected override void OnSetLevelModeEventHandler(LevelEvents.SetMode @event)
        {
            if (@event.Mode is not LevelMode.End)
            {
                return;
            }
            
            EventBus<ScoreEvents.Add>.Raise(new ScoreEvents.Add
            {
                Litter = 1,
                Deductions = Data.LitterDeduction
            });
        }

#endregion

#region Implementation of IUsable

        /// <inheritdoc />
        public void Use(PlayerContext playerContext)
        {
            playerContext.Bag.Carry(Data);

            if (Data == potionRegistry.Pootion)
            {
                EventBus<TrashcanEvents.Excite>.Raise(TrashcanEvents.Excite.Default);
            }
            
            Despawn();
        }

#endregion

#region Implementation of IPotionData

        /// <inheritdoc />
        public PotionData Data { get; private set; }

        /// <inheritdoc />
        public void SetData(PotionData data)
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

#region Implementation of IShippable

        /// <inheritdoc />
        public void Place()
        {
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