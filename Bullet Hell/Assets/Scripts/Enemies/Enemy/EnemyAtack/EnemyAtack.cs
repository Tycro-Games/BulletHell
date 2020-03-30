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
    [SerializeField]
    private float rangeToShoot = 5.0f;
    [SerializeField]
    private float speedRotation = 180.0f;
    #endregion
    private void Awake ()
    {
        path = GetComponentInParent<AIPath> ();
    }
    private void Start ()
    {

        
        #region range
        enemyTransform = path.transform;
        #endregion
    }
    public void ChangeRepath (float speed)
    {
        path.repathRate = speed;
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
    public IEnumerator CheckDamageProxi ()
    {
        bool atacked = false;
        while (OnHit != null)
        {
            if (!atacked && inRange && damageProximity)
            //translation: the player cand take damage, is in range and you've set true, the damage proximity bool
            {
                atacked = true;

                yield return StartCoroutine (OnHit (stats.Damage));

                atacked = false;
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
            Vector3 dir = (path.destination - transform.position).normalized;
            Quaternion newRot = path.SimulateRotationTowards (dir, 360);
            enemyTransform.rotation = Quaternion.RotateTowards(enemyTransform.rotation,newRot, speedRotation*Time.deltaTime);
            yield return null;
        }
    }
    void StartMovement ()
    {
        path.canMove = true;
    }
    #endregion

   public IEnumerator AtackRange ()
    {
        while (OnShoot != null)
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
            yield return null;
        }
    }
    private void OnDrawGizmos ()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine (transform.position, transform.up * 5);
    }
}
