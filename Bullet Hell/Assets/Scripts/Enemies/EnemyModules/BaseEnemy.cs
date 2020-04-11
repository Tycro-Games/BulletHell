using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
[RequireComponent (typeof (Collider))]
public class BaseEnemy : MonoBehaviour
{
    private NavMeshAgent agent = null;
    [SerializeField]
    private Stats stats = null;
    private float TimeBetweenAtacks = .5f;
    [SerializeField]
    private float speedRotation = 180.0f;
    private void Awake ()
    {
        agent = GetComponentInParent<NavMeshAgent> ();
    }
    void Start ()
    {
        enemyTransform = agent.transform;
    }
    private void Update ()
    {
        if (enabled)
            agent.SetDestination (StaticInfo.PlayerPos);
    }
    public void Init (bool Shoot, bool Proxy, float repathSpeed)
    {
        //ChangeRepath (repathSpeed);

        StopAllCoroutines ();
        if (Shoot)
        {
            StartCoroutine (AtackRange ());
        }
        if (Proxy)
        {
            StartCoroutine (AtackProximity ());
            damageProxy = true;
        }
    }
    private void OnDisable ()
    {
        StopAllCoroutines ();
    }
    public void ChangeRepath (float speed)
    {

    }

    #region Atack Range


    [Header ("Range")]
    [SerializeField]
    private UnityEvent OnShoot = null;
    [SerializeField]
    private float rangeToShoot = 5.0f;


    private Transform enemyTransform = null;

    public IEnumerator AtackRange ()
    {

        while (OnShoot != null && OnHit != null)
        {

            if (agent.remainingDistance <= rangeToShoot)
            {
                agent.isStopped = true;
                StartCoroutine (PointPlayer ());
                OnShoot.Invoke ();
            }
            else
            {
                agent.isStopped = false;
            }
            yield return null;
        }
    }
    IEnumerator PointPlayer ()
    {
        while (agent.isStopped)
        {

            Vector3 dir = (agent.destination - transform.position).normalized;
            Quaternion newRot = Quaternion.LookRotation (dir);

            enemyTransform.rotation = Quaternion.RotateTowards (enemyTransform.rotation, newRot, speedRotation * Time.deltaTime);
            yield return null;
        }
    }

    private void OnDrawGizmosSelected ()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere (transform.position, rangeToShoot);
    }
    #endregion

    #region atack proximity
    //Damage PlayerEvent

    public delegate void Onhit (int dg);
    public static event Onhit OnHit;

    [Header ("Damge Proximity")]
    private bool damageProxy;
    private bool inRange = false;
    private void OnTriggerEnter (Collider collision)
    {
        //Enemies layer can only interact with the player
        inRange = true;
    }
    private void OnTriggerExit (Collider collision)
    {
        //Enemies layer can only interact with the player
        inRange = false;
    }
    public IEnumerator AtackProximity ()
    {

        while (OnHit != null)
        {

            if (!PlayerStats.atacked && inRange && damageProxy && gameObject.activeSelf)
            //translation: the player cand take damage, is in range and you've set true, the damage proximity bool
            {
                OnHit (stats.Damage);
            }
            yield return new WaitForSeconds (TimeBetweenAtacks);
        }

    }
    #endregion
}
