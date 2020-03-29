﻿using UnityEngine;

public class EnemyAtackController : MonoBehaviour
{
    [SerializeField]
    private AIEnemyAtack AtackType = null;

    private EnemyAtack atack;
    private void OnEnable ()
    {
        AtackType.OnChangeAtack.AddListener (ChangeOfAtack);
    }
    private void OnDisable ()
    {
        AtackType.OnChangeAtack.RemoveListener (ChangeOfAtack);
    }
    private void Start ()
    {
        atack = GetComponentInChildren<EnemyAtack> ();
        ChangeOfAtack ();
    }
    public void ChangeOfAtack ()
    {
        switch (AtackType.TypeOfAtack)
        {
            case Attack.Ranged:
                //range attack
                Debug.Log ("range");
                break;
            
        }
    }
}