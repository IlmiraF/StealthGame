using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    [CreateAssetMenu]
    public class ShootState : EnemyStates
    {
        public EnemyStates waitState;

        public override EnemyStates DoState(EnemyController enemy)
        {
            enemy.DamageToPlayer();
            waitState.EnterState(enemy);
            enemy.SetState(waitState);
            return waitState;
        }

        public override void EnterState(EnemyController enemy)
        {
            Debug.Log("Entering shoot state");
            enemy.Timer = 0;
            enemy.Agent.speed = 0;
            enemy.Agent.isStopped = true;
            enemy.Animator.SetBool("IsMoving", false);
            enemy.Animator.SetBool("IsAlert", false);
            enemy.Animator.SetBool("IsShooting", true);
        }
    }
}
