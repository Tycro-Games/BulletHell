using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public struct NonPlayerOriented
{
    public bool RandomDir;
    public bool AutoRotation;
    public bool ClockWise;

    public NonPlayerOriented(bool randomDir, bool autoRotation, bool clockWise)
    {
        RandomDir = randomDir;
        AutoRotation = autoRotation;
        ClockWise = clockWise;
    }
}

public class ShootEnemy : BaseEnemy
{
    [SerializeField]
    private Animator animator = null;

    [SerializeField]
    private bool anim = false;

    [Header("Range")]
    [SerializeField]
    private UnityEvent OnShoot = null;

    [SerializeField]
    private float rangeToShoot = 5.0f;

    [SerializeField]
    private bool StopAndShoot = false;

    [SerializeField]
    private bool Non_Moveable = false;

    [SerializeField]
    private bool NeedToFace = false;

    [Header("No Player")]
    [SerializeField]
    private bool PlayerNeed = false;

    [SerializeField]
    private bool NoDistantace = false;

    public NonPlayerOriented NonPlayer;

    [SerializeField]
    private LayerMask InteractableToFace = new LayerMask();

    [SerializeField]
    private float speedRotation = 90.0f;

    [Header("Lateral")]
    [SerializeField]
    private float speedOnLateralShoot = 90.0f;

    [SerializeField]
    private float rotateLateralTime = 3.0f;

    [SerializeField]
    private bool OnBoost = false;

    public void MakeItChangeDir()
    {
        if (!NonPlayer.RandomDir)
            return;
        if (Random.Range(0, 1) == 0)
            NonPlayer.ClockWise = !NonPlayer.ClockWise;
        else
            return;
    }

    private IEnumerator currentAtack = null;

    private void OnDisable()
    {
        currentAtack = null;
    }

    public void ToStart()
    {
        if (currentAtack == null)
        {
            currentAtack = AtackRange();
            StartCoroutine(currentAtack);
        }
    }

    public void LookAtPlayer()
    {
        if (!OnBoost)
        {
            StartCoroutine(ChangeRotation());
        }
    }

    private IEnumerator ChangeRotation()
    {
        float temp = speedRotation;
        speedRotation = speedOnLateralShoot;

        OnBoost = true;

        yield return new WaitForSeconds(rotateLateralTime);

        OnBoost = false;

        speedRotation = temp;
    }

    public IEnumerator AtackRange()
    {
        PointPlayer(true);

        while (OnShoot != null && EnemyController.HitEvent != null && agent.enabled == true)
        {
            if (PlayerNeed)//the player is needed
            {
                PointPlayer();
                if (agent.remainingDistance <= rangeToShoot || NoDistantace)//in range
                {
                    if (IsFacingPlayer() || NeedToFace) //if you face the player shoot
                    {
                        if (StopAndShoot)
                            agent.isStopped = true;

                        if (anim)
                        {
                            if (!animator.GetBool("atack"))
                                OnShoot?.Invoke();
                        }
                        else
                            OnShoot?.Invoke();
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
                PointPlayer();

                if (anim)
                {
                    if (!animator.GetBool("atack"))
                        OnShoot?.Invoke();
                }
                else
                    OnShoot?.Invoke();

                if (Non_Moveable)
                    agent.isStopped = true;
                yield return null;
            }
        }
    }

    private bool IsFacingPlayer()
    {
        RaycastHit2D ray = Physics2D.Raycast(transform.position, transform.forward, rangeToShoot, InteractableToFace);
        Debug.DrawRay(enemyTransform.position, transform.forward * rangeToShoot, Color.blue);
        if (ray.collider != null && ray.collider.CompareTag("Player"))
            return true;
        else
            return false;
    }

    private Vector2 Dir()
    {
        return (target.position - transform.position).normalized;
    }

    private void PointPlayer(bool justRot = false)
    {
        Vector2 dir;
        if (!NonPlayer.AutoRotation)
        {
            dir = Dir();
        }
        else
        {
            if (NonPlayer.ClockWise)
                dir = new Vector2(Mathf.Sin(Time.time), Mathf.Cos(Time.time));
            else
                dir = new Vector2(Mathf.Cos(Time.time), Mathf.Sin(Time.time));
        }
        Debug.DrawLine(transform.position, (Vector2)transform.position + dir);

        Rotate(justRot, dir, speedRotation);
    }

    public void Rotate(bool justRot, Vector2 dir, float speedRotation)
    {
        if (!justRot)
        {
            Quaternion newRot = Quaternion.LookRotation(dir, -Vector3.forward);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, newRot, speedRotation * Time.deltaTime);
        }
        else
        {
            transform.rotation = Quaternion.LookRotation(dir, -Vector3.forward);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, rangeToShoot);
    }
}