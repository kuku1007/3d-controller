using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LM
{
    public class EnemyAnimationManager : AnimationManager
    {
        EnemyManager enemyManager;
        private void Awake() {
            anim = GetComponent<Animator>();
            enemyManager = GetComponentInParent<EnemyManager>();
        }

        // private void OnAnimatorMove() {
        //     float delta = Time.deltaTime;
        //     enemyManager.myRigidbody.drag = 0;
        //     Vector3 deltaPostion = anim.deltaPosition;
        //     deltaPostion.y = 0;
        //     enemyManager.myRigidbody.velocity = deltaPostion / delta;
        // }
    }
}
