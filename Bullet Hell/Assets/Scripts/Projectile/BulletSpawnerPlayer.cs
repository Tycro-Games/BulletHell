using UnityEngine;

public class BulletSpawnerPlayer : MonoBehaviour
{
    [SerializeField]
    private ProjectileObjects projectile;
    [SerializeField]
    private Transform projectiles = null;
    [SerializeField]
    private LayerMask CollideableMask = 0;
    public void ChangeProjectile (ProjectileObjects project)
    {
        projectile = project;
    }
    public void Spawn ()
    {
        Projectile projectileInit = Spawner.Spawn (projectile.projectilePrefab, transform, true, projectiles).GetComponent<Projectile> ();
        projectileInit.Init (projectile.speed, projectile.damage, CollideableMask,projectile.Thickness);
    }
}
