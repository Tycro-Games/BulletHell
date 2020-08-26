using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class EnemyAIStats : CommonStats, IHitable
{
    [SerializeField]
    private UnityEvent OnDamage = null;
    [SerializeField]
    private UnityEvent OnDead = null;
    private NavMeshAgent agent = null;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    //this script kills you
    public void Die()
    {
        OnDead?.Invoke();
        EnemyManager.currentEnemies.Remove(gameObject);
        PoolingObjectsSystem.Destroy(gameObject);
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
