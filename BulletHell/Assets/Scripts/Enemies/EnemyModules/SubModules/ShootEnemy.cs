using System.Collections;
using UnityEngine;
using UnityEngine.Events;
[RequireComponent (typeof (ShootEnemy))]
public class ShootEnemy : BaseEnemy
{
    [Header ("Range")]
    [SerializeField]
    private UnityEvent OnShoot = null;
    [SerializeField]
    private float rangeToShoot = 5.0f;
    [SerializeField]
    private float SpeedOfPrefire = 1.5f;
    [SerializeField]
    private bool StopAndShoot = false;
    [SerializeField]
    private bool Non_Moveable = false;
    [SerializeField]
    private bool NeedToFace = false;
    [SerializeField]
    private bool autoRotation = false;
    [SerializeField]
    private bool ClockWise = false;
    [SerializeField]
    private LayerMask TrueIfFacing = new LayerMask ();
    [SerializeField]
    private float speedRotation = 180.0f;


    public void ToStart ()
    {
        StartCoroutine (AtackRange ());
    }
    public IEnumerator AtackRange ()
    {

        while (OnShoot != null && EnemyController.HitEvent != null)
        {
            if (agent.remainingDistance <= rangeToShoot)//in range
            {
                PointPlayer ();

                if (IsFacingPlayer () || NeedToFace) //if you face the player shoot
                {
                    agent.isStopped = true;
                    OnShoot.Invoke ();
                }
                else if (!StopAndShoot)
                {
                    agent.isStopped = false;//go to player
                }
            }
            else
                agent.isStopped = false;//go to player

            if (Non_Moveable)
                agent.isStopped = true;
            yield return null;

        }
    }
    bool IsFacingPlayer ()
    {
        RaycastHit2D ray = Physics2D.Raycast (enemyTransform.position, enemyTransform.forward, rangeToShoot, TrueIfFacing);
        Debug.DrawRay (enemyTransform.position, enemyTransform.forward * rangeToShoot, Color.blue);
        if (ray.collider != null)
            return true;
        else
            return false;
    }
    void PointPlayer ()
    {
        Vector2 dir;
        if (!autoRotation)
        {
            Vector2 target = (Vector2)StaticInfo.PlayerPos + StaticInfo.VelocityOfPlayer * SpeedOfPrefire;
            dir = (target - (Vector2)enemyTransform.position).normalized;
        }
        else
        {
            if (ClockWise)
                dir = new Vector2 (Mathf.Sin (Time.time), Mathf.Cos (Time.time));
            else
                dir = new Vector2 (Mathf.Cos (Time.time), Mathf.Sin (Time.time));
        }
        Debug.DrawLine (enemyTransform.position, (Vector2)enemyTransform.position + dir);

        Quaternion newRot = Quaternion.LookRotation (dir, enemyTransform.up);

        enemyTransform.rotation = Quaternion.RotateTowards (enemyTransform.rotation, newRot, speedRotation * Time.deltaTime);
    }
    private void OnDrawGizmosSelected ()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere (transform.position, rangeToShoot);
    }
}
