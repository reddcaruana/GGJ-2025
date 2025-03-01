namespace WitchesBasement.Data
{
    public interface IItemData
    {
        ItemData Data { get; }

        void SetData(ItemData itemData);
    }
}