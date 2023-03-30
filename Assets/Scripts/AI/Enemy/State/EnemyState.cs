using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LM
{
    public abstract class EnemyState : MonoBehaviour
    {
        public abstract EnemyState Tick(
            EnemyManager enemyManager, 
            EnemyStats enemyStats, 
            EnemyAnimationManager enemyAnimationManager
        ); 
    }
    
}