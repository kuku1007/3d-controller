using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LM
{
    public class IdleState : EnemyState
    {
        public LayerMask detectionLayer;
        public PursueState pursueState;

        public override EnemyState Tick(
            EnemyManager enemyManager, 
            EnemyStats enemyStats,
            EnemyAnimationManager enemyAnimationManager)
        {
            Collider[] colliders = Physics.OverlapSphere(enemyManager.transform.position, enemyManager.detectionRadius, detectionLayer);

            for(int i=0; i < colliders.Length; i++) {
                CharacterStats characterStats = colliders[i].GetComponent<CharacterStats>();
                // Debug.Log("Detected: " + characterStats.tag);
                if(characterStats != null) {
                    Vector3 targetDir = characterStats.transform.position - transform.position;
                    float angleToTarget = Vector3.Angle(targetDir, transform.forward);

                    if(angleToTarget > enemyManager.minDetectionAngle && angleToTarget < enemyManager.maxDetectionAngle) {
                        enemyManager.currentDetectedCharacter = characterStats;
                        return pursueState;
                    }
                }
            }
            
            return this;
        }
    }
}
