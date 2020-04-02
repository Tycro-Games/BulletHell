using System.Collections;
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
    private void OnEnable ()
    {
        base.Start ();
    }
    public IEnumerator TakeDamage (int dg)
    {
        HP -= dg;
        if (HP <= 0)
            Die ();
        yield return null;
    }
}
