using Obvious.Soap;
using UnityEngine;
using WitchesBasement.Data;

namespace WitchesBasement.System
{
    public class PotionSpawnListener : MonoBehaviour
    {
        [SerializeField] private FloatVariable throwForce;
        [SerializeField] private FloatVariable angle;
        [SerializeField] private Direction direction;

        [SerializeField] private ScriptableEventPotionData spawnEvent;

#region Lifecycle Events

        private void OnEnable()
        {
            spawnEvent.OnRaised += OnSpawnPotion;
        }

        private void OnDisable()
        {
            spawnEvent.OnRaised -= OnSpawnPotion;
        }

#endregion

#region Subscriptions

        private void OnSpawnPotion(PotionData potionData)
        {
            var manager = Singleton.GetOrCreateMonoBehaviour<ItemManager>();
            var item = manager.Generate(potionData, transform);
            item.Throw(direction.ToVector(), throwForce.Value, angle.Value);

        }

#endregion
    }
}