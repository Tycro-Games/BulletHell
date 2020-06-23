using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent (typeof (Collider2D))]
public class BaseEnemy : MonoBehaviour
{
    protected Transform enemyTransform = null;
    protected NavMeshAgent agent = null;

    [SerializeField]
    private UnityEvent OnRepath = null;
    
    private IEnumerator Repath (float time)
    {
        while (true)
        {
            if (enemyTransform.gameObject.activeInHierarchy)
            {
                agent.SetDestination (StaticInfo.PlayerPos);
                OnRepath.Invoke ();
            }
            yield return new WaitForSeconds (time);
        }
    }
    public void Init (float repathSpeed, Transform EnemyTransform, NavMeshAgent Agent)
    {
        StopAllCoroutines ();

        enemyTransform = EnemyTransform;
        agent = Agent;

        StartCoroutine (Repath (repathSpeed));
    }
    
    private void OnDisable ()
    {
        StopAllCoroutines ();
    }
}
