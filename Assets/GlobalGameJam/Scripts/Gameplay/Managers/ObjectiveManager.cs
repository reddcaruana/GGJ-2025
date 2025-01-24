using System.Collections.Generic;
using GlobalGameJam.Data;
using UnityEngine;

namespace GlobalGameJam.Gameplay
{
    public class ObjectiveManager : MonoBehaviour
    {
        public event System.Action<PotionData> OnChanged;

        private List<PotionData> availablePotions;
        
        public PotionData TargetPotion { get; private set; }

#region Lifecycle Events

        private void Awake()
        {
            var potionRegistry = Singleton.GetOrCreateScriptableObject<PotionRegistry>();
            availablePotions = new List<PotionData>(potionRegistry.Potions);
        }

#endregion
        
#region Methods

        public void Next()
        {
            var index = Random.Range(0, availablePotions.Count);
            var next = availablePotions[index];
            
            availablePotions.Add(TargetPotion);
            availablePotions.Remove(next);

            TargetPotion = next;
            OnChanged?.Invoke(TargetPotion);
        }

#endregion
    }
}