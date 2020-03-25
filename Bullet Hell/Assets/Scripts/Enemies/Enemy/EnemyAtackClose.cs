using UnityEngine;
using Pathfinding;
using System.Collections;

public class EnemyAtackClose : MonoBehaviour
{
    private AIPath path;

    [SerializeField]
    private Stats stats = null;

    //Damage PlayerEvent
    public delegate IEnumerator Onhit (int dg);
    public static event Onhit OnHit;

    //aux
    private Collider2D col;
    private void Start ()
    {
        col = GetComponent<Collider2D> ();
        path = GetComponentInParent<AIPath> ();
    }

    private IEnumerator OnTriggerEnter2D (Collider2D other)
    {
        if (other.tag == "Player"&& PlayerStats.CanTakeDG)
        {
            col.enabled = false;

            PlayerStats.CanTakeDG = false;
            
            yield return StartCoroutine (OnHit (stats.Damage));

            PlayerStats.CanTakeDG = true;

            col.enabled = true;
        }
    }
}
