using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    enum Direction
    {
        forw, right, back, left
    };

    [SerializeField]
    private ProjectileObjects projectile;
    [SerializeField]
    private Transform projectiles = null;

    [SerializeField]
    private float offset = 1.0f;

    [SerializeField]
    private LayerMask CollideableMask = 0;

    [SerializeField]
    private Direction dir = Direction.forw;

    [SerializeField]
    private bool Custom = false;

    private Vector3 VectDir = Vector3.zero;

    private float currentTime = 0.0f;
    public void ChangeProjectile (ProjectileObjects project)
    {
        projectile = project;
    }
    private void Awake ()
    {
        if (!Custom)
        {
            if (dir == Direction.forw)
                VectDir = transform.forward;
            else if (dir == Direction.right)
                VectDir = transform.right;
            else if (dir == Direction.back)
                VectDir = -transform.forward;
            else if (dir == Direction.left)
                VectDir = -transform.right;
        }
        else
        {

        }
        transform.rotation = Quaternion.LookRotation (VectDir);
    }
    private void Start ()
    {
        projectiles = GameObject.FindGameObjectWithTag ("PRojectORg").transform;
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
    private void OnDrawGizmosSelected ()
    {
        Gizmos.color = Color.yellow;
        
    }
}
