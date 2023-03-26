using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LM {
    public class UIManager : MonoBehaviour
    {
        public EquipementWindowUI equipementWindowUI;
        public GameObject equipementWindowParent;
        public bool rightHandSlot01Selected;
        public bool rightHandSlot02Selected;
        public bool leftHandSlot01Selected;
        public bool leftHandSlot02Selected;

        public GameObject HUDWindow;
        public GameObject selectWindow;
        public GameObject inventoryWindow;
        public GameObject weaponInventorySlotPrefab;
        public GameObject weaponInventorySlotsParent;
        public PlayerInventory playerInventory;
        public WeaponInventorySlot[] weaponInventorySlots;

        void Start() {
            weaponInventorySlots = GetComponentsInChildren<WeaponInventorySlot>();
            equipementWindowUI.LoadWeaponsOnEquipementScreen(playerInventory);
        }

        public void UpdateUI() {

            int clearSlotsStartIndex = 0;
            for(int i = 0; i < playerInventory.weaponsInventory.Count; i++) {

                if(i >= weaponInventorySlots.Length) {
                    GameObject weaponInventorySlot = Instantiate(weaponInventorySlotPrefab, weaponInventorySlotsParent.transform);
                    weaponInventorySlots = weaponInventorySlotsParent.GetComponentsInChildren<WeaponInventorySlot>(true);
                }
                weaponInventorySlots[i].AddItem(playerInventory.weaponsInventory[i]);
                clearSlotsStartIndex = i + 1;
            }

            for(int i = clearSlotsStartIndex; i < weaponInventorySlots.Length; i++) {
                weaponInventorySlots[i].ClearSlot();
            }
        }

        public void OpenSelectWindow() {
            selectWindow.SetActive(true);
        }

        public void CloseSelectWindow() {
            selectWindow.SetActive(false);
        }

        public void OpenHUDWindow() {
            HUDWindow.SetActive(true);
        }

        public void CloseHUDWindow() {
            HUDWindow.SetActive(false);
        }

        public void CloseAllInventoryWindow() {
            ResetSelectedSlots();
            inventoryWindow.SetActive(false);
            equipementWindowParent.SetActive(false);
        }

        public void ResetSelectedSlots() {
            rightHandSlot01Selected = false;
            rightHandSlot02Selected = false;
            leftHandSlot01Selected = false;
            leftHandSlot02Selected = false;
        }
    }
}
