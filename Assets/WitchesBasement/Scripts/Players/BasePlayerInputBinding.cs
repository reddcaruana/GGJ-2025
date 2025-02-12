using UnityEngine;
using UnityEngine.InputSystem;

namespace WitchesBasement.Players
{
    public abstract class BasePlayerInputBinding : MonoBehaviour
    {
        protected PlayerInput Input { get; private set; }
        protected int ID { get; private set; }

#region Binding Methods

        public virtual void Bind(int playerID)
        {
            var dataManager = Singleton.GetOrCreateMonoBehaviour<PlayerDataManager>();
            
            ID = playerID;
            Input = dataManager.FindInputByID(ID);
        }

        public virtual void Release()
        {
            Input = null;
            ID = -1;
        }

#endregion
    }
}