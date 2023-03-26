using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LM {
    public class DamageCollider : MonoBehaviour
    {
        Collider dmgCollider;
        public int itemDamage = 25;

        private void Awake() {
            dmgCollider = GetComponent<Collider>();
            dmgCollider.gameObject.SetActive(true);
            dmgCollider.isTrigger = true;
            dmgCollider.enabled = false;
        }

        public void EnableDamageCollider() {
            dmgCollider.enabled = true;
        }
        public void DisableDamageCollider() {
            dmgCollider.enabled = false;
        }

        private void OnTriggerEnter(Collider collision) {
            if(collision.tag == "Player") {
                PlayerStats playerStats = collision.GetComponent<PlayerStats>();

                if(playerStats != null) {
                    playerStats.TakeDamage(itemDamage);
                }
            }
            // if(collision.tag == "Enemy") {
            //     EnemyStats enemyStats = collision.GetComponent<EnemyStats>();

            //     if(enemyStats != null) {
            //         enemyStats.TakeDamage(itemDamage);
            //     }
            // }
        }
    }
}
