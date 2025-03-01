using WitchesBasement.Data;

namespace WitchesBasement.System
{
    internal interface IUsable : IItemData
    {
        ItemData Use();
    }
}