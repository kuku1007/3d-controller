using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LM
{

public class RollState : State
{
    private float rollSpeed = 50;
    public override void EnterState(PlayerLocomotion playerLocomotion)
    {
        Debug.Log("Entered Roll state");
        PlayRollAnimation(playerLocomotion.playerAnimationManager, playerLocomotion.inputHandler);
    }

    public override void OnUpdate(PlayerLocomotion playerLocomotion)
    {
        playerLocomotion.HandleRotation();
        HandleAnimatorMotionMovement(playerLocomotion.playerAnimationManager, playerLocomotion.characterController);
        if(playerLocomotion.playerAnimationManager.anim.GetBool("animationOngoing") == false) {
            ExitState(playerLocomotion, playerLocomotion.moveState);
        }
    }
    
    public override void ExitState(PlayerLocomotion playerLocomotion, State newState)
    {
        playerLocomotion.SetState(newState);
    }

    private void HandleAnimatorMotionMovement(PlayerAnimationManager playerAnimationManager, CharacterController characterController) {
        Vector3 deltaPostion = playerAnimationManager.anim.deltaPosition;
        deltaPostion.y = 0;
        characterController.Move(deltaPostion * Time.deltaTime * rollSpeed);
    }

    private void PlayRollAnimation(PlayerAnimationManager playerAnimationManager, InputHandler inputHandler) {
        if(inputHandler.movementInput.x < 0) {
            playerAnimationManager.PlayTargetAnimation("FastRollLeft", true); // TODO nth: diagonal roll
        } else if(inputHandler.movementInput.x > 0) {
            playerAnimationManager.PlayTargetAnimation("FastRollRight", true);
        } else if(inputHandler.movementInput.y > 0) {
            playerAnimationManager.PlayTargetAnimation("Rolling", true);
        } else {
            playerAnimationManager.PlayTargetAnimation("Backstep", true);
        }
    }
}
    
}