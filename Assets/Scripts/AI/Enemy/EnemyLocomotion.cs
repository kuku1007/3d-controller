using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LM
{
    public class EnemyLocomotion : MonoBehaviour
    {
        EnemyAnimationManager enemyAnimationManager;
        EnemyManager enemyManager;
        public float distanceToTarget;
        public CapsuleCollider characterCapsuleCollider;
        public CapsuleCollider collisionBlockerCollider;

        private void Awake() {
            enemyAnimationManager = GetComponentInChildren<EnemyAnimationManager>();
            enemyManager = GetComponent<EnemyManager>();
        }

        private void Start() {
            Physics.IgnoreCollision(characterCapsuleCollider, collisionBlockerCollider, true);
        }
    }
}
