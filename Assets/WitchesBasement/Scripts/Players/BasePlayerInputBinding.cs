using UnityEngine;
using UnityEngine.InputSystem;

namespace WitchesBasement.Players
{
    public abstract class BasePlayerInputBinding : MonoBehaviour
    {
        public event System.Action<int> OnBind;
        public event System.Action OnRelease;
        
        protected PlayerInput Input { get; private set; }
        protected int ID { get; private set; }

#region Binding Methods

        public virtual void Bind(int playerID)
        {
            var dataManager = Singleton.GetOrCreateMonoBehaviour<PlayerDataManager>();
            
            ID = playerID;
            Input = dataManager.FindInputByID(ID);
            
            OnBind?.Invoke(ID);
        }

        public virtual void Release()
        {
            Input = null;
            ID = -1;
            
            OnRelease?.Invoke();
        }

#endregion
    }
}