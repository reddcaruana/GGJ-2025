using GlobalGameJam.Data;

namespace GlobalGameJam.Gameplay
{
    public interface IPotionData
    {
        PotionData Data { get; }

        void SetData(PotionData data);
    }
}