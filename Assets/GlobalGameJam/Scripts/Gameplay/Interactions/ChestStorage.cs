using GlobalGameJam.Data;
using UnityEngine;

namespace GlobalGameJam.Gameplay
{
    public class ChestStorage : MonoBehaviour
    {
        private static readonly int CollectTriggerHash = Animator.StringToHash("Collect");
        
        [SerializeField] private Transform ingredientAnchor;
        [SerializeField] private SpriteRenderer ingredientSprite;
        [SerializeField] private Light glowLight;
        
        [SerializeField] private CarryableData data;
        private Carryable storedInstance;

        private Animator animator;

#region Lifecycle Events

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

#endregion

#region Methods

        public void ClearData()
        {
            if (data is null)
            {
                return;
            }
            
            if (storedInstance is not null)
            {
                if (storedInstance is Ingredient ingredient)
                {
                    ingredient.OnUsed -= OnUsedHandler;
                }
                
                Destroy(storedInstance);
                storedInstance = null;
            }
            data = null;
        }

        public void SetData(CarryableData newData)
        {
            ClearData();
            
            data = newData;
            switch (data)
            {
                case IngredientData ingredientData:
                    var ingredientManager = Singleton.GetOrCreateMonoBehaviour<IngredientManager>();
                    storedInstance = ingredientManager.Generate(ingredientData, ingredientAnchor);

                    ingredientSprite.sprite = ingredientData.Sprite;
                    glowLight.color = ingredientData.Color;
                    
                    break;
            }

            if (storedInstance is Ingredient ingredient)
            {
                ingredient.OnUsed += OnUsedHandler;
            }
            
            storedInstance.AttachedRigidbody.isKinematic = true;
        }

#endregion

#region Event Handlers

        private void OnUsedHandler()
        {
            animator.Play(CollectTriggerHash);
        }

#endregion
    }
}