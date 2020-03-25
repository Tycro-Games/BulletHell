using UnityEngine;
public class EnemyAIStats : CommonStats,IHitable,IKillable
{
    //this script kills you
    public override void Start ()
    {
        base.Start ();
    }
    public void Die ()
    {
        
    }

    
    public void TakeDamage (int dg)
    {
        stats.Health -= dg;
        if (stats.Health <= 0)
            Die ();
    }


}
