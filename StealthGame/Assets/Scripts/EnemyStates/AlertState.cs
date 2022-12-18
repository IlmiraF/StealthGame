using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    [CreateAssetMenu]
    public class AlertState : EnemyStates
    {
        public EnemyStates waitState;
        public EnemyStates chaseState;

        public override EnemyStates DoState(EnemyController enemy)
        {
            enemy.MoveToSound();
            if (enemy.DestinationReached())
            {
                waitState.EnterState(enemy);
                enemy.SetState(waitState);
                return waitState;
            }
            if (enemy.PlayerInSight())
            {
                chaseState.EnterState(enemy);
                enemy.SetState(chaseState);
                return chaseState;
            }
            if (enemy.PlayerIsHeard())
            {
                return this;
            }
            
            return this;
        }

        public override void EnterState(EnemyController enemy)
        {
            Debug.Log("Entering alert state");
            enemy.Timer = 0;
            enemy.Agent.speed = enemy.walkSpeed;
            enemy.Animator.SetBool("IsMoving", true);
            enemy.Animator.SetBool("IsAlert", false);
            enemy.Animator.SetBool("IsShooting", false);
        }
    }
}
