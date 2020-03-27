using System.Collections;
using UnityEngine;

public class PlayerStats : CommonStats
{
    [SerializeField]
    private float TimeToWaitUntilNextHit = .5f;

    //auxiliary
    [Header("Stats")]
    public static bool CanTakeDG = true;
    public override void Start ()
    {
        base.Start ();
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
        stats.Health -= dg;
        if (stats.Health <= 0)
            Die ();
        yield return new WaitForSeconds (TimeToWaitUntilNextHit);  
    }

    public void Die ()
    {        
        PoolingObjectsSystem.Destroy (gameObject);
    }
}