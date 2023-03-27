using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LM {
    public class PlayerStats : CharacterStats
    {
        public HealthBar healthBar;
        public StaminaBar staminaBar;
        public FocusBar focusBar;
        [HideInInspector] public PlayerAnimationManager animationHandler;
        PlayerLocomotion playerLocomotion;

        public float staminaRecoveryMultiplier = 10;
        float staminaRecoveryTimer = 0;
        
        private void Awake() {
            animationHandler = GetComponentInChildren<PlayerAnimationManager>();
            playerLocomotion = GetComponent<PlayerLocomotion>();
        }

        void Start()
        {
            maxHealth = calculateMaxHealthFromHealthLevel(healthLevel);
            currentHealth = maxHealth;
            healthBar.SetMaxHealthAndCurrent(maxHealth);

            maxStamina = calculateMaxStaminaFromStaminaLevel(staminaLevel);
            currentStamina = maxStamina;
            staminaBar.SetMaxStaminaAndCurrent(maxStamina);

            maxFocus = calculateMaxFocusFromFocusLevel(focusLevel);
            currentFocus = maxFocus;
            focusBar.SetMaxFocusAndCurrent(maxFocus);
        }

        public int calculateMaxHealthFromHealthLevel(int healthLevel) {
            maxHealth = healthLevel * 10;
            return maxHealth;
        }

        public float calculateMaxStaminaFromStaminaLevel(int staminaLevel) {
            maxStamina = staminaLevel * 10;
            return maxStamina;
        }
                
        public float calculateMaxFocusFromFocusLevel(int focusLevel) {
            maxFocus = focusLevel * 10;
            return maxFocus;
        }

        public void TakeDamage(int damage) {
            if(playerLocomotion.playerAnimationManager.anim.GetBool("isInvulnerable"))
                return;
            
            if(isDeath)
                return;

            currentHealth = currentHealth - damage;
            healthBar.SetCurrentHealth(currentHealth);

            animationHandler.PlayTargetAnimation("Damage", true);

            if(currentHealth <= 0) {
                isDeath = true;
                currentHealth = 0;
                animationHandler.PlayTargetAnimation("Death", true);
            }
        }

        public void TakeStaminaCost(int staminaCost) {
            currentStamina = currentStamina - staminaCost;
            staminaBar.SetCurrentStamina(currentStamina);
            if(currentStamina <= 0) {
                currentStamina = 0;
                // TODO: exhausted anim
            }
        }

        public void TakeFocusCost(int focusCost) {
            currentFocus = currentFocus - focusCost;
            focusBar.SetCurrentFocus(currentFocus);
            if(currentFocus <= 0) {
                currentFocus = 0;
                // TODO: no mana anim
            }
        }

        public void RecoverStamina()
        {
            if(playerLocomotion.playerAnimationManager.anim.GetBool("animationOngoing")) { // TODO: rename to nonBasicMoveAnimOngoing?
                staminaRecoveryTimer = 0;
                return;
            }
            
            if(currentStamina < maxStamina) 
            {
                staminaRecoveryTimer += Time.deltaTime;
                if(staminaRecoveryTimer >= 1.5)
                    currentStamina += staminaRecoveryMultiplier * Time.deltaTime;
                    staminaBar.SetCurrentStamina(currentStamina);
            }

        }

        public void RecoverHealth(int healthAmount) {
            currentHealth += healthAmount;
            if(currentHealth > maxHealth) {
                currentHealth = maxHealth;
            }

            healthBar.SetCurrentHealth(currentHealth);
        }
    }
}

