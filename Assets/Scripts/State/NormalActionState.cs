using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LM
{

    public class NormalActionState : State
    {
        public override void EnterState(PlayerLocomotion playerLocomotion)
        {
             base.EnterState(playerLocomotion);
            HandleMeleeLightAttack(playerLocomotion.playerAnimationManager, null); // TODO: current weapon should be taken from player inventory
        }

        public override void OnUpdate(PlayerLocomotion playerLocomotion)
        {            
            if(playerLocomotion.playerAnimationManager.anim.GetBool("animationOngoing") == false) ExitState(playerLocomotion, playerLocomotion.moveState);
        }
        
        public override void ExitState(PlayerLocomotion playerLocomotion, State newState)
        {
            playerLocomotion.SetState(newState);
        }

        private void HandleMeleeLightAttack(PlayerAnimationManager playerAnimationManager, WeaponItem weapon) { // TODO: use attack name from weapon
            playerAnimationManager.PlayTargetAnimation("Standing Melee Attack Downward", true);
        }
    }

}