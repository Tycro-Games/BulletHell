using UnityEngine;
[RequireComponent(typeof(Collider2D))]
public class EnemyAIStats : CommonStats, IHitable
{
    //this script kills you
    public override void Start ()
    {
        base.Start ();
    }
    public void Die ()
    {
        PoolingObjectsSystem.Destroy (gameObject);
    }
    public void TakeDamage (int dg)
    {
        stats.Health -= dg;
        if (stats.Health <= 0)
            Die ();
    }
}
