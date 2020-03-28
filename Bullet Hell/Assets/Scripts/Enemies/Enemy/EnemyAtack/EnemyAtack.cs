using UnityEngine;
using System.Collections;
using Pathfinding;
[RequireComponent (typeof (Collider2D))]
public class EnemyAtack : MonoBehaviour
{
    //Damage PlayerEvent
    public delegate IEnumerator Onhit (int dg);
    public static event Onhit OnHit;

    private AIPath path;

    [SerializeField]
    protected Stats stats = null;

    [SerializeField]
    private bool damageProximity = true;
    private bool inRange = false;

    private void Start ()
    {
        path = GetComponentInParent<AIPath> ();

        StartCoroutine (CheckDamageProxi ());
    }
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
          //translation: the player cand take damage, is in range and you've set true damage proximity
            {
                PlayerStats.CanTakeDG = false;

                yield return StartCoroutine (OnHit (stats.Damage));

                PlayerStats.CanTakeDG = true;
            }
            yield return null;
        }

    }
}
