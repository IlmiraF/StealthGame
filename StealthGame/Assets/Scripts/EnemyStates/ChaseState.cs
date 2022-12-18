using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    [CreateAssetMenu]
    public class ChaseState : EnemyStates
    {
        public EnemyStates shootState;
        public EnemyStates waitState;
        public EnemyStates patrolState;

        public override EnemyStates DoState(EnemyController enemy)
        {
            enemy.MoveToPlayer();

            //Debug.Log(enemy.TimeOut(0.05f));

            if (enemy.PlayerInSight() && enemy.TimeOut(0.8f))
            {
                shootState.EnterState(enemy);
                enemy.SetState(shootState);
                return shootState;
            }
            if (!enemy.IsPlayerAlive)
            {
                waitState.EnterState(enemy);
                enemy.SetState(waitState);
                return waitState;
            }
            if (!enemy.PlayerInSight())
            {
                patrolState.EnterState(enemy);
                enemy.SetState(patrolState);
                return patrolState;
            }

            return this;
        }

        public override void EnterState(EnemyController enemy)
        {
            Debug.Log("Entering chase state");
            enemy.Timer = 0;
            enemy.Agent.speed = enemy.runSpeed;
            enemy.Animator.SetBool("IsMoving", true);
            enemy.Animator.SetBool("IsAlert", true);
            enemy.Animator.SetBool("IsShooting", false);
        }
    }
}
