using System.Collections;
using UnityEngine;
[RequireComponent(typeof(CircleCollider2D))]
public class Projectile : MonoBehaviour
{
    private LayerMask collideableLayer;
    private float speed = 10.0f;
    private float velocity = 0.0f;
    [SerializeField]
    private float thickness = 0.25f;
    private CircleCollider2D col = null;

    private int damage;
    private bool destroyed = false;
    private readonly float lifetime = 5.0f;

    public void Init(float Speed, int Damage, LayerMask Collide)
    {
        speed = Speed;
        collideableLayer = Collide;
        damage = Damage;

        destroyed = false;

        CheckStart();
    }

    private void OnEnable()
    {
        StartCoroutine(DestroyProjectileTime(lifetime));
    }
    private void OnDestroy()
    {
        DestroyProjectile();
    }
    private void Awake()
    {
        col = GetComponent<CircleCollider2D>();
    }
    private void Update()
    {
        velocity = Time.deltaTime * speed;




        transform.Translate(Vector2.up * velocity, Space.Self);

    }
    private void FixedUpdate()
    {
        if (!destroyed)
            CheckSoroundings(velocity);
    }
    public void DestroyProjectile()
    {
        if (gameObject.activeInHierarchy)
            PoolingObjectsSystem.Destroy(gameObject);
    }
    public IEnumerator DestroyProjectileTime(float lifetime)
    {
        yield return new WaitForSeconds(lifetime);
        PoolingObjectsSystem.Destroy(gameObject);
    }
    void CheckSoroundings(float veloc)
    {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, thickness, transform.up, veloc, collideableLayer);
        if (hit.collider != null)
        {
            HitObject(hit.collider, hit.point);
        }
    }
    void HitObject(Collider2D col, Vector2 pointHit)
    {
        IHitable hit = col.GetComponent<IHitable>();
        if (col.tag != "Player")
        {
            if (hit != null)
                hit.TakeDamage(damage);

        }
        else
        {
            if (hit != null && !PlayerStats.atacked)
            {
                hit.TakeDamage(damage);
            }
        }
        DestroyProjectile();
    }

    void CheckStart()
    {
        Collider2D[] colliders = new Collider2D[10];
        int cols = Physics2D.OverlapCircleNonAlloc(transform.position, thickness, colliders, collideableLayer);
        if (cols > 0)
        {
            HitObject(colliders[0], transform.position);
            destroyed = true;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, thickness);
    }


}
