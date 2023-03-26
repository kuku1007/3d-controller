using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LM {
    [CreateAssetMenu(menuName = "Items/Weapon Item")]
    public class WeaponItem : Item
    {
        public GameObject modelPrefab;
        public bool isUnarmed;
        
        [Header("Idle armed animations names")]
        public string leftHandWeaponIdle;
        public string rightHandWeaponIdle;
        public string twoHandWeaponIdle;

        [Header("One Handed Attack Animations")]
        public string lightAttack1;
        public string heavyAttack1;

        [Header("Two Handed Attack Animations")]
        public string slashAttack;
        
        [Header("Stamina cost parameters")]
        // public int baseStaminaCost;
        public int lightAttackCost;
        public int heavyAttackCost;
        public int slashAttackCost;

        public WeaponType weaponType;
    }
}
