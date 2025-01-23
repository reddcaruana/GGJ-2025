namespace GlobalGameJam.Gameplay
{
    public class Throw
    {
        public void Drop(Bag bag, Direction direction)
        {
            var ingredientManager = Singleton.GetOrCreateMonoBehaviour<IngredientManager>();
            var ingredient = ingredientManager.Generate(bag.Contents, bag.GetAnchor());
            
            // TODO: Throw the object
            bag.Clear();
        }
    }
}