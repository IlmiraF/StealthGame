using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    [CreateAssetMenu]
    public class WaitState : EnemyStates
    {
        public EnemyStates patrolState;
        public EnemyStates alertState;
        public EnemyStates chaseState;

        public override EnemyStates DoState(EnemyController enemy)
        {
            if (enemy.TimeOut(3))
            {
                patrolState.EnterState(enemy);
                enemy.SetState(patrolState);
                return patrolState;
            }
            if (enemy.PlayerIsHeard())
            {
                alertState.EnterState(enemy);
                enemy.SetState(alertState);
                return alertState;
            }
            if (enemy.PlayerInSight())
            {
                chaseState.EnterState(enemy);
                enemy.SetState(chaseState);
                return chaseState;
            }
            
            return this;
        }

        public override void EnterState(EnemyController enemy)
        {
            Debug.Log("Entering wait state");
            enemy.Timer = 0;
            enemy.Agent.speed = 0;
            enemy.Agent.isStopped = true;
            enemy.Animator.SetBool("IsMoving", false);
            enemy.Animator.SetBool("IsAlert", false);
            enemy.Animator.SetBool("IsShooting", false);
        }
    }
}
