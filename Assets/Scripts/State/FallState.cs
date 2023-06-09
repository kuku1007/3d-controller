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
            playerLocomotion.currentInAirDirection = playerLocomotion.inputDirection;
            playerLocomotion.movementSpeed = 3;
            playerLocomotion.playerAnimationManager.PlayTargetAnimation("Falling", true);
        }

        public override void OnUpdate(PlayerLocomotion playerLocomotion)
        {
            playerLocomotion.HandleMovement(playerLocomotion.currentInAirDirection);
            playerLocomotion.HandleGravity();
            playerLocomotion.HandleAimRotationOnly();
            if(playerLocomotion.isOnGround || playerLocomotion.characterVelocity.y == 0) ExitState(playerLocomotion, playerLocomotion.moveState);  
        }

        public override void ExitState(PlayerLocomotion playerLocomotion, State newState)
        {
            playerLocomotion.playerAnimationManager.PlayTargetAnimation("Empty", false);
            playerLocomotion.SetState(newState);
        }
    }

}