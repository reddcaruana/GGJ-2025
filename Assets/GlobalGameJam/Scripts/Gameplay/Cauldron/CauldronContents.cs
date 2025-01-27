using GlobalGameJam.Data;
using UnityEngine;

namespace GlobalGameJam.Gameplay
{
    public class CauldronContents : MonoBehaviour
    {
        [SerializeField] private CauldronIngredient[] ingredients;
        [SerializeField] private float minRadius = 1.0f;
        [SerializeField] private float maxRadius = 2.0f;
        
        private int index;
        
#region Lifecycle Events

        private void Start()
        {
            foreach (var ingredient in ingredients)
            {
                ingredient.SetAnimationOffset(Random.value * 2.0f);
            }
        }

        private void Reset()
        {
            ingredients = GetComponentsInChildren<CauldronIngredient>();
        }

#endregion

#region Methods

        public void Submerge()
        {
            foreach (var ingredient in ingredients)
            {
                ingredient.Submerge();
            }

            index = 0;
        }

#endregion

#region Event Handlers

        public void OnAddedHandler(IngredientData ingredient)
        {
            if (index >= ingredients.Length)
            {
                return;
            }
            
            ingredients[index].Emerge(ingredient);
            index++;
        }

#endregion
    }
}