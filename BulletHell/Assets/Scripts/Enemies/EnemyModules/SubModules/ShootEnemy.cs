using System.Collections;
using UnityEngine;
using UnityEngine.Events;
[System.Serializable]
public struct NonPlayerOriented
{
    public bool RandomDir;
    public bool AutoRotation;
    public bool ClockWise;
    public NonPlayerOriented (bool randomDir, bool autoRotation, bool clockWise)
    {
        RandomDir = randomDir;
        AutoRotation = autoRotation;
        ClockWise = clockWise;
    }
}
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
    [Header ("No Player")]
    [SerializeField]
    private bool PlayerNeed = false;
    [SerializeField]
    private bool NoDistantace = false;
    public NonPlayerOriented NonPlayer;

    [SerializeField]
    private LayerMask TrueIfFacing = new LayerMask ();
    [SerializeField]
    private float speedRotation = 90.0f;
    [Header ("Lateral")]
    [SerializeField]
    private float speedOnLateralShoot = 90.0f;
    [SerializeField]
    private float rotateLateralTime = 3.0f;
    [SerializeField]
    private bool OnBoost = false;

    public void MakeItChangeDir ()
    {
        if (!NonPlayer.RandomDir)
            return;
        if (Random.Range (0, 1) == 0)
            NonPlayer.ClockWise = !NonPlayer.ClockWise;
        else
            return;
    }
    public void ToStart ()
    {
        PointPlayer (true);

        StartCoroutine (AtackRange ());
    }

    public void LookAtPlayer ()
    {
        if (!OnBoost)
        {
            StartCoroutine (ChangeRotation ());
        }
    }
    IEnumerator ChangeRotation ()
    {
        float temp = speedRotation;
        speedRotation = speedOnLateralShoot;

        OnBoost = true;

        yield return new WaitForSeconds (rotateLateralTime);

        OnBoost = false;

        speedRotation = temp;
    }
    public IEnumerator AtackRange ()
    {
        while (OnShoot != null && EnemyController.HitEvent != null)
        {
            if (PlayerNeed)//the player is needed
            {
                if (agent.remainingDistance <= rangeToShoot || NoDistantace)//in range
                {
                    PointPlayer ();

                    if (IsFacingPlayer () || NeedToFace) //if you face the player shoot
                    {
                        agent.isStopped = true;
                        OnShoot?.Invoke ();
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
            else //behavior if you just want to shoot
            {
                PointPlayer ();
                OnShoot?.Invoke ();

                if (Non_Moveable)
                    agent.isStopped = true;
                yield return null;
            }
        }
    }
    bool IsFacingPlayer ()
    {
        RaycastHit2D ray = Physics2D.Raycast (transform.position, transform.forward, rangeToShoot, TrueIfFacing);
        Debug.DrawRay (enemyTransform.position, enemyTransform.forward * rangeToShoot, Color.blue);
        if (ray.collider != null)
            return true;
        else
            return false;
    }
    private Vector2 Dir ()
    {
        Vector2 target = (Vector2)StaticInfo.PlayerPos + StaticInfo.VelocityOfPlayer * SpeedOfPrefire;
        return (target - (Vector2)transform.position).normalized;
    }
    void PointPlayer (bool justRot = false)
    {
        Vector2 dir;
        if (!NonPlayer.AutoRotation)
        {
            dir = Dir ();
        }
        else
        {
            if (NonPlayer.ClockWise)
                dir = new Vector2 (Mathf.Sin (Time.time), Mathf.Cos (Time.time));
            else
                dir = new Vector2 (Mathf.Cos (Time.time), Mathf.Sin (Time.time));
        }
        Debug.DrawLine (transform.position, (Vector2)transform.position + dir);

        Rotate (justRot, dir, speedRotation);
    }
    public void Rotate (bool justRot, Vector2 dir, float speedRotation)
    {
        if (!justRot)
        {
            Quaternion newRot = Quaternion.LookRotation (dir, transform.up);

            transform.rotation = Quaternion.RotateTowards (transform.rotation, newRot, speedRotation * Time.deltaTime);
        }
        else
        {
            transform.rotation = Quaternion.LookRotation (dir, transform.up);
        }
    }

    private void OnDrawGizmosSelected ()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere (transform.position, rangeToShoot);
    }

}
