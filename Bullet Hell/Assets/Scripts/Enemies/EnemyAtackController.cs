using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                break;
            case Attack.Melee:
                atack.DamageProximity = true;
                break;
            case Attack.Stay:
                //do nothing
                atack.DamageProximity = false;
                break;
        }
    }
}
