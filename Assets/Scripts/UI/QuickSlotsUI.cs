using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LM {
    public class QuickSlotsUI : MonoBehaviour
    {
        public Image quickLeftSlotIcon;
        public Image quickRightSlotIcon;
        
        public void LoadWeaponIcon(bool isLeft, WeaponItem weaponItem) {
            if(isLeft) {
                quickLeftSlotIcon.sprite = weaponItem.itemIcon;
                if(weaponItem.itemIcon == null) {
                    quickLeftSlotIcon.enabled = false;
                } else {
                    quickLeftSlotIcon.enabled = true;
                }
            } else {
                quickRightSlotIcon.sprite = weaponItem.itemIcon;
                if(weaponItem.itemIcon == null) {
                    quickRightSlotIcon.enabled = false;
                } else {
                    quickRightSlotIcon.enabled = true;
                }
            }
        }
    }
}

