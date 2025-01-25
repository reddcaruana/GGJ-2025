using System.Collections;
using System.Collections.Generic;
using GlobalGameJam.Data;
using GlobalGameJam.Level;
using UnityEngine;

namespace GlobalGameJam.Gameplay
{
    public class IngredientCatcher : MonoBehaviour
    {
        public event System.Action<IngredientData> OnAdded;
        
        private void OnTriggerEnter(Collider other)
        {
            var ingredient = other.GetComponentInParent<Ingredient>();
            if (ingredient is null)
            {
                return;
            }

            var data = ingredient.Data;
            ingredient.Despawn();
            
            OnAdded?.Invoke(data);
        }
    }
}