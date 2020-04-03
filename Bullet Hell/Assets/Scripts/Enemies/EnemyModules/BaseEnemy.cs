using Pathfinding;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
[RequireComponent (typeof (Collider2D))]
public class BaseEnemy : MonoBehaviour
{
    private AIPath path = null;
    [SerializeField]
    private Stats stats = null;
    private float TimeBetweenAtacks = .5f;

    private void Awake ()
    {
        path = GetComponentInParent<AIPath> ();
    }
    void Start ()
    {
        enemyTransform = path.transform;
    }
    private void Update ()
    {
        path.destination = StaticInfo.PlayerPos;
    }
    public void Init (bool Shoot, bool Proxy, float repathSpeed)
    {
        ChangeRepath (repathSpeed);

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
        path.repathRate = speed;
    }

    #region Atack Range


    [Header ("Range")]
    [SerializeField]
    private UnityEvent OnShoot = null;
    [SerializeField]
    private float rangeToShoot = 5.0f;
    [SerializeField]
    private float speedRotation = 180.0f;


    private Transform enemyTransform = null;

    public IEnumerator AtackRange ()
    {

        while (OnShoot != null && OnHit != null)
        {

            if (path.remainingDistance <= rangeToShoot)
            {
                if (path.canMove)
                {
                    path.canMove = false;
                    StartCoroutine (PointPlayer ());
                }
                OnShoot.Invoke ();
            }
            else
            {
                path.canMove = true;
            }
            yield return null;
        }
    }
    IEnumerator PointPlayer ()
    {
        while (!path.canMove)
        {

            Vector3 dir = (path.destination - transform.position).normalized;
            Quaternion newRot = path.SimulateRotationTowards (dir, 360);
            if (!path.enableRotation)
                transform.rotation = Quaternion.RotateTowards (transform.rotation, newRot, speedRotation * Time.deltaTime);
            else
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
    private void OnTriggerEnter2D (Collider2D collision)
    {
        //Enemies layer can only interact with the player
        inRange = true;
    }
    private void OnTriggerExit2D (Collider2D collision)
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
