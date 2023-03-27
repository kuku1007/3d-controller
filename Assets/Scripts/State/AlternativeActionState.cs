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
            HandleMeleeHeavyAttack(
                playerLocomotion.playerAnimationManager, 
                playerLocomotion.playerInventory.rightHandWeapon,
                playerLocomotion.weaponBodySlotManager
            );
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

        private void HandleMeleeHeavyAttack(PlayerAnimationManager playerAnimationManager, WeaponItem weapon, WeaponBodySlotManager weaponBodySlotManager) {
            weaponBodySlotManager.SetAttackingWeapon(weapon);
            playerAnimationManager.PlayTargetAnimation(weapon.heavyAttack1, true);
        }
    }

}
