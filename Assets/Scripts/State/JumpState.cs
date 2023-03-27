using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LM
{

    public class JumpState : State
    {
        public float jumpPower = 1f;
        public override void EnterState(PlayerLocomotion playerLocomotion)
        {
            Debug.Log("Entering Jump state");
            playerLocomotion.currentInAirDirection = playerLocomotion.inputDirection;
            playerLocomotion.movementSpeed = 3;
            SetVerticalVelocityAndPlayJump(playerLocomotion.playerAnimationManager, playerLocomotion.inputHandler, playerLocomotion);
        }

        public override void OnUpdate(PlayerLocomotion playerLocomotion)
        {
            playerLocomotion.HandleMovement(playerLocomotion.currentInAirDirection);
            playerLocomotion.HandleGravity();
            if(playerLocomotion.playerAnimationManager.anim.GetBool("animationOngoing") == false) 
                ExitState(playerLocomotion, playerLocomotion.fallState);
        }
        
        public override void ExitState(PlayerLocomotion playerLocomotion, State newState)
        {
            playerLocomotion.SetState(newState);
        }

        private void SetVerticalVelocityAndPlayJump(PlayerAnimationManager playerAnimationManager, InputHandler inputHandler, PlayerLocomotion playerLocomotion) {
            playerLocomotion.verticalVelocity.y = Mathf.Sqrt(jumpPower * -3f * playerLocomotion.gravity);
            playerAnimationManager.PlayTargetAnimation("Jump", true);
        }
    }

}