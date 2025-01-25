using System.Collections.Generic;
using GlobalGameJam.Data;
using UnityEngine;
using UnityEngine.UI;

namespace GlobalGameJam.Gameplay
{
    public class CauldronManager : MonoBehaviour
    {
        [SerializeField] private CauldronObjective cauldronObjective;
        [SerializeField] private IngredientCatcher ingredientCatcher;
        [SerializeField] private IngredientSequencer ingredientSequencer;

        private CauldronContext cauldronContext;

#region Lifecycle Events

        private void Awake()
        {
            cauldronContext = new CauldronContext
            {
                CauldronMixture = new CauldronMixture(),
                CauldronObjective = cauldronObjective,
                IngredientCatcher = ingredientCatcher,
                IngredientSequencer = ingredientSequencer
            };
        }

        private void OnEnable()
        {
            cauldronContext.CauldronObjective.OnChanged += cauldronContext.CauldronMixture.OnTargetPotionChanged;
            cauldronContext.IngredientSequencer.OnChanged += cauldronContext.CauldronMixture.OnExpectedIngredientChangedHandler;
            cauldronContext.IngredientCatcher.OnAdded += cauldronContext.CauldronMixture.OnIngredientAddedHandler;

            cauldronContext.CauldronMixture.OnSuccess += cauldronContext.CauldronObjective.Next;
        }

        private void OnDisable()
        {
            cauldronContext.CauldronObjective.OnChanged -= cauldronContext.CauldronMixture.OnTargetPotionChanged;
            cauldronContext.IngredientSequencer.OnChanged -= cauldronContext.CauldronMixture.OnExpectedIngredientChangedHandler;
            cauldronContext.IngredientCatcher.OnAdded -= cauldronContext.CauldronMixture.OnIngredientAddedHandler;
            
            cauldronContext.CauldronMixture.OnSuccess -= cauldronContext.CauldronObjective.Next;
        }

        private void Start()
        {
            cauldronContext.CauldronObjective.Next();
        }

#endregion

#region Methods

        public CauldronContext GetContext()
        {
            return cauldronContext;
        }

#endregion
    }
}