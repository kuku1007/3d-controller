using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LM
{
    [CreateAssetMenu(menuName = "AI/Enemy Action/Attack Action")]
    public class EnemyAttackAction : EnemyAction
    {
        public int attackScore = 3;
        public float recoveryTime = 2;

        public float minAttackAngle = -35;
        public float maxAttackAngle = 35;

        public float minDistanceNeededToAttack = 0;
        public float maxDistanceNeededToAttack = 3;
    }
}
