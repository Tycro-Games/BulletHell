using UnityEngine;
using System.Collections;
using Pathfinding;

[RequireComponent (typeof (Collider2D))]
public class EnemyAtack : MonoBehaviour
{
    //Damage PlayerEvent
    public delegate IEnumerator Onhit (int dg);
    public static event Onhit OnHit;
    //aux
    private Collider2D col;

    private AIPath path;

    [SerializeField]
    protected Stats stats = null;

    public bool DamageProximity = true;
    private void Start ()
    {
        path = GetComponentInParent<AIPath> ();
        col = GetComponent<Collider2D> ();
    }

    private IEnumerator OnTriggerEnter2D (Collider2D other)
    {
        if (!DamageProximity)
            yield break;
        if (other.tag == "Player" && PlayerStats.CanTakeDG)
        {
            col.enabled = false;

            PlayerStats.CanTakeDG = false;

            yield return StartCoroutine (OnHit (stats.Damage));

            PlayerStats.CanTakeDG = true;

            col.enabled = true;
        }
    }
}
