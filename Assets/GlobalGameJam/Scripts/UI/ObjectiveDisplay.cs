using GlobalGameJam.Data;
using UnityEngine;
using UnityEngine.UI;

namespace GlobalGameJam.UI
{
    public class ObjectiveDisplay : MonoBehaviour
    {
        [SerializeField] private Image potionImage;
        [SerializeField] private Image[] ingredientImages;

#region Event Handlers
        
        private void TargetPotionChangedHandler(PotionData data)
        {
            if (data is null)
            {
                Debug.Log("Data is null.");
                return;
            }
            
            potionImage.sprite = data.Sprite;
            potionImage.preserveAspect = true;

            for (var i = 0; i < ingredientImages.Length; i++)
            {
                if (i >= data.Ingredients.Length)
                {
                    ingredientImages[i].sprite = null;
                    ingredientImages[i].color = Color.clear;
                    continue;
                }

                ingredientImages[i].sprite = data.Ingredients[i].Sprite;
                    ingredientImages[i].color = Color.white;
            }
        }

#endregion
    }
}