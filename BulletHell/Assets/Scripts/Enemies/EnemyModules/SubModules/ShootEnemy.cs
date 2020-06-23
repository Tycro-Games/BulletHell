﻿using System.Collections;
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
        StartCoroutine (AtackRange ());
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
            else //behavior if you just want to shoot
            {
                PointPlayer ();
                OnShoot.Invoke ();

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
    void PointPlayer ()
    {
        Vector2 dir;
        if (!NonPlayer.AutoRotation)
        {
            Vector2 target = (Vector2)StaticInfo.PlayerPos + StaticInfo.VelocityOfPlayer * SpeedOfPrefire;
            dir = (target - (Vector2)transform.position).normalized;
        }
        else
        {
            if (NonPlayer.ClockWise)
                dir = new Vector2 (Mathf.Sin (Time.time), Mathf.Cos (Time.time));
            else
                dir = new Vector2 (Mathf.Cos (Time.time), Mathf.Sin (Time.time));
        }
        Debug.DrawLine (transform.position, (Vector2)transform.position + dir);

        Quaternion newRot = Quaternion.LookRotation (dir, transform.up);

        transform.rotation = Quaternion.RotateTowards (transform.rotation, newRot, speedRotation * Time.deltaTime);
    }
    private void OnDrawGizmosSelected ()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere (transform.position, rangeToShoot);
    }
}
