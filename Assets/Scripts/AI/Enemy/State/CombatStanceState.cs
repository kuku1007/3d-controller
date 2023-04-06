using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LM
{
    public class CombatStanceState : EnemyState
    {
        public AttackState attackState;
        public PursueState pursueState;

        public override EnemyState Tick(
            EnemyManager enemyManager, 
            EnemyStats enemyStats, 
            EnemyAnimationManager enemyAnimationManager)
        {
            if(enemyManager.isPerformingAction)
                enemyAnimationManager.anim.SetFloat("Vertical", 0f, 0.1f, Time.deltaTime);
            // TODO: would be nice to add different anim for combat stance
            HandleRotateTowardsTarget(enemyManager);
            float distanceToTarget = enemyManager.calculateDistanceToCurrentTarget();

            if(enemyManager.currentRecoveryTime <= 0 && distanceToTarget <= enemyManager.maxDistanceToFight) { // if maxdistance to fight higher than any of attackaction max distance then enemy will never attack
                return attackState;
            } else if(distanceToTarget > enemyManager.maxDistanceToFight) {
                return pursueState;
            } 

            return this;
        }
        
        private void HandleRotateTowardsTarget(EnemyManager enemyManager) {
            if(enemyManager.isPerformingAction) {
                Vector3 targetDir = enemyManager.currentDetectedCharacter.transform.position - enemyManager.transform.position;
                targetDir.y = 0;
                targetDir.Normalize();

                if(targetDir == Vector3.zero) { // TODO: when?
                    targetDir = enemyManager.transform.forward;
                }

                Quaternion targetRotation = Quaternion.LookRotation(targetDir);
                enemyManager.transform.rotation = 
                    Quaternion.Slerp(enemyManager.transform.rotation, targetRotation, enemyManager.rotationSpeed / Time.deltaTime); // TODO: not fixedDeltaTime?
            } else {
                Vector3 relativeDir = enemyManager.transform.InverseTransformDirection(enemyManager.navMeshAgent.desiredVelocity); // TODO:?
                // Vector3 targetVelocity = enemyManager.myRigidbody.velocity;

                enemyManager.navMeshAgent.enabled = true;
                enemyManager.navMeshAgent.SetDestination(enemyManager.currentDetectedCharacter.transform.position);
                // enemyManager.myRigidbody.velocity = targetVelocity; // TODO: so navMeshAgent already changed this?
                enemyManager.transform.rotation = 
                    Quaternion.Slerp(enemyManager.transform.rotation, enemyManager.navMeshAgent.transform.rotation, enemyManager.rotationSpeed / Time.deltaTime);
            }   

        }
    }
}
