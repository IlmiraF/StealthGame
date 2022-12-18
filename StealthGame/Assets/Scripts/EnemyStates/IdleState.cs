using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    [CreateAssetMenu]
    public class IdleState : EnemyStates
    {
        public EnemyStates patrolState;

        public override EnemyStates DoState(EnemyController enemy)
        {
            patrolState.EnterState(enemy);
            enemy.SetState(patrolState);
            return patrolState;
        }

        public override void EnterState(EnemyController enemy)
        {
            enemy.Timer = 0;
            enemy.Agent.speed = 0;
            enemy.Agent.isStopped = true;
            enemy.Animator.SetBool("IsMoving", false);
            enemy.Animator.SetBool("IsAlert", false);
            enemy.Animator.SetBool("IsShooting", false);
        }
    }
}
