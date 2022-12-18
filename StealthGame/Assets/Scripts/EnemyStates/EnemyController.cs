using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class EnemyController : MonoBehaviour
    {
        [Header("Movement")]
        public GameObject[] checkPoints;
        public float walkSpeed = 2;
        public float runSpeed = 4;
        public float waitTime = 5;

        [Header("Senses")]
        public float hearRange = 4;
        public float sightRange = 5;

        [Range(0, 360)]
        public float sightAngle = 120;

        private float stateTimer = 0;
        private int actualCheckPoint = 0;

        private NavMeshAgent agent;
        private PlayerController player;
        private Vector3 soundSource;
        private Ray ray;
        private RaycastHit hit;
        private Health health;

        [SerializeField]
        private EnemyStates enemyStates;

        private Animator animator;
        
        //private LastPlayerSighting lastPlayerSighting;

        private void OnEnable()
        {
            player = FindObjectOfType<PlayerController>();
            agent = GetComponent<NavMeshAgent>();
            animator = transform.GetChild(0).gameObject.GetComponent<Animator>();
            health = FindObjectOfType<Health>();
        }

        private void Update()
        {
            stateTimer += Time.deltaTime;
            SetState(enemyStates);
        }

        public void SetState(EnemyStates state)
        {
            enemyStates = state;
            enemyStates.DoState(this);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, hearRange);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, sightRange);
        }

        #region Actions
        public void MoveToPlayer()
        {
            agent.destination = player.transform.position;
            agent.isStopped = false;
        }

        public void MoveToSound()
        {
            agent.destination = soundSource;
            agent.isStopped = false;
        }

        public void MoveToCheckPoint()
        {
            agent.destination = checkPoints[actualCheckPoint].transform.position;
            agent.isStopped = false;
        }

        public void NextCheckPoint()
        {
            actualCheckPoint++;
            if (actualCheckPoint >= checkPoints.Length)
            {
                actualCheckPoint = 0;
            }
        }

        #endregion

        #region Decisions
        public bool IsPlayerAlive => !player.IsDead;

        public bool DestinationReached()
        {
            return agent.remainingDistance < agent.stoppingDistance && !agent.pathPending;
        }

        public bool TimeOut(float timeToWait) => stateTimer >= timeToWait;

        public bool PlayerIsHeard()
        {
            if (player.IsDead)
            {
                return false;
            }

            float distance = Vector3.Distance(transform.position, player.transform.position);
            bool result = !player.IsStealth && player.IsMoving && distance < hearRange;

            if (result)
            {
                soundSource = player.transform.position;
            }

            return result;
        }

        public bool PlayerInSight()
        {
            if (player.IsDead)
            {
                return false;
            }

            //in range
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            if (distanceToPlayer < sightRange)
            {
                //in angle
                Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;
                float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);
                if (angleToPlayer < sightAngle / 2)
                {
                    //in line
                    Vector3 startPos;
                    startPos = transform.position + Vector3.up * 0.8f;
                    Debug.DrawRay(startPos, directionToPlayer, Color.red, 0.5f);
                    //if (player.IsStealth)
                    //{
                    //    startPos = transform.position + Vector3.up * 0.7f;
                    //}
                    //else
                    //{
                    //    startPos = transform.position + Vector3.up * 0.7f;
                    //}
                    ray = new Ray(startPos, directionToPlayer);
                    if (Physics.Raycast(ray, out hit))
                    {
                        //is player
                        if (hit.transform.gameObject.tag == "Player")
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        public void KillPlayer()
        {
            player.KillMe();
        }

        public void DamageToPlayer()
        {
            health.Damage(5);
        }
        #endregion

        public float Timer { get { return stateTimer; } set { stateTimer = value; } }
        public NavMeshAgent Agent { get { return agent; } }
        public Animator Animator { get { return animator; } }
        public Health Gethealth { get { return health; } }
    }
}
