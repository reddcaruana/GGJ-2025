using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GlobalGameJam.Endgame
{
    public class ScoreScreenManager : MonoBehaviour
    {
        [SerializeField] private ScorePlayerInput[] scoreInputs;
        [SerializeField] private TMP_Text charactersText;
        
        private char[] groupName = { '_', '_', '_', '_' };
        private int inputtedCharacters;
        
#region Methods

        public void SetName(int playerID, char character)
        {
            groupName[playerID] = character;
            charactersText.text = new string(groupName);

            if (inputtedCharacters > 3)
            {
                StartCoroutine(ReloadSceneRoutine());
            }
        }

        public void Activate()
        {
            inputtedCharacters = 0;
            for (var i = 0; i < scoreInputs.Length; i++)
            {
                scoreInputs[i].Bind(i);
            }
        }

#endregion

#region Coroutines

        private IEnumerator ReloadSceneRoutine()
        {
            yield return new WaitForSeconds(5.0f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

#endregion
    }
}