using GlobalGameJam.Data;

namespace GlobalGameJam.Gameplay
{
    public interface IIngredientData
    {
        IngredientData Data { get; }

        void SetData(IngredientData data);
    }
}