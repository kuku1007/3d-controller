using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LM
{
    public class AlternativeActionState : State // TODO: currrently lots of duplication with NormalActionState maybe merge?
    {
        public override void EnterState(PlayerLocomotion playerLocomotion)
        {
             base.EnterState(playerLocomotion);
            HandleMeleeLightAttack(playerLocomotion.playerAnimationManager, null); // TODO: current weapon should be taken from player inventory
        }

        public override void OnUpdate(PlayerLocomotion playerLocomotion)
        {
            playerLocomotion.HandleRotation();
            if(playerLocomotion.playerAnimationManager.anim.GetBool("animationOngoing") == false) ExitState(playerLocomotion, playerLocomotion.moveState);
        }
        
        public override void ExitState(PlayerLocomotion playerLocomotion, State newState)
        {
            playerLocomotion.SetState(newState);
        }

        private void HandleMeleeLightAttack(PlayerAnimationManager playerAnimationManager, WeaponItem weapon) { // TODO: use attack name from weapon
            playerAnimationManager.PlayTargetAnimation("Standing Melee Attack Horizontal", true);
        }
    }

}
