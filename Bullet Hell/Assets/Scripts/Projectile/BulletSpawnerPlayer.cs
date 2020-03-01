using UnityEngine;

public class BulletSpawnerPlayer : MonoBehaviour
{
    [SerializeField]
    private ProjectileObjects projectile;
    [SerializeField]
    private Transform projectiles = null;
    public void ChangeProjectile (ProjectileObjects project)
    {
        projectile = project;
    }
    public void Spawn ()
    {
        Spawner.Spawn (projectile.projectilePrefab, transform, true, projectiles);
    }
}
