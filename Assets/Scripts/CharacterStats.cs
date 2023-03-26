using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LM
{
    public class CharacterStats : MonoBehaviour
    {
        // Health
        public int healthLevel = 10;
        public int maxHealth;
        public int currentHealth;

        // Stamina
        public int staminaLevel = 10;
        public float maxStamina;
        public float currentStamina;
        
        // Focus
        public int focusLevel = 10;
        public float maxFocus;
        public float currentFocus;

        public bool isDeath = false;
    }
}
