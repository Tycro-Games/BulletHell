using UnityEngine;

public class BulletSpawnerPlayer : MonoBehaviour
{
    [SerializeField]
    private Projectile projectile;
    [SerializeField]
    private Transform projectiles = null;
    public void ChangeProjectile (Projectile project)
    {
        projectile = project;
    }
    public void Spawn ()
    {
        Spawner.Spawn (projectile.projectilePrefab, transform, true, projectiles);
    }
}
