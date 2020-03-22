using System.Collections;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    private Stats stats = null;
    [SerializeField]
    private float TimeToWaitUntilNextHit = .5f;

    //auxiliary
    [Header("Stats")]
    [SerializeField]
    private int statingHP=100;
    public static bool CanTakeDG = true;
    private void Start ()
    {
         stats.Health=statingHP;
    }
    private void OnEnable ()
    {
        EnemyAtack.OnHit += TakeDamage;
    }
    private void OnDisable ()
    {
        EnemyAtack.OnHit -= TakeDamage;
    }
    private IEnumerator TakeDamage (int dg)
    {
        Debug.Log (stats.Health);
        stats.Health -= dg;
        if (stats.Health <= 0)
            Debug.Log ("player dead");

        yield return new WaitForSeconds (TimeToWaitUntilNextHit);
       
    }
}
