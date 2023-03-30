using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LM {
    public class EnemyStats : CharacterStats
    {
            Animator animator;
            
            private void Awake() {
                animator = GetComponentInChildren<Animator>();
            }

            void Start()
            {
                maxHealth = calculateMaxHealthFromHealthLevel(healthLevel);
                currentHealth = maxHealth;
            }

            public int calculateMaxHealthFromHealthLevel(int healthLevel) {
                maxHealth = healthLevel * 10;
                return maxHealth;
            }

            public void TakeDamage(int damage) {
                if(isDeath)
                    return;

                currentHealth = currentHealth - damage;
                animator.Play("Damage");

                if(currentHealth <= 0) {
                    isDeath = true;
                    currentHealth = 0;
                    animator.Play("Death");
                }
            }
    }
}

