using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace LM
{
    public class EnemyManager : CharacterManager
    {
        public float detectionRadius = 6;
        public float minDetectionAngle = -70;
        public float maxDetectionAngle = 70;
        public float rotationSpeed = 500;
        public bool isPerformingAction;
        public bool isInteracting;
        public float currentRecoveryTime = 0;
        public float maxDistanceToFight = 2.5f;

        public EnemyState currentState;
        public CharacterStats currentDetectedCharacter;
        public NavMeshAgent navMeshAgent;
        // public Rigidbody myRigidbody;

        EnemyLocomotion enemyLocomotion;
        EnemyAnimationManager enemyAnimationManager;
        EnemyStats enemyStats;

        private void Awake() {
            enemyLocomotion = GetComponent<EnemyLocomotion>();
            enemyAnimationManager = GetComponentInChildren<EnemyAnimationManager>();
            enemyStats = GetComponent<EnemyStats>();
            // myRigidbody = GetComponent<Rigidbody>();
            navMeshAgent = GetComponentInChildren<NavMeshAgent>();
            navMeshAgent.enabled = false;
        }

        private void Start() {
            // myRigidbody.isKinematic = false;
        }

        private void Update() {
            isInteracting = enemyAnimationManager.anim.GetBool("isInteracting");
            HandleRecoveryTime();
        }

        private void FixedUpdate() {
            HandleStateMachine();
        }

        public float calculateDistanceToCurrentTarget() {
            return Vector3.Distance(currentDetectedCharacter.transform.position, transform.position);
        }
        public float calculateAngleToCurrentTarget() {
            Vector3 targetDir = currentDetectedCharacter.transform.position - transform.position;
            return Vector3.Angle(targetDir, transform.forward);
        }

        private void HandleStateMachine() {
            if(currentState != null) {
                EnemyState nextState = currentState.Tick(this, enemyStats, enemyAnimationManager);
                if(nextState != null) {
                    currentState = nextState;
                }
            }
        }

        private void HandleRecoveryTime() {
            if(currentRecoveryTime > 0) {
                currentRecoveryTime -= Time.deltaTime;
            }
            if(isPerformingAction) {
                
                if(currentRecoveryTime <= 0) {
                    isPerformingAction = false;
                }
            }
        }
    }
}