using System.Collections;
using System.Collections.Generic;
using GlobalGameJam.Data;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace GlobalGameJam.Gameplay
{
    public class IngredientSequencer : MonoBehaviour
    {
        private static readonly int EmissionColorID = Shader.PropertyToID("_EmissionColor");
        public event System.Action<IngredientData> OnChanged;
        
        [Header("Cycling")]
        [SerializeField] private float cycleDuration = 3f;

        [Header("Visual Feedback")]
        [SerializeField] private float colorChangeDuration = 0.5f;
        [SerializeField] private Renderer cauldronRenderer;
        [SerializeField] private Light cauldronGlow;
        
        public IngredientData Current { get; private set; }
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
                if (Current is not null)
                {
                    ingredientQueue.Enqueue(Current);
                }

                Current = ingredientQueue.Dequeue();
                StartCoroutine(ChangeMaterialColorRoutine());
                OnChanged?.Invoke(Current);
                
                yield return new WaitForSeconds(cycleDuration);
            }
        }

        private IEnumerator ChangeMaterialColorRoutine()
        {
            if (Current is null)
            {
                yield break;
            }

            var targetColor = Current.Color;
            var materialColor = cauldronRenderer.material.GetColor(EmissionColorID);

            var progress = 0f;

            while (progress < colorChangeDuration)
            {
                progress += Time.deltaTime;
                var colorValue = Color.Lerp(materialColor, targetColor, progress / colorChangeDuration);
                
                cauldronRenderer.material.SetColor(EmissionColorID, colorValue);
                cauldronGlow.color = colorValue;
                yield return null;
            }
        }

#endregion
    }
}