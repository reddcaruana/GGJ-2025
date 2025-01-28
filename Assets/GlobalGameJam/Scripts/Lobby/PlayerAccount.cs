using System.Collections;
using System.Linq;
using GlobalGameJam.Data;
using GlobalGameJam.Gameplay;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace GlobalGameJam.Lobby
{
    [RequireComponent(typeof(Animator), typeof(CanvasGroup))]
    public class PlayerAccount : MonoBehaviour, IBindable
    {
        private static readonly int PasswordInAnimatorHash = Animator.StringToHash("Password In");
        private static readonly int PasswordErrorAnimatorHash = Animator.StringToHash("Password Error");
        private static readonly int LogInAnimatorHash = Animator.StringToHash("Log In");
        private static readonly int LoggedInAnimatorHash = Animator.StringToHash("Logged In");
        private static readonly int LoggedOutAnimatorHash = Animator.StringToHash("Logged Out");
        
        [SerializeField] private Image image;
        [SerializeField] private TMP_Text username;
        [SerializeField] private TMP_InputField input;

        [SerializeField] private float passwordDuration = 1.5f;

        private ProfileData data;
        
        private Animator animator;
        private CanvasGroup canvasGroup;

        private PlayerInput playerInput;
        private InputAction joinAction;
        private InputAction leaveAction;

        private int attempts = 1;
        private bool canLogIn;
        private Coroutine activeCoroutine;
        private int playerID;

#region Lifecycle Events

        /// <summary>
        /// Called when the script instance is being loaded.
        /// </summary>
        private void Awake()
        {
            animator = GetComponent<Animator>();
            canvasGroup = GetComponent<CanvasGroup>();
            
            input.asteriskChar = '•';
        }

        /// <summary>
        /// Resets the component to its default state.
        /// </summary>
        private void Reset()
        {
            image = GetComponentInChildren<Image>();
            username = GetComponentInChildren<TMP_Text>();
        }

        /// <summary>
        /// Called when the object becomes enabled and active.
        /// </summary>
        private void OnEnable()
        {
            canvasGroup.alpha = 1.0f;
        }

        /// <summary>
        /// Called when the object becomes disabled.
        /// </summary>
        private void OnDisable()
        {
            canvasGroup.alpha = 0.5f;
            canLogIn = false;
        }

#endregion

#region Methods

        private float[] CreateIntervals(int count)
        {
            var factors = new int[count];
            for (var i = 0; i < count; i++)
            {
                factors[i] = Random.Range(1, 5);
            }

            var total = factors.Sum();
            var intervals = new float[count];
            for (var i = 0; i < count; i++)
            {
                intervals[i] = (float)factors[i] / total * passwordDuration;
            }

            return intervals;
        }

        /// <summary>
        /// Sets up the player account with the provided profile data.
        /// </summary>
        /// <param name="profileData">The profile data to set up.</param>
        public void Setup(ProfileData profileData)
        {
            data = profileData;
            
            image.sprite = profileData.Sprite;
            username.text = profileData.Username;

            attempts = profileData.Attempts;
            
            input.text = string.Empty;
        }

        public void WritePassword()
        {
            if (activeCoroutine is not null)
            {
                StopCoroutine(activeCoroutine);
            }
            
            activeCoroutine = StartCoroutine(WritePasswordRoutine());
        }

#endregion

#region IBindable Implementation

        /// <summary>
        /// Binds the player input to the specified player number.
        /// </summary>
        /// <param name="playerNumber">The player number to bind.</param>
        public void Bind(int playerNumber)
        {
            var playerDataManager = Singleton.GetOrCreateMonoBehaviour<PlayerDataManager>();
            playerInput = playerDataManager.GetPlayerInput(playerNumber);
            playerID = playerNumber;

            playerInput.SwitchCurrentActionMap("Lobby");

            leaveAction = playerInput.currentActionMap.FindAction("Leave");
            leaveAction.started += LeaveHandler;

            joinAction = playerInput.currentActionMap.FindAction("Join");
            joinAction.started += JoinHandler;
            
            animator.Play(PasswordInAnimatorHash);
        }

        /// <summary>
        /// Releases the player input and unbinds the leave action.
        /// </summary>
        public void Release()
        {
            if (leaveAction is not null)
            {
                leaveAction.started -= LeaveHandler;
            }
            leaveAction = null;

            if (joinAction is not null)
            {
                joinAction.started -= JoinHandler;
            }
            joinAction = null;

            playerInput = null;
        }

#endregion

#region Action Handlers

        private void JoinHandler(InputAction.CallbackContext context)
        {
            if (canLogIn == false)
            {
                return;
            }
            
            if (attempts > 1)
            {
                attempts--;
                animator.Play(PasswordErrorAnimatorHash);
                return;
            }
            
            animator.Play(LoggedInAnimatorHash);

            var levelManager = Singleton.GetOrCreateMonoBehaviour<LevelManager>();
            levelManager.AddPlayer(playerID);
        }
        
        /// <summary>
        /// Handles the leave action when it is started.
        /// </summary>
        /// <param name="context">The callback context of the input action.</param>
        private void LeaveHandler(InputAction.CallbackContext context)
        {
            input.text = string.Empty;
            canLogIn = false;
            attempts = data.Attempts;

            if (activeCoroutine is not null)
            {
                StopCoroutine(activeCoroutine);
                activeCoroutine = null;
            }
            
            var targetPlayerInput = playerInput;
            Release();
            
            animator.Play(LoggedOutAnimatorHash);

            Destroy(targetPlayerInput.gameObject);
        }

#endregion

#region Coroutines

        private IEnumerator WritePasswordRoutine()
        {
            canLogIn = false;
            input.text = string.Empty;
            
            var passwordLength = data.PasswordLength;
            if (attempts > 1)
            {
                passwordLength = Random.Range(passwordLength + 4, passwordLength + 8);
            }

            var intervals = CreateIntervals(passwordLength);
            yield return new WaitForSeconds(0.5f);

            for (var i = 0; i < passwordLength; i++)
            {
                input.text += '0';
                yield return new WaitForSeconds(intervals[i]);
            }
            
            animator.Play(LogInAnimatorHash);
            
            activeCoroutine = null;
            canLogIn = true;
        }

#endregion
    }
}