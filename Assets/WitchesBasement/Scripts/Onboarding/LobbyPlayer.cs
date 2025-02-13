using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WitchesBasement.Data;
using WitchesBasement.Players;

namespace WitchesBasement.Onboarding
{
    [RequireComponent(
        typeof(Animator),
        typeof(CanvasGroup),
        typeof(LobbyPlayerInputBinding))]
    internal class LobbyPlayer : MonoBehaviour
    {
        [SerializeField] private Image profileImage;
        [SerializeField] private TMP_Text usernameText;
        [SerializeField] private TMP_InputField passwordField;

        public LobbyPlayerInputBinding Binding { get; private set; }
        private ProfileData data;

        private Animator animator;
        private CanvasGroup canvasGroup;

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
        }

        private void OnDisable()
        {
            canvasGroup.alpha = 0.5f;
        }

        private void Reset()
        {
            profileImage = GetComponentInChildren<Image>();
            usernameText = GetComponentInChildren<TMP_Text>();
            passwordField = GetComponentInChildren<TMP_InputField>();
        }

#endregion

#region Methods

        public void Setup(ProfileData profileData)
        {
            data = profileData;

            profileImage.sprite = profileData.Sprite;
            usernameText.text = profileData.Username;

            passwordField.text = string.Empty;
        }

#endregion
    }
}