using System.Collections.Generic;
using Obvious.Soap;
using UnityEngine;
using WitchesBasement.Data;

namespace WitchesBasement.System
{
    internal class PotionSequencer : MonoBehaviour
    {
        [Header("Registry")]
        [SerializeField] private PotionRegistry registry;
        
        [Header("Variables")]
        [SerializeField] private PotionDataVariable targetPotion;
        [SerializeField] private ScriptableListIngredientData requiredIngredients;

        [Header("Events")]
        [SerializeField] private ScriptableEventPotionData targetPotionChangedEvent;

        [Header("Subscriptions")]
        [SerializeField] private ScriptableEventNoParam nextPotionEvent;
        
        private readonly List<PotionData> potions = new();
        
#region Lifecycle Events

        private void Awake()
        {
            potions.Clear();
            potions.AddRange(registry.Potions);
        }

        private void OnEnable()
        {
            nextPotionEvent.OnRaised += Next;
        }

        private void OnDisable()
        {
            nextPotionEvent.OnRaised -= Next;
        }

#endregion
        
#region Methods

        private void Next()
        {
            var index = Random.Range(0, potions.Count);
            
            var current = targetPotion.Value;
            var next = potions[index];

            if (current is not null)
            {
                potions.Add(current);
            }
            
            potions.Remove(next);
            targetPotion.Value = next;
            
            requiredIngredients.Clear();
            requiredIngredients.AddRange(next.Ingredients);
            
            targetPotionChangedEvent.Raise(targetPotion.Value);
        }

#endregion
    }
}