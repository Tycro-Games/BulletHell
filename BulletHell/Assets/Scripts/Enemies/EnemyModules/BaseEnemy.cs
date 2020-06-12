using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent (typeof (Collider2D))]
public class BaseEnemy : MonoBehaviour
{
    protected Transform enemyTransform = null;
    protected NavMeshAgent agent = null;

    
    private IEnumerator Repath (float time)
    {
        while (true)
        {
            if (enemyTransform.gameObject.activeInHierarchy)
                agent.SetDestination (StaticInfo.PlayerPos);
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
