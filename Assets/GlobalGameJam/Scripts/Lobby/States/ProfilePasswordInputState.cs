using UnityEngine;

namespace GlobalGameJam.Lobby
{
    public class ProfilePasswordInputState : StateMachineBehaviour
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var playerAccount = animator.GetComponent<PlayerAccount>();
            if (playerAccount is null)
            {
                return;
            }
            
            playerAccount.WritePassword();
        }
    }
}