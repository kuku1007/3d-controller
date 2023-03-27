using System;
using UnityEngine;

namespace LM
{
    public class PlayerAttacker : MonoBehaviour
    {
        WeaponBodySlotManager weaponSlotManager;
        PlayerAnimationManager animatorHandler;
        // PlayerInventory playerInventory;
        // PlayerStats playerStats;
        InputHandler inputHandler;

        private void Awake() {
            inputHandler = GetComponentInParent<InputHandler>();
            weaponSlotManager = GetComponent<WeaponBodySlotManager>();
            animatorHandler = GetComponent<PlayerAnimationManager>();
            // playerInventory = GetComponentInParent<PlayerInventory>();
            // playerStats = GetComponentInParent<PlayerStats>();
        }

        public void HandleMeleeLightAttack(WeaponItem weapon) {
            // weaponSlotManager.SetAttackingWeapon(weapon);

            if(inputHandler.twoHFlag) {
                animatorHandler.PlayTargetAnimation(weapon.slashAttack, true);
            } else {
                animatorHandler.PlayTargetAnimation(weapon.lightAttack1, true);
            }
        }

        public void HandleMeleeHeavyAttack(WeaponItem weapon) {
            // weaponSlotManager.SetAttackingWeapon(weapon);
            
            if(inputHandler.twoHFlag) {
                animatorHandler.PlayTargetAnimation(weapon.slashAttack, true);
            } else {
                animatorHandler.PlayTargetAnimation(weapon.heavyAttack1, true);
            }
        }

        public void HandleNormalModeSpell(WeaponItem weaponItem) {
            // if(playerInventory.currentSpellItem != null && SpellType.FAITH.Equals(playerInventory.currentSpellItem.spellType)) {
            //     Debug.Log("I have spell, healing");
            //     if(playerStats.currentFocus >= playerInventory.currentSpellItem.focusCost)
            //         playerInventory.currentSpellItem.AttemptToCastSpell(animatorHandler, playerStats);
            //     else
            //         Debug.Log("TODO: Play anim/sound - not enough mana");
            // }
        }

        public void HandleNormalAttack() {
            // if(WeaponType.MELEE.Equals(playerInventory.rightWeapon.weaponType)) 
            // {
            //     animatorHandler.anim.SetBool("isUsingRightHand", true);
            //     HandleMeleeLightAttack(playerInventory.rightWeapon);
            // } 
            // else if(WeaponType.SPELL.Equals(playerInventory.rightWeapon.weaponType)) 
            // {
            //     Debug.Log("Spell invoked");
            //     HandleNormalModeSpell(playerInventory.rightWeapon);
            // }    
        }

        public void AttemptToBackstabOrRiposte()
        {
            throw new NotImplementedException();
        }

        public void SuccessfullSpellCast() {
            // if(playerInventory.currentSpellItem != null)
            //     playerInventory.currentSpellItem.SuccessfullSpellCast(animatorHandler, playerStats);
        }
    }
}
