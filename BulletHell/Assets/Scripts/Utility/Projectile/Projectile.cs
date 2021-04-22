using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

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
    private float lifetime = 30f;

    private float lives = 0;
    private Rigidbody2D rb = null;

    private Light2D light = null;

    [SerializeField]
    private float limitIntensity = 3;

    [SerializeField]
    private float lightMultiplier = 1.0f;

    public void Init(float Speed, int Damage, LayerMask Collide, float life)
    {
        lives = life;
        speed = Speed;
        collideableLayer = Collide;
        damage = Damage;

        destroyed = false;
        rb = GetComponent<Rigidbody2D>();
        light = GetComponentInChildren<Light2D>();
        light.intensity = Mathf.Clamp(lives * lightMultiplier, limitIntensity, 100);
        CheckStart();
    }

    private void OnDestroy()
    {
        DestroyProjectile();
    }

    private void OnEnable()
    {
        StartCoroutine(DestroyProjectileTime(lifetime));
    }

    private void Awake()
    {
        col = GetComponent<CircleCollider2D>();
    }

    private void CheckStart()
    {
        Collider2D[] colliders = new Collider2D[1];
        int cols = Physics2D.OverlapCircleNonAlloc(transform.position, thickness, colliders, collideableLayer);
        if (cols > 0)
        {
            CheckSoroundings(thickness);
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
        StopCoroutine("DestroyProjectileTime");
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
        else if (hit.collider.CompareTag("project") && lives > 0)
        {
            Vector2 reflect = Vector2.Reflect(transform.up, hit.normal);
            float rot = 90 - Mathf.Atan2(reflect.y, reflect.x) * Mathf.Rad2Deg;
            transform.eulerAngles = -new Vector3(0, 0, rot);

            if (lives <= 0)
                DestroyProjectile();
            lives--;
            light.intensity = Mathf.Clamp(lives * lightMultiplier, limitIntensity, 100); ;
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