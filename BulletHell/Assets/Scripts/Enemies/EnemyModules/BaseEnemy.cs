using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class BaseEnemy : MonoBehaviour
{
    protected Transform enemyTransform = null;
    protected NavMeshAgent agent = null;

    [SerializeField]
    private LayerMask Obstacles = 0;

    [SerializeField]
    private UnityEvent OnRepath = null;

    [SerializeField]
    private float DirPower = 1.0f;

    private bool chasing = false;
    protected Transform target;

    private IEnumerator Repath(float time)
    {
        while (true)
        {
            if (enemyTransform.gameObject.activeInHierarchy)
            {
                OnRepath.Invoke();
            }
            yield return new WaitForSeconds(time);
        }
    }

    public void GoToARandomSpot(float radius)
    {
        if (chasing)
            return;
        Vector2 point = (Vector2)transform.position + Random.insideUnitCircle * radius;

        Vector2 dir = (target.position - transform.position).normalized * DirPower;

        Vector2 newpos = point + dir;
        agent.isStopped = false;
        if (!Physics2D.OverlapCircle(newpos, radius, Obstacles))
            agent.SetDestination(newpos);
        else
            GoToARandomSpot(radius - .5f);
    }

    public void ChasePlayer()
    {
        chasing = true;
        agent.SetDestination(target.position);
    }

    public void Init(Transform Target, float repathSpeed, Transform EnemyTransform, NavMeshAgent Agent)
    {
        StopAllCoroutines();

        target = Target;
        enemyTransform = EnemyTransform;
        agent = Agent;

        StartCoroutine(Repath(repathSpeed));
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}