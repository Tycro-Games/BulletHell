using System.Collections;
using UnityEngine;

public class PlayerStats : CommonStats,IHitable
{
    [SerializeField]
    private float TimeToWaitUntilNextHit = .5f;
    [SerializeField]
    private bool immortal=false;

    //auxiliary
    public static bool atacked = false;
    public override void Start ()
    {
        base.Start ();
    }
    private void Update ()
    {
        Debug.Log (atacked);
    }
    public void Immortal ()
    {
        immortal = !immortal;
    }
    private void OnEnable ()
    {
        BaseEnemy.OnHit += TakeDamage;
        Projectile.OnHit += TakeDamage;
    }
    private void OnDisable ()
    {
        BaseEnemy.OnHit -= TakeDamage;
        Projectile.OnHit -= TakeDamage;
    }
    public IEnumerator TakeDamage (int dg)
    {
        if (!immortal)
        {
            HP -= dg;
            Debug.Log (HP);
            if (HP <= 0)
                Die ();
            yield return new WaitForSeconds (TimeToWaitUntilNextHit);
            atacked = false;
        }
    }
    
    

    public void Die ()
    {        
        PoolingObjectsSystem.Destroy (gameObject);
    }
}