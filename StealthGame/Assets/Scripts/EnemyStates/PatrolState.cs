using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    [CreateAssetMenu]
    public class PatrolState : EnemyStates
    {
        public EnemyStates waitState;
        public EnemyStates alertState;
        public EnemyStates chaseState;

        public override EnemyStates DoState(EnemyController enemy)
        {
            enemy.MoveToCheckPoint();

            if (enemy.DestinationReached())
            {
                enemy.NextCheckPoint();
                waitState.EnterState(enemy);
                enemy.SetState(waitState);
                return waitState;
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
            enemy.Timer = 0;
            enemy.Agent.speed = enemy.walkSpeed;
            enemy.Animator.SetBool("IsMoving", true);
            enemy.Animator.SetBool("IsAlert", false);
            enemy.Animator.SetBool("IsShooting", false);
        }
    }
}
