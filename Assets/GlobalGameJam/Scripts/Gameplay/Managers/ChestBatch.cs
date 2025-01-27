using System.Linq;
using GlobalGameJam.Data;
using UnityEngine;

namespace GlobalGameJam.Gameplay
{
    /// <summary>
    /// Manages a batch of chests, allowing for setting ingredients in each chest.
    /// </summary>
    public class ChestBatch : MonoBehaviour
    {
        /// <summary>
        /// Array of chest storage components.
        /// </summary>
        [SerializeField] private ChestStorage[] chests;

#region Lifecycle Events

        /// <summary>
        /// Resets the chest array to the children ChestStorage components.
        /// </summary>
        private void Reset()
        {
            chests = GetComponentsInChildren<ChestStorage>();
        }

#endregion

#region Methods

        /// <summary>
        /// Sets the ingredients in the chests, randomizing their order.
        /// </summary>
        /// <param name="ingredients">Array of ingredients to set in the chests.</param>
        public void SetChests(IngredientData[] ingredients)
        {
            var randomizedIngredients = ingredients.OrderBy(_ => Random.value).ToArray();
            for (var i = 0; i < chests.Length; i++)
            {
                if (i >= chests.Length)
                {
                    chests[i].gameObject.SetActive(false);
                    continue;
                }

                chests[i].SetData(randomizedIngredients[i]);
            }
        }

#endregion
    }
}