using UnityEngine;
using UnityEngine.Events;

public class BulletSpawner : MonoBehaviour
{
    private enum Direction
    {
        forw, right, back, left
    };

    [SerializeField]
    private ProjectileObjects projectile;

    [SerializeField]
    private Transform projectiles = null;

    [SerializeField]
    private float offsetX = 1.0f;

    [SerializeField]
    private float offsetY = 0;

    [SerializeField]
    private LayerMask CollideableMask = 0;

    [SerializeField]
    private Direction dir = Direction.forw;

    private Vector3 VectDir = Vector3.zero;

    private float currentTime = 0.0f;

    [Header("Raycast")]
    [SerializeField]
    private float distance = 10.0f;

    [SerializeField]
    private float radiusCircle = 1.0f;

    [SerializeField]
    private LayerMask collideable = 0;

    [SerializeField]
    private UnityEvent OnShoot = null;

    [SerializeField]
    private bool RandomTime = false;

    [SerializeField]
    private float FactorTime = 5;

    public void ChangeProjectile(ProjectileObjects project)
    {
        projectile = project;
    }

    private void Start()
    {
        if (dir == Direction.forw)
            VectDir = transform.forward;
        else if (dir == Direction.right)
            VectDir = transform.right;
        else if (dir == Direction.back)
            VectDir = -transform.forward;
        else if (dir == Direction.left)
            VectDir = -transform.right;

        transform.rotation = Quaternion.LookRotation(VectDir, Vector3.forward);
    }

    public void LineOfSight()
    {
        if (Physics2D.CircleCast(transform.position, radiusCircle, transform.forward, distance, collideable))
        {
            Spawn();
        }
    }

    public void Spawn()
    {
        if (currentTime <= Time.time)
        {
            OnShoot?.Invoke();

            currentTime = Time.time + 1 / projectile.FirePerSecond;

            if (RandomTime)
                currentTime += Mathf.Abs(Mathf.Sin(Time.time)) * FactorTime;
            // Vector2 dir = (CursorController.cursorTransform.position - transform.position).normalized;
            Projectile projectileInit = Spawner.Spawn(projectile.projectilePrefab,
                   transform.position + transform.forward * offsetX + transform.up * offsetY,
                   Quaternion.LookRotation(Vector3.forward, transform.forward),
                   projectiles).GetComponent<Projectile>();
            projectileInit.Init(projectile.speed, projectile.damage, CollideableMask, projectile.life);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * offsetX + transform.up * offsetY);
    }
}