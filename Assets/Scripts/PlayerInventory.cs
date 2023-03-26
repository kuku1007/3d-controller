using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LM { 
    public class PlayerInventory : MonoBehaviour
    {
        WeaponBodySlotManager weaponSlotManager;
        [HideInInspector] public WeaponItem rightHandWeapon;
        [HideInInspector] public WeaponItem leftHandWeapon;

        public WeaponItem unarmed;
        public WeaponItem[] weaponsInRightSlot = new WeaponItem[2];
        public WeaponItem[] weaponsInLeftSlot = new WeaponItem[2];
        public int currentRightWeaponIndex = -1;
        public int currentLeftWeaponIndex = -1;

        public List<WeaponItem> weaponsInventory;
        // public SpellItem currentSpellItem;

        private void Awake() {
            weaponSlotManager = GetComponentInChildren<WeaponBodySlotManager>();
        }

        private void Start() {
            rightHandWeapon = weaponsInRightSlot[0];
            leftHandWeapon = weaponsInLeftSlot[0];
            weaponSlotManager.LoadWeaponOnSlot(rightHandWeapon, false);
            weaponSlotManager.LoadWeaponOnSlot(leftHandWeapon, true);
            currentRightWeaponIndex = 0;
            currentLeftWeaponIndex = 0;
        }

        public void NextRightWeapon() {
            // int nextWeaponIndex = currentRightWeaponIndex + 1;

            // while (nextWeaponIndex != currentRightWeaponIndex) {
            //     if(nextWeaponIndex == weaponsInRightSlot.Length) {
            //         nextWeaponIndex = 0;
            //     }

            //     if(weaponsInRightSlot[nextWeaponIndex] != null) {
            //         rightWeapon = weaponsInRightSlot[nextWeaponIndex];
            //         currentRightWeaponIndex = nextWeaponIndex;
            //         weaponSlotManager.LoadWeaponOnSlot(rightWeapon, false);
            //         return;
            //     }
            //     // All items null in weapon slots
            //     nextWeaponIndex++;
            // }

            // currentRightWeaponIndex = -1;
            // rightWeapon = unarmed;
        }

        public void NextLeftWeapon() {
            // int nextWeaponIndex = currentLeftWeaponIndex + 1;

            // while (nextWeaponIndex != currentLeftWeaponIndex) {
            //     if(nextWeaponIndex == weaponsInLeftSlot.Length) {
            //         nextWeaponIndex = 0;
            //     }

            //     if(weaponsInLeftSlot[nextWeaponIndex] != null) {
            //         leftWeapon = weaponsInLeftSlot[nextWeaponIndex];
            //         currentLeftWeaponIndex = nextWeaponIndex;
            //         weaponSlotManager.LoadWeaponOnSlot(leftWeapon, true);
            //         return;
            //     }

            //     nextWeaponIndex++;
            // }
            // // All items null in weapon slots
            // currentLeftWeaponIndex = -1;
            // leftWeapon = unarmed;
            // weaponSlotManager.LoadWeaponOnSlot(unarmed, false);
        }
    }
}