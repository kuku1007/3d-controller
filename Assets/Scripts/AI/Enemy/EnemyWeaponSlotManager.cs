using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LM
{
    public class EnemyWeaponSlotManager : MonoBehaviour
    {
        public WeaponItem leftHandWeapon;
        public WeaponItem rightHandWeapon;

        WeaponBodyHolderSlot leftHandSlot;
        WeaponBodyHolderSlot rightHandSlot;

        DamageCollider leftDamageCollider;
        DamageCollider rightDamageCollider;

        private void Awake() {
            WeaponBodyHolderSlot[] weaponHolderSlots = GetComponentsInChildren<WeaponBodyHolderSlot>();

            foreach(WeaponBodyHolderSlot weaponHolderSlot in weaponHolderSlots) {
                if(weaponHolderSlot.isLeftHandSlot) {
                    leftHandSlot = weaponHolderSlot;
                } else if(weaponHolderSlot.isRightHandSlot) {
                    rightHandSlot = weaponHolderSlot;
                }
            }
        }

        private void Start() {
            LoadWeaponsInBothHands();
        }

        private void LoadWeaponsInBothHands() {
            if(leftHandWeapon != null) {
                LoadWeaponOnSlot(leftHandWeapon, true);
            }
            if(rightHandWeapon != null) {
                LoadWeaponOnSlot(rightHandWeapon, false);
            }
        }

        public void LoadWeaponOnSlot(WeaponItem weaponItem, bool isLeft) {
            if(isLeft) {
                leftHandSlot.currentWeapon = weaponItem;
                leftHandSlot.LoadWeaponModel(weaponItem);
                RetrieveLeftDamageCollider();

            } else {
                rightHandSlot.currentWeapon = weaponItem;
                rightHandSlot.LoadWeaponModel(weaponItem);
                RetrieveRightDamageCollider();
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
            rightDamageCollider.EnableDamageCollider();
        }

        public void CloseDamageCollider() {
            rightDamageCollider.DisableDamageCollider();
        }
        #endregion
    
        public void SetAttackingWeapon(WeaponItem weaponItem) {

        }

        public void DrainStaminaFromLightAttack() {

        }
        public void DrainStaminaFromHeavyAttack() {

        }
        public void DrainStaminaFromSlashAttack() {

        }
    }
    
}

