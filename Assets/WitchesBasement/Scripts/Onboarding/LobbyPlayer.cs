using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WitchesBasement.Data;
using WitchesBasement.Events;
using WitchesBasement.Players;

namespace WitchesBasement.Onboarding
{
    [RequireComponent(
        typeof(Animator),
        typeof(CanvasGroup),
        typeof(LobbyPlayerInputBinding))]
    internal class LobbyPlayer : MonoBehaviour
    {
        private static readonly int PasswordInAnimatorHash = Animator.StringToHash("Password In");
        private static readonly int PasswordErrorAnimatorHash = Animator.StringToHash("Password Error");
        private static readonly int LogInAnimatorHash = Animator.StringToHash("Log In");
        private static readonly int LoggedInAnimatorHash = Animator.StringToHash("Logged In");
        private static readonly int LoggedOutAnimatorHash = Animator.StringToHash("Logged Out");
        
        [SerializeField] private Image profileImage;
        [SerializeField] private TMP_Text usernameText;
        [SerializeField] private TMP_InputField passwordField;

        [SerializeField] private float passwordInputDuration = 1.5f;

        public LobbyPlayerInputBinding Binding { get; private set; }
        private ProfileData data;

        private Animator animator;
        private CanvasGroup canvasGroup;

        private int ID = -1;
        private int attempts = 1;
        private bool canLogIn;
        private Coroutine activeCoroutine;

#region Lifecycle Events

        private void Awake()
        {
            animator = GetComponent<Animator>();
            canvasGroup = GetComponent<CanvasGroup>();

            Binding = GetComponent<LobbyPlayerInputBinding>();

            passwordField.asteriskChar = '•';
        }

        private void OnEnable()
        {
            canvasGroup.alpha = 1.0f;

            Binding.OnBind += OnBindHandler;
            Binding.OnJoined += OnJoinHandler;
            Binding.OnLeft += OnLeftHandler;
        }

        private void OnDisable()
        {
            canvasGroup.alpha = 0.5f;

            Binding.OnBind -= OnBindHandler;
            Binding.OnJoined -= OnJoinHandler;
            Binding.OnLeft -= OnLeftHandler;
            
            canLogIn = false;
        }

        private void Reset()
        {
            profileImage = GetComponentInChildren<Image>();
            usernameText = GetComponentInChildren<TMP_Text>();
            passwordField = GetComponentInChildren<TMP_InputField>();
        }

#endregion

#region Methods

        private float[] CreateInputIntervals(int count)
        {
            var factors = new int[count];
            for (var i = 0; i < count; i++)
            {
                factors[i] = Random.Range(1, 5);
            }

            var sum = factors.Sum();
            var intervals = new float[count];
            for (var i = 0; i < count; i++)
            {
                intervals[i] = (float)factors[i] / sum * passwordInputDuration;
            }

            return intervals;
        }

        public void Setup(ProfileData profileData)
        {
            data = profileData;

            profileImage.sprite = profileData.Sprite;
            usernameText.text = profileData.Username;

            passwordField.text = string.Empty;
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

#region Action Handlers

        private void OnBindHandler(int playerID)
        {
            ID = playerID;
            animator.Play(PasswordInAnimatorHash);
        }
        
        private void OnJoinHandler()
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
            EventBus<PlayerStatusChangedEvent>.Raise(new PlayerStatusChangedEvent
            {
                Status = PlayerStatusChangedEvent.StatusType.Ready,
                PlayerID = ID
            });
        }

        private void OnLeftHandler()
        {
            passwordField.text = string.Empty;
            canLogIn = false;
            attempts = data.Attempts;

            if (activeCoroutine is not null)
            {
                StopCoroutine(activeCoroutine);
                activeCoroutine = null;
            }
            
            animator.Play(LoggedOutAnimatorHash);
            enabled = false;
        }

#endregion

#region Coroutines

        private IEnumerator WritePasswordRoutine()
        {
            canLogIn = false;
            passwordField.text = string.Empty;

            var passwordLength = data.PasswordLength;
            if (attempts > 1)
            {
                passwordLength = Random.Range(passwordLength + 4, passwordLength + 8);
            }

            var intervals = CreateInputIntervals(passwordLength);
            yield return new WaitForSeconds(0.5f);

            for (var i = 0; i < passwordLength; i++)
            {
                passwordField.text += '0';
                yield return new WaitForSeconds(intervals[i]);
            }
            
            animator.Play(LogInAnimatorHash);
            
            activeCoroutine = null;
            canLogIn = true;
        }

#endregion
    }
}