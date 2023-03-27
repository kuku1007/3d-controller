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
            HandleMeleeLightAttack(
                playerLocomotion.playerAnimationManager, 
                playerLocomotion.playerInventory.rightHandWeapon,
                playerLocomotion.weaponBodySlotManager
            ); // TODO: currently attack always with right hand
        }
        public override void OnUpdate(PlayerLocomotion playerLocomotion)
        {            
            if(playerLocomotion.playerAnimationManager.anim.GetBool("animationOngoing") == false) ExitState(playerLocomotion, playerLocomotion.moveState);
        }
        
        public override void ExitState(PlayerLocomotion playerLocomotion, State newState)
        {
            playerLocomotion.SetState(newState);
        }

        private void HandleMeleeLightAttack(PlayerAnimationManager playerAnimationManager, WeaponItem weapon, WeaponBodySlotManager weaponBodySlotManager) {
            weaponBodySlotManager.SetAttackingWeapon(weapon);
            playerAnimationManager.PlayTargetAnimation(weapon.lightAttack1, true);
        }
    }

}