using System.Linq;
using GlobalGameJam.Data;
using UnityEngine;

namespace GlobalGameJam.Gameplay
{
    public class ChestBatch : MonoBehaviour
    {
        [SerializeField] private ChestStorage[] chests;

#region Lifecycle Events

        private void Reset()
        {
            chests = GetComponentsInChildren<ChestStorage>();
        }

#endregion

#region Methods

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