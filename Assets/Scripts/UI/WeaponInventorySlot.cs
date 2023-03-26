using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LM
{
    public class WeaponInventorySlot : MonoBehaviour
    {
        PlayerInventory playerInventory;
        UIManager uIManager;
        WeaponBodySlotManager weaponHandSlotManager;

        WeaponItem weaponItem;
        public Image itemIcon;

        private void Awake() {
            playerInventory = FindFirstObjectByType<PlayerInventory>();
            uIManager = FindFirstObjectByType<UIManager>();
            weaponHandSlotManager = FindFirstObjectByType<WeaponBodySlotManager>();
        }

        public void AddItem(WeaponItem weaponItem) {
            this.weaponItem = weaponItem;
            itemIcon.sprite = weaponItem.itemIcon;
            itemIcon.enabled = true;
            gameObject.SetActive(true);
        }

        public void ClearSlot() {
            this.weaponItem = null;
            itemIcon.sprite = null;
            itemIcon.enabled = false;
            gameObject.SetActive(false);
        }

        public void EquipItem() {
            // if(uIManager.rightHandSlot01Selected) {
            //     Debug.Log("selected01");
            //     playerInventory.weaponsInventory.Add(playerInventory.weaponsInRightSlot[0]);
            //     playerInventory.weaponsInRightSlot[0] = weaponItem;
            //     playerInventory.weaponsInventory.Remove(weaponItem);
            //     playerInventory.currentRightWeaponIndex = 0;
            //     playerInventory.rightWeapon = weaponItem;
            //     weaponHandSlotManager.LoadWeaponOnSlot(weaponItem, false);

            // } else if(uIManager.rightHandSlot02Selected) {
            //     Debug.Log("selected01");
            //     playerInventory.weaponsInventory.Add(playerInventory.weaponsInRightSlot[1]);
            //     playerInventory.weaponsInRightSlot[1] = weaponItem;
            //     playerInventory.weaponsInventory.Remove(weaponItem);
            //     playerInventory.currentRightWeaponIndex = 1;
            //     playerInventory.rightWeapon = weaponItem;
            //     weaponHandSlotManager.LoadWeaponOnSlot(weaponItem, false);

            // } else if(uIManager.leftHandSlot01Selected) {
            //     Debug.Log("selected01");
            //     playerInventory.weaponsInventory.Add(playerInventory.weaponsInLeftSlot[0]);
            //     playerInventory.weaponsInLeftSlot[0] = weaponItem;
            //     playerInventory.weaponsInventory.Remove(weaponItem);
            //     playerInventory.currentLeftWeaponIndex = 0;
            //     playerInventory.leftWeapon = weaponItem;
            //     weaponHandSlotManager.LoadWeaponOnSlot(weaponItem, true);

            // } else if(uIManager.leftHandSlot02Selected) {
            //     Debug.Log("selected01");
            //     playerInventory.weaponsInventory.Add(playerInventory.weaponsInLeftSlot[1]);
            //     playerInventory.weaponsInLeftSlot[1] = weaponItem;
            //     playerInventory.weaponsInventory.Remove(weaponItem);
            //     playerInventory.currentLeftWeaponIndex = 1;
            //     playerInventory.leftWeapon = weaponItem;
            //     weaponHandSlotManager.LoadWeaponOnSlot(weaponItem, true);
            // }
            // uIManager.equipementWindowUI.LoadWeaponsOnEquipementScreen(playerInventory);
        }
    }
}
