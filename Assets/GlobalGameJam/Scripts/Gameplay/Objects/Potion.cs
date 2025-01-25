using GlobalGameJam.Data;

namespace GlobalGameJam.Gameplay
{
    public class Potion : Carryable, IUsable, IPotionData, IShippable
    {
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
        }

#endregion

#region Implementation of IUsable

        /// <inheritdoc />
        public void Use(PlayerContext playerContext)
        {
        }

#endregion

#region Implementation of IPotionData

        /// <inheritdoc />
        public PotionData Data { get; private set; }

        /// <inheritdoc />
        public void SetData(PotionData data)
        {
        }

#endregion

#region Implementation of IShippable

        /// <inheritdoc />
        public void Place()
        {
        }

#endregion
    }
}