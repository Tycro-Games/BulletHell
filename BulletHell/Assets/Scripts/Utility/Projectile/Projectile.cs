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

    [SerializeField]
    private float lifetime = 5.0f;

    private Rigidbody2D rb = null;

    public void Init(float Speed, int Damage, LayerMask Collide)
    {
        speed = Speed;
        collideableLayer = Collide;
        damage = Damage;

        destroyed = false;
        rb = GetComponent<Rigidbody2D>();
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

    private void CheckStart()
    {
        Collider2D[] colliders = new Collider2D[10];
        int cols = Physics2D.OverlapCircleNonAlloc(transform.position, thickness, colliders, collideableLayer);
        if (cols > 0)
        {
            DestroyProjectile();
            destroyed = true;
        }
    }

    private void FixedUpdate()
    {
        velocity = Time.fixedDeltaTime * speed;

        transform.Translate(Vector2.up * velocity, Space.Self);
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

    private void CheckSoroundings(float veloc)
    {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, thickness, transform.up, veloc, collideableLayer);
        if (hit.collider != null)
        {
            HitObject(hit.collider, hit);
        }
    }

    private void HitObject(Collider2D col, RaycastHit2D hit)
    {
        IHitable hitable = col.GetComponent<IHitable>();
        if (col.tag != "Player" && hitable != null)
        {
            hitable.TakeDamage(damage);
        }
        else if (hitable != null && !PlayerStats.atacked)
        {
            hitable.TakeDamage(damage);
        }
        else if (hit.collider.CompareTag("project"))
        {
            Vector2 reflect = Vector2.Reflect(transform.up, hit.normal);
            float rot = 90 - Mathf.Atan2(reflect.y, reflect.x) * Mathf.Rad2Deg;
            transform.eulerAngles = -new Vector3(0, 0, rot);
            return;
        }

        DestroyProjectile();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, thickness);
    }
}