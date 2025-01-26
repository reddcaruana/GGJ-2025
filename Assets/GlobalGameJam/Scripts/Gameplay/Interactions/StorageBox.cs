using GlobalGameJam.Data;
using UnityEngine;

namespace GlobalGameJam.Gameplay
{
    public class StorageBox : MonoBehaviour, IUsable
    {
        private static readonly int MainTex = Shader.PropertyToID("_MainTex");

        [SerializeField] private CarryableData carryableData;
        [SerializeField] private Transform ingredientAnchor;
        [SerializeField] private SpriteRenderer ingredientSprite;
        [SerializeField] private Light glowLight;
        
        private Carryable storedInstance;

#region Lifecycle Events

        private void Start()
        {
            switch (carryableData)
            {
                case IngredientData ingredientData:
                    var ingredientManager = Singleton.GetOrCreateMonoBehaviour<IngredientManager>();
                    storedInstance = ingredientManager.Generate(ingredientData, ingredientAnchor);

                    ingredientSprite.sprite = ingredientData.Sprite;
                    glowLight.color = ingredientData.Color;
                    break;
            }
            
            storedInstance.AttachedRigidbody.isKinematic = true;
        }

#endregion
        
#region IUsable Implementation
        
        /// <inheritdoc />
        public void Use(PlayerContext playerContext)
        {
            if (playerContext.Bag.Contents is not null)
            {
                return;
            }

            playerContext.Bag.Carry(carryableData);
        }

#endregion
    }
}