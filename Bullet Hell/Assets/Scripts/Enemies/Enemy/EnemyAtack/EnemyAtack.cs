using UnityEngine;
using System.Collections;
using Pathfinding;
using UnityEngine.Events;

[RequireComponent (typeof (Collider2D))]
public class EnemyAtack : MonoBehaviour
{
    //Damage PlayerEvent
    public delegate IEnumerator Onhit (int dg);
    public static event Onhit OnHit;

    private AIPath path;

    [SerializeField]
    protected Stats stats = null;
    #region melee
    [SerializeField]
    private bool damageProximity = true;
    private bool inRange = false;
    #endregion
    #region range
    private Transform enemyTransform;

    [SerializeField]
    private UnityEvent OnShoot = null;
    private float rangeToShoot = 5.0f;
    private float speedRotation = 360f;
    #endregion

    private void Start ()
    {

        path = GetComponentInParent<AIPath> ();
        #region melee
        StartCoroutine (CheckDamageProxi ());
        #endregion
        #region range
        enemyTransform = path.transform;
        #endregion
    }
    #region melee
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
    private IEnumerator CheckDamageProxi ()
    {
        while (OnHit != null)
        {
            if (PlayerStats.CanTakeDG && inRange && damageProximity)
            //translation: the player cand take damage, is in range and you've set true, the damage proximity bool
            {
                PlayerStats.CanTakeDG = false;

                yield return StartCoroutine (OnHit (stats.Damage));

                PlayerStats.CanTakeDG = true;
            }
            yield return null;
        }

    }
    #endregion

    #region range
    IEnumerator StopMovement ()
    {
        while (!path.canMove)
        {
            Debug.Log (true);
            Vector3 dir = (path.destination - transform.position).normalized;
            enemyTransform.rotation = path.SimulateRotationTowards (dir, speedRotation);
            yield return null;
        }
    }
    void StartMovement ()
    {
       path.canMove = true;
    }
    #endregion


    private void Update ()
    {
        if (path.remainingDistance <= rangeToShoot)
        {
            if (path.canMove)
            {
                path.canMove = false;
                StartCoroutine (StopMovement ());
            }
            OnShoot.Invoke ();
        }
        else
        {
            StartMovement ();
        }
    }
    private void OnDrawGizmos ()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine (transform.position, transform.up * 5);
    }
}
