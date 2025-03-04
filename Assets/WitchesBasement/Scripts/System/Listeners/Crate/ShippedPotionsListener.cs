using UnityEngine;
using WitchesBasement.Data;

namespace WitchesBasement.System
{
    internal class ShippedPotionsListener : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer[] potionSpriteRenderers;
        [SerializeField] private ScriptableListPotionData potionList;

#region Lifecycle Events

        private void Awake()
        {
            foreach (var spriteRenderer in potionSpriteRenderers)
            {
                spriteRenderer.sprite = null;
            }
        }

        private void OnEnable()
        {
            potionList.OnItemAdded += OnPotionAdded;
        }

        private void OnDisable()
        {
            potionList.OnItemAdded -= OnPotionAdded;
        }

        private void Reset()
        {
            potionSpriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        }

#endregion

#region Subscriptions
        
        private void OnPotionAdded(PotionData potionData)
        {
            var index = potionList.Count - 1;
            potionSpriteRenderers[index].sprite = potionData.Sprite;
        }

#endregion
    }
}