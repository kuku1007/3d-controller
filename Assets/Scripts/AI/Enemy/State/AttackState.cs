using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LM
{
    public class AttackState : EnemyState
    {
        public CombatStanceState combatStanceState;
        public EnemyAttackAction[] enemyAttackActions;
        public EnemyAttackAction currentAttackAction;

        public override EnemyState Tick(
            EnemyManager enemyManager, 
            EnemyStats enemyStats, 
            EnemyAnimationManager enemyAnimationManager)
        {
            if(enemyManager.isPerformingAction)
                return combatStanceState;

            HandleRotateTowardsTarget(enemyManager);
            
            float distanceToTarget = enemyManager.calculateDistanceToCurrentTarget();
            float angleToTarget = enemyManager.calculateAngleToCurrentTarget();

            if(currentAttackAction != null) 
            {
                if(distanceRequirementFulfilled(distanceToTarget, currentAttackAction) 
                    && angleRequirementFulfilled(angleToTarget, currentAttackAction)) 
                {
                    if(enemyManager.currentRecoveryTime <= 0) 
                    {
                        enemyManager.isPerformingAction = true;
                        enemyManager.currentRecoveryTime = currentAttackAction.recoveryTime;
                        enemyAnimationManager.PlayTargetAnimation(currentAttackAction.actionAnimationName, true);
                        currentAttackAction = null;
                        return combatStanceState;
                    }
                }
            } else 
            {
                FetchNewAttack(enemyManager);
            }
            return combatStanceState;
        }

        private void FetchNewAttack(EnemyManager enemyManager) {
            float distanceToTarget = enemyManager.calculateDistanceToCurrentTarget();
            float angleToTarget = enemyManager.calculateAngleToCurrentTarget();
            int maxScore = 0;
            for (int i = 0; i < enemyAttackActions.Length; i++)
            {
                if(distanceRequirementFulfilled(distanceToTarget, enemyAttackActions[i]) 
                    && angleRequirementFulfilled(angleToTarget, enemyAttackActions[i])) {

                        maxScore += enemyAttackActions[i].attackScore;
                }
            }

            int randomValue = Random.Range(0, maxScore);
            
            int tmpScore = 0;
            for (int i = 0; i < enemyAttackActions.Length; i++) // TODO: explanation?
            {
                if(distanceRequirementFulfilled(distanceToTarget, enemyAttackActions[i]) 
                    && angleRequirementFulfilled(angleToTarget, enemyAttackActions[i])) {

                        if(currentAttackAction != null)
                            return;

                        tmpScore += enemyAttackActions[i].attackScore;
                        if(tmpScore > randomValue) {
                            currentAttackAction = enemyAttackActions[i];
                        }
                }
            }

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

        private bool distanceRequirementFulfilled(float distanceToTarget, EnemyAttackAction enemyAttackAction) {
            return distanceToTarget <= enemyAttackAction.maxDistanceNeededToAttack 
                && distanceToTarget > enemyAttackAction.minDistanceNeededToAttack;
        }
        private bool angleRequirementFulfilled(float angleToTarget, EnemyAttackAction enemyAttackAction) {
            return angleToTarget <= enemyAttackAction.maxAttackAngle 
                && angleToTarget > enemyAttackAction.minAttackAngle;
        }
    }
}
