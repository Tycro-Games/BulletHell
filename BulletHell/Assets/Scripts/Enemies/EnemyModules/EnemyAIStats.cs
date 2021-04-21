using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace Bog.Assets.Scripts.Enemies.EnemyModules
{
    [RequireComponent(typeof(Collider2D))]
    public class EnemyAIStats : CommonStats, IHitable
    {
        [SerializeField]
        private UnityEvent OnDamage = null;

        [SerializeField]
        private UnityEvent OnDead = null;

        [SerializeField]
        private UnityEvent onDestroy = null;

        private NavMeshAgent agent = null;

        private void Start()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        //this script kills you
        public void Die()
        {
            OnDead?.Invoke();
            RoomTriggerStart.currentEnemies.Remove(gameObject);
            GetComponentInParent<RoomTriggerStart>().End();

            PoolingObjectsSystem.Destroy(gameObject);
        }

        private void OnDestroy()
        {
            onDestroy?.Invoke();
        }

        public void TakeDamage(int dg)
        {
            HP -= dg;

            if (HP <= 0)
                Die();
            if (agent.isActiveAndEnabled)
                OnDamage?.Invoke();
        }
    }
}