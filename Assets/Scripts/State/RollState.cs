using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LM
{

public class RollState : State
{
    private float rollSpeed = 200;
    private Vector3 initDirection; 
    public override void EnterState(PlayerLocomotion playerLocomotion)
    {
        Debug.Log("Entered Roll state");
        PlayRollAnimation(playerLocomotion, playerLocomotion.inputHandler);
    }

    public override void OnUpdate(PlayerLocomotion playerLocomotion)
    {
        playerLocomotion.HandleMovement(initDirection);
        playerLocomotion.HandleGravity();
        playerLocomotion.HandleAimRotationOnly();
        playerLocomotion.HandleGravity();
        if(playerLocomotion.playerAnimationManager.anim.GetBool("animationOngoing") == false) {
            ExitState(playerLocomotion, playerLocomotion.moveState);
        }
        if(playerLocomotion.isOnGround == false) ExitState(playerLocomotion, playerLocomotion.fallState);
    }
    
    public override void ExitState(PlayerLocomotion playerLocomotion, State newState)
    {
        playerLocomotion.SetState(newState);
    }

    private void PlayRollAnimation(PlayerLocomotion playerLocomotion, InputHandler inputHandler) {
        playerLocomotion.movementSpeed = 7;
        initDirection = playerLocomotion.inputDirection;
        if(inputHandler.movementInput.x < 0) {
            playerLocomotion.playerAnimationManager.PlayTargetAnimation("FastRollLeft", true); // TODO nth: diagonal roll
        } else if(inputHandler.movementInput.x > 0) {
            playerLocomotion.playerAnimationManager.PlayTargetAnimation("FastRollRight", true);
        } else if(inputHandler.movementInput.y > 0) {
            playerLocomotion.playerAnimationManager.PlayTargetAnimation("Rolling", true);
        } else {
            playerLocomotion.movementSpeed = 3;
            initDirection = playerLocomotion.transform.forward * -1;
            initDirection.Normalize();
            playerLocomotion.playerAnimationManager.PlayTargetAnimation("Backstep", true);
        }
    }
}
    
}