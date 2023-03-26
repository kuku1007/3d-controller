using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LM
{

    public class FallState : State
    {
        public override void EnterState(PlayerLocomotion playerLocomotion)
        {
            Debug.Log("Entering Fall State");
            PlayFallAnim(playerLocomotion.playerAnimationManager);
        }

        public override void OnUpdate(PlayerLocomotion playerLocomotion)
        {
            playerLocomotion.HandleMovement(playerLocomotion.currentInAirDirection);
            playerLocomotion.HandleGravity();
            if(playerLocomotion.characterController.isGrounded) ExitState(playerLocomotion, playerLocomotion.moveState);
        }

        public override void ExitState(PlayerLocomotion playerLocomotion, State newState)
        {
            playerLocomotion.playerAnimationManager.PlayTargetAnimation("Empty", false);
            playerLocomotion.SetState(newState);
        }

        private void PlayFallAnim(PlayerAnimationManager playerAnimationManager) {
            playerAnimationManager.PlayTargetAnimation("Falling", true);
        }
    }

}