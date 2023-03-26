using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LM
{
    public class EquipementWindowUI : MonoBehaviour
    {
        public bool rightHandSlot01Selected;
        public bool rightHandSlot02Selected;
        public bool leftHandSlot01Selected;
        public bool leftHandSlot02Selected;
        public HandEquipmentSlot[] handEquipmentSlots;
        PlayerInventory playerInventory;

        public void LoadWeaponsOnEquipementScreen(PlayerInventory playerInventory) {
            for(int i = 0; i < handEquipmentSlots.Length; i++) {
                if(handEquipmentSlots[i].rightHandSlot01) {
                    handEquipmentSlots[i].AddItem(playerInventory.weaponsInRightSlot[0]);

                } else if(handEquipmentSlots[i].rightHandSlot02) {
                    handEquipmentSlots[i].AddItem(playerInventory.weaponsInRightSlot[1]);

                } else if(handEquipmentSlots[i].leftHandSlot01) {
                    handEquipmentSlots[i].AddItem(playerInventory.weaponsInLeftSlot[0]);

                } else {
                    handEquipmentSlots[i].AddItem(playerInventory.weaponsInLeftSlot[1]);
                }
            }
        }

        public void SelectRightHandSlot01() {
            rightHandSlot01Selected = true;
        }
        
        public void SelectRightHandSlot02() {
            rightHandSlot02Selected = true;
        }

        public void SelectLeftHandSlot01() {
            leftHandSlot01Selected = true;
        }
        
        public void SelectLeftHandSlot02() {
            leftHandSlot02Selected = true;
        }
    }
}
