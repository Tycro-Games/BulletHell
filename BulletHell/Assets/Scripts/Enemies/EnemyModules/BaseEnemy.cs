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
        Vector2 point = (Vector2)transform.position + Random.insideUnitCircle * radius;
        Vector2 dir = (StaticInfo.PlayerPos - transform.position).normalized * DirPower;

        if (!Physics2D.OverlapCircle(point + dir, radius, Obstacles))
            agent.SetDestination(point + dir);
        else
            GoToARandomSpot(radius);
    }
    public void ChasePlayer()
    {
        agent.SetDestination(StaticInfo.PlayerPos);
    }
    public void Init(float repathSpeed, Transform EnemyTransform, NavMeshAgent Agent)
    {
        StopAllCoroutines();

        enemyTransform = EnemyTransform;
        agent = Agent;

        StartCoroutine(Repath(repathSpeed));
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
