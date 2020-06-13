using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent (typeof (BaseEnemy))]
public class ProximityDamage : BaseEnemy
{
    [SerializeField]
    private Stats stats = null;

    

    [Header ("Damge Proximity")]
    [SerializeField]
    protected float TimeBetweenAtacks = .5f;

    [SerializeField]
    private bool damageProxy=true;

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
    public void ToStart ()
    {
        StartCoroutine (AtackProximity ());
    }
    public IEnumerator AtackProximity ()
    {

        while (EnemyController.HitEvent != null)
        {

            if (!PlayerStats.atacked && inRange && damageProxy && gameObject.activeInHierarchy)
            //translation: the player cand take damage, is in range and you've set true, the damage proximity bool
            {
                EnemyController.HitEvent (stats.Damage);
            }
            yield return new WaitForSeconds (TimeBetweenAtacks);
        }

    }
}
