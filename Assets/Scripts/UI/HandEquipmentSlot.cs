using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LM
{
    public class HandEquipmentSlot : MonoBehaviour
    {
        public bool rightHandSlot01;
        public bool rightHandSlot02;
        public bool leftHandSlot01;
        public bool leftHandSlot02;
        UIManager uIManager;

        public Image itemIcon;
        WeaponItem weaponItem;

        private void Awake() {
            uIManager = FindFirstObjectByType<UIManager>();
        }
        public void AddItem(WeaponItem newItem) {
            if(newItem != null) {
                weaponItem = newItem;
                itemIcon.sprite = weaponItem.itemIcon;
                itemIcon.enabled = true;
                gameObject.SetActive(true);
            }
        }

        public void ClearItem(WeaponItem newItem) {
            weaponItem = null;
            itemIcon.sprite = null;
            itemIcon.enabled = false;
            gameObject.SetActive(false);
        }

        public void SelectItem() {
            if(rightHandSlot01) {
                uIManager.rightHandSlot01Selected = true;
            } else if(rightHandSlot02) {
                uIManager.rightHandSlot02Selected = true;
            } else if(leftHandSlot01) {
                uIManager.leftHandSlot01Selected = true;
            } else if(leftHandSlot02) {
                uIManager.leftHandSlot02Selected = true;
            }
        }
    }
}
