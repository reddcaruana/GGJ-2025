using GlobalGameJam.Data;
using UnityEngine;

namespace GlobalGameJam.Gameplay
{
    /// <summary>
    /// Manages the storage of ingredients in a chest.
    /// </summary>
    public class ChestStorage : MonoBehaviour
    {
        /// <summary>
        /// Hash for the collect trigger in the animator.
        /// </summary>
        private static readonly int CollectTriggerHash = Animator.StringToHash("Collect");

        /// <summary>
        /// The transform where the ingredient will be anchored.
        /// </summary>
        [SerializeField] private Transform ingredientAnchor;

        /// <summary>
        /// The sprite renderer for the ingredient.
        /// </summary>
        [SerializeField] private SpriteRenderer ingredientSprite;

        /// <summary>
        /// The light that glows when an ingredient is stored.
        /// </summary>
        [SerializeField] private Light glowLight;

        /// <summary>
        /// The data for the carryable item stored in the chest.
        /// </summary>
        [SerializeField] private CarryableData data;

        /// <summary>
        /// The instance of the carryable item currently stored.
        /// </summary>
        private Carryable storedInstance;

        /// <summary>
        /// The animator component for the chest.
        /// </summary>
        private Animator animator;

#region Lifecycle Events

        /// <summary>
        /// Initializes the animator component.
        /// </summary>
        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

#endregion

#region Methods

        /// <summary>
        /// Clears the data of the stored item.
        /// </summary>
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

        /// <summary>
        /// Sets the data for a new carryable item and updates the chest.
        /// </summary>
        /// <param name="newData">The new carryable data.</param>
        public void SetData(CarryableData newData)
        {
            ClearData();

            data = newData;
            switch (data)
            {
                case IngredientData ingredientData:
                    var ingredientManager = Singleton.GetOrCreateMonoBehaviour<IngredientPool>();
                    storedInstance = ingredientManager.Generate(ingredientData, ingredientAnchor);

                    ingredientSprite.sprite = ingredientData.CrateLabel ?? ingredientData.Sprite;
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

        /// <summary>
        /// Handles the event when the stored ingredient is used.
        /// </summary>
        private void OnUsedHandler()
        {
            animator.Play(CollectTriggerHash);
        }

#endregion
    }
}