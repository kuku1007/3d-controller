using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LM {
    public class WeaponBodyHolderSlot : MonoBehaviour
    {
        public Transform parentOverride;
        public bool isLeftHandSlot;
        public bool isRightHandSlot;
        public bool isBackSlot;

        public WeaponItem currentWeapon;
        public GameObject currentWeaponModel;

        public void LoadWeaponModel(WeaponItem weaponItem) {
           
            DestroyCurrentWeapon();
           
            if(weaponItem == null) {
                UnloadCurrentWeapon();
                return;
            }

            GameObject model = Instantiate(weaponItem.modelPrefab) as GameObject;

            if(model != null) {
                if(parentOverride != null) {
                    model.transform.parent = parentOverride;
                } else {
                    model.transform.parent = transform;
                }
                
                model.transform.localPosition = Vector3.zero;
                model.transform.localRotation = Quaternion.identity;
                model.transform.localScale = Vector3.one;
            }
            currentWeaponModel = model;
        }

        public void DestroyCurrentWeapon() {
            if(currentWeaponModel != null) {
                Destroy(currentWeaponModel);
            }
        }

        private void UnloadCurrentWeapon() {
            if(currentWeaponModel != null) {
                currentWeaponModel.SetActive(false);
            }
        }
    }
}

