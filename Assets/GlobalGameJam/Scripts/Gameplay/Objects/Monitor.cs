using System;
using GlobalGameJam.Events;
using UnityEngine;

namespace GlobalGameJam.Gameplay
{
    public class Monitor : MonoBehaviour
    {
        /// <summary>
        /// Array of transforms representing the screens.
        /// </summary>
        [SerializeField] private Transform[] screenTransforms;

        /// <summary>
        /// Event binding for the ChangeScreens event.
        /// </summary>
        private EventBinding<LevelEvents.SetMonitors> onChangeScreensEventBinding;

#region Lifecycle Events

        /// <summary>
        /// Called when the script instance is being loaded.
        /// Initializes the event binding for the ChangeScreens event.
        /// </summary>
        private void Awake()
        {
            onChangeScreensEventBinding = new EventBinding<LevelEvents.SetMonitors>(OnChangeScreensEventHandler);
        }

        /// <summary>
        /// Called when the script instance is enabled.
        /// Registers the ChangeScreens event binding.
        /// </summary>
        private void OnEnable()
        {
            EventBus<LevelEvents.SetMonitors>.Register(onChangeScreensEventBinding);
        }

        /// <summary>
        /// Called when the script instance is disabled.
        /// Deregisters the ChangeScreens event binding.
        /// </summary>
        private void OnDisable()
        {
            EventBus<LevelEvents.SetMonitors>.Deregister(onChangeScreensEventBinding);
        }

#endregion

#region Methods

        /// <summary>
        /// Sets the z-position of the screen transforms based on the given index.
        /// The screen at the specified index will have a z-position of 0, while others will have a z-position of 1.
        /// </summary>
        /// <param name="index">The index of the screen to set to the front.</param>
        private void SetIndex(int index)
        {
            if (index >= screenTransforms.Length)
            {
                return;
            }

            for (var i = 0; i < screenTransforms.Length; i++)
            {
                var screenTransform = screenTransforms[i];
                if (!screenTransform)
                {
                    continue;
                }
                
                var position = screenTransform.localPosition;
                position.z = i == index ? 0 : 1;
                screenTransform.localPosition = position;
            }
        }

#endregion

#region Event Handlers

        /// <summary>
        /// Handles the ChangeScreens event.
        /// Sets the screen index based on the event's screen setup mode.
        /// </summary>
        /// <param name="event">The ChangeScreens event.</param>
        private void OnChangeScreensEventHandler(LevelEvents.SetMonitors @event)
        {
            switch (@event.Mode)
            {
                case MonitorMode.Intro:
                    SetIndex(0);
                    break;
                case MonitorMode.Gameplay:
                    SetIndex(1);
                    break;
                case MonitorMode.Outro:
                    SetIndex(2);
                    break;
                case MonitorMode.Crash:
                    SetIndex(3);
                    break;
            }
        }

#endregion
    }
}