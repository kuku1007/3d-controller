using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LM
{

    public class MoveState : State
    {
        public override void EnterState(PlayerLocomotion playerLocomotion)
        {
            Debug.Log("Entering Move State");
        }

        public override void OnUpdate(PlayerLocomotion playerLocomotion)
        {
            playerLocomotion.HandleMovement(playerLocomotion.direction);
            playerLocomotion.HandleGravity();
            playerLocomotion.HandleRotation();
            playerLocomotion.playerAnimationManager.UpdateAnimator(
                playerLocomotion.inputHandler.movementInput.y, 
                playerLocomotion.inputHandler.movementInput.x, 
                false
            );
            if(playerLocomotion.inputHandler.roll_Input) ExitState(playerLocomotion, playerLocomotion.rollState);
            if(playerLocomotion.inputHandler.jumpImput) ExitState(playerLocomotion, playerLocomotion.jumpState);
            if(playerLocomotion.inputHandler.normalAttackInput) ExitState(playerLocomotion, playerLocomotion.normalActionState);
            if(playerLocomotion.inputHandler.alternativeAttackInput) ExitState(playerLocomotion, playerLocomotion.alternativeActionState);
        }

        public override void ExitState(PlayerLocomotion playerLocomotion, State newState)
        {
            playerLocomotion.SetState(newState);
        }
    }

}