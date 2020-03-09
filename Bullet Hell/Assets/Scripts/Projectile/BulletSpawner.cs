using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField]
    private ProjectileObjects projectile;
    [SerializeField]
    private Transform projectiles = null;
    [SerializeField]
    private LayerMask CollideableMask = 0;
    [SerializeField]
    private int HowManyBulletsReady = 10;
    public void ChangeProjectile (ProjectileObjects project)
    {
        projectile = project;
    }
    private void Awake ()
    {
        InitSpawn (HowManyBulletsReady);
    }
    public void Spawn ()
    {
        Projectile projectileInit = Spawner.Spawn (projectile.projectilePrefab, transform, projectiles).GetComponent<Projectile> ();
        projectileInit.Init (projectile.speed, projectile.damage, CollideableMask);
    }
    private void InitSpawn (int count)
    {
        Projectile[] projes = new Projectile[count];
        for (int i = 0; i < count; i++)
        {
            projes[i]  = Spawner.Spawn (projectile.projectilePrefab, transform, projectiles).GetComponent<Projectile> ();
        }
        for (int i = 0; i < count; i++)
        {
            projes[i].DestroyProjectile ();
        }
    }
}
