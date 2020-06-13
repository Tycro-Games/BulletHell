using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField]
    private ProjectileObjects projectile;
    [SerializeField]
    private Transform projectiles = null;

    [SerializeField]
    private float offset = 1.0f;

    [SerializeField]
    private LayerMask CollideableMask = 0;


    private float currentTime = 0.0f;
    public void ChangeProjectile (ProjectileObjects project)
    {
        projectile = project;
    }

    public void Spawn ()
    {
        if (currentTime <= Time.time)
        {
            currentTime = Time.time + 1 / projectile.FirePerSecond;

            Projectile projectileInit = Spawner.Spawn (projectile.projectilePrefab,
                transform.position + transform.forward * offset,
                transform.rotation,
                projectiles).GetComponent<Projectile> ();
            projectileInit.Init (projectile.speed, projectile.damage, CollideableMask);
        }

    }
}
