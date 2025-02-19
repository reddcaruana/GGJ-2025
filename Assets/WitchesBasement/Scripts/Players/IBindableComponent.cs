using UnityEngine.InputSystem;

namespace WitchesBasement.Players
{
    public interface IBindableComponent
    {
        void Bind(PlayerInput playerInput);
        void Release();
    }
}