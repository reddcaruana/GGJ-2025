using UnityEngine;

namespace WitchesBasement.Onboarding
{
    public class LobbyPasswordTriggerState : StateMachineBehaviour
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var player = animator.GetComponent<LobbyPlayer>();
            if (player is not null)
            {
                player.WritePassword();
            }
        }
    }
}