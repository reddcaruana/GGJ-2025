using System.Collections.Generic;
using GlobalGameJam.Data;
using UnityEngine;

namespace GlobalGameJam.Gameplay
{
    public class CauldronObjective : MonoBehaviour
    {
        public event System.Action<PotionData> OnChanged;

        private readonly List<PotionData> availablePotions = new();
        
        public PotionData Target { get; private set; }

#region Lifecycle Events

        private void Awake()
        {
            var potionRegistry = Singleton.GetOrCreateScriptableObject<PotionRegistry>();
            
            availablePotions.Clear();
            availablePotions.AddRange(potionRegistry.Potions);
        }

#endregion

#region Methods

        public void Next()
        {
            var index = Random.Range(0, availablePotions.Count);
            var next = availablePotions[index];
            
            availablePotions.Add(Target);
            availablePotions.Remove(next);

            Target = next;
            OnChanged?.Invoke(Target);
        }

#endregion
    }
}