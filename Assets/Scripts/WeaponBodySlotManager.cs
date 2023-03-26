using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LM {
    public class WeaponBodySlotManager : MonoBehaviour
    {
        WeaponBodyHolderSlot leftHandSlot;
        WeaponBodyHolderSlot rightHandSlot;
        WeaponBodyHolderSlot backSlot;

        DamageCollider leftDamageCollider;
        DamageCollider rightDamageCollider;

        QuickSlotsUI quickSlotsUI;
        PlayerStats playerStats;
        PlayerManager playerManager;
        WeaponItem attackingWeapon;

        InputHandler inputHandler;
        Animator animator;

        private void Awake() {
            animator = GetComponent<Animator>();
            playerStats = GetComponentInParent<PlayerStats>();
            playerManager = GetComponentInParent<PlayerManager>();
            quickSlotsUI = FindFirstObjectByType<QuickSlotsUI>();
            inputHandler = FindFirstObjectByType<InputHandler>();
            WeaponBodyHolderSlot[] weaponHolderSlots = GetComponentsInChildren<WeaponBodyHolderSlot>();

            foreach(WeaponBodyHolderSlot weaponHolderSlot in weaponHolderSlots) {
                if(weaponHolderSlot.isLeftHandSlot) {
                    leftHandSlot = weaponHolderSlot;
                } else if(weaponHolderSlot.isRightHandSlot) {
                    rightHandSlot = weaponHolderSlot;
                } else if(weaponHolderSlot.isBackSlot) {
                    backSlot = weaponHolderSlot;
                }
            }
        }

        public void LoadWeaponOnSlot(WeaponItem weaponItem, bool isLeft) {
            if(isLeft) {
                leftHandSlot.currentWeapon = weaponItem;
                leftHandSlot.LoadWeaponModel(weaponItem);
                RetrieveLeftDamageCollider();
                quickSlotsUI.LoadWeaponIcon(isLeft, weaponItem);
                
                if(weaponItem == null || weaponItem.itemName.Equals("unarmed")) 
                    animator.CrossFade("Left Arm Empty",0.2f);
                else
                    animator.CrossFade(weaponItem.leftHandWeaponIdle, 0.2f);

            } else {
                if(inputHandler.twoHFlag) {
                    
                    // backSlot.LoadWeaponModel(weaponItem);
                    // move left weapon on back
                    backSlot.LoadWeaponModel(leftHandSlot.currentWeapon);
                    leftHandSlot.DestroyCurrentWeapon();
                    animator.CrossFade(weaponItem.twoHandWeaponIdle, 0.2f);
                } else {
                    backSlot.DestroyCurrentWeapon();
                    animator.CrossFade("Two Hand Arm Empty",0.2f);
                    if(weaponItem == null || weaponItem.itemName.Equals("unarmed")) 
                        animator.CrossFade("Right Arm Empty", 0.2f);
                    else
                        animator.CrossFade(weaponItem.rightHandWeaponIdle, 0.2f);
                }
                rightHandSlot.currentWeapon = weaponItem;
                rightHandSlot.LoadWeaponModel(weaponItem);
                RetrieveRightDamageCollider();
                quickSlotsUI.LoadWeaponIcon(isLeft, weaponItem);
            }         
        }
    
        #region Handle Damage colliders
        public void RetrieveLeftDamageCollider() {
            leftDamageCollider = leftHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
        }

        public void RetrieveRightDamageCollider() {
            rightDamageCollider = rightHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
        }

        public void OpenDamageCollider() {
            if(playerManager.isUsingRightHand)
                rightDamageCollider.EnableDamageCollider();
            else if(playerManager.isUsingLeftHand)
                leftDamageCollider.EnableDamageCollider();
        }

        public void CloseDamageCollider() {
            if(playerManager.isUsingRightHand)
                rightDamageCollider.DisableDamageCollider();
            else if(playerManager.isUsingLeftHand)
                leftDamageCollider.DisableDamageCollider();
        }
        #endregion
    
        // public void SetAttackingWeapon(WeaponItem weaponItem) {
        //     this.attackingWeapon = weaponItem;
        // }

        // public void DrainStaminaFromLightAttack() {
        //     playerStats.TakeStaminaCost(attackingWeapon.lightAttackCost);
        // }
        // public void DrainStaminaFromHeavyAttack() {
        //     playerStats.TakeStaminaCost(attackingWeapon.heavyAttackCost);
        // }
        // public void DrainStaminaFromSlashAttack() {
        //     playerStats.TakeStaminaCost(attackingWeapon.slashAttackCost);
        // }
    }
}

