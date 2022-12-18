using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public abstract class EnemyStates : ScriptableObject
    {
        public abstract void EnterState(EnemyController enemy);
        public abstract EnemyStates DoState(EnemyController enemy);
    }
}
