using System.Collections;
using System.Collections.Generic;
using GlobalGameJam.Data;
using UnityEngine;
namespace GlobalGameJam.Gameplay
{
    public class IngredientSequencer : MonoBehaviour
    {
        [Header("Timing Values")]
        [SerializeField] private float cycleDuration = 1.75f;
        [SerializeField] private float colorChangeDuration = 0.15f;

        private IngredientData current;
        private Queue<IngredientData> ingredientQueue;
        
#region Lifecycle Events

        private void Awake()
        {
            var registry = Singleton.GetOrCreateScriptableObject<IngredientRegistry>();
            ingredientQueue = new Queue<IngredientData>(registry.Ingredients);
        }

        private void Start()
        {
            StartCoroutine(IngredientCycleRoutine());
        }

#endregion

#region Coroutines

        private IEnumerator IngredientCycleRoutine()
        {
            while (true)
            {
                if (current is not null)
                {
                    ingredientQueue.Enqueue(current);
                }

                current = ingredientQueue.Dequeue();
                EventBus<CauldronEvents.ChangedExpectedIngredient>.Raise(new CauldronEvents.ChangedExpectedIngredient
                {
                    Ingredient = current,
                    ColorChangeDuration = colorChangeDuration
                });
                
                yield return new WaitForSeconds(cycleDuration);
            }
        }

#endregion
    }
}