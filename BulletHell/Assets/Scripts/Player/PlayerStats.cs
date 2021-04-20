using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;
using UnityEngine.Events;

public class PlayerStats : CommonStats, IHitable
{
    [SerializeField]
    private float UntilNextHit = .5f;

    [SerializeField]
    private bool immortal = false;

    //auxiliary
    public static bool atacked = false;

    private float currentTime = 0.0f;
    private float finishTime = 0.0f;

    public static event Action deathEvent;
    public static event Action<int> Hit;
    [SerializeField]
    private  UnityEvent OnHit=null;
    [SerializeField]
    private UnityEvent OnDead=null;

    private void Update()
    {
        if (atacked)
        {
            currentTime += Time.deltaTime;
            if (currentTime > finishTime)
                atacked = false;
        }
    }

    public void Immortal()
    {
        immortal = !immortal;
    }

    private void OnEnable()
    {
        EnemyController.HitEvent += TakeDamage;
    }

    private void OnDisable()
    {
        EnemyController.HitEvent -= TakeDamage;
    }

    public void TakeDamage(int dg)
    {
        if (!immortal)
        {
            if (atacked)
            {
                return;
            }
            else
            {
                //time staff
                atacked = true;

                currentTime = Time.time;
                finishTime = Time.time + UntilNextHit;

                //health staff
                HP -= dg;
                Debug.Log(HP);

                if (HP <= 0)
                {
                    Die();

                }
                else
                {
                    OnHit?.Invoke();
                    Hit?.Invoke(HP);
                }
            }
        }
    }

    public void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);//restart
        deathEvent?.Invoke();
        OnDead?.Invoke();
    }
}