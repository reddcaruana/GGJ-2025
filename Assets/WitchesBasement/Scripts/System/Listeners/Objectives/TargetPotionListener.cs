using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Obvious.Soap;
using UnityEngine;
using WitchesBasement.Data;

namespace WitchesBasement.System
{
    internal class TargetPotionListener : MonoBehaviour
    {
        [Header("Refresh Variables")]
        [SerializeField] private FloatVariable monitorRefreshDelay;
        [SerializeField] private FloatVariable refreshInterval;
        [SerializeField] private FloatVariable refreshRandomValue;
        
        [Header("Targets")]
        [SerializeField] private PotionDataVariable targetPotion;
        [SerializeField] private ObjectiveMonitorTarget potionMonitorTarget;
        [SerializeField] private ObjectiveMonitorTarget[] ingredientTargets;

        private PotionData previousPotion;
        
#region Lifecycle Events

        private void OnEnable()
        {
            targetPotion.OnValueChanged += OnTargetPotionChanged;
        }

        private void OnDisable()
        {
            targetPotion.OnValueChanged -= OnTargetPotionChanged;
        }

        private void Reset()
        {
            ingredientTargets = GetComponentsInChildren<ObjectiveMonitorTarget>();
        }

#endregion

#region Subscriptions

        private void OnTargetPotionChanged(PotionData potionData)
        {
            if (potionData is null)
            {
                Debug.Log("Potion will not be displayed because it is null");
                return;
            }

            StartCoroutine(OnTargetPotionChangedRoutine());
        }

#endregion

#region Coroutines

        private IEnumerator OnTargetPotionChangedRoutine()
        {
            var targets = new List<ObjectiveMonitorTarget>(ingredientTargets) { potionMonitorTarget };
            
            if (previousPotion is not null)
            {
                yield return ClearSpritesInOrderRoutine(targets);
                yield return new WaitForSeconds(monitorRefreshDelay.Value);
            }
            
            targets.Remove(potionMonitorTarget);
            yield return SetSpritesInOrderRoutine(targets);
            
            potionMonitorTarget.Set(targetPotion.Value.Sprite);
            previousPotion = targetPotion.Value;
        }

        private IEnumerator ClearSpritesInOrderRoutine(List<ObjectiveMonitorTarget> targets)
        {
            var indices = Enumerable.Range(0, targets.Count).OrderBy(_ => Random.value).ToArray();
            foreach (var index in indices)
            {
                targets[index].gameObject.SetActive(true);
                targets[index].Clear();

                var interval = refreshInterval.Value + Random.value * refreshRandomValue.Value;
                yield return new WaitForSeconds(interval);
            }
        }

        private IEnumerator SetSpritesInOrderRoutine(List<ObjectiveMonitorTarget> targets)
        {
            var indices = Enumerable.Range(0, targets.Count).OrderBy(_ => Random.value).ToArray();
            foreach (var index in indices)
            {
                if (index >= targetPotion.Value.IngredientCount)
                {
                    targets[index].gameObject.SetActive(false);
                    continue;
                }
                
                targets[index].Set(targetPotion.Value.Ingredients[index].Sprite);
                
                var interval = refreshInterval.Value + Random.value * refreshRandomValue.Value;
                yield return new WaitForSeconds(interval);
            }
        }

#endregion
    }
}