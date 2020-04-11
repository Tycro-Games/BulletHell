using System.Collections;
using UnityEngine;
[RequireComponent (typeof (SphereCollider))]
public class Projectile : MonoBehaviour
{
    private LayerMask collideableLayer;
    private float speed = 10.0f;
    private float velocity = 0.0f;
    private float thickness = 0.25f;
    private SphereCollider col = null;

    private int damage;
    private bool destroyed = false;
    private readonly float lifetime = 5.0f;

    public void Init (float Speed, int Damage, LayerMask Collide)
    {
        speed = Speed;
        collideableLayer = Collide;
        damage = Damage;

        destroyed = false;

        CheckStart ();
    }
    private void OnEnable ()
    {
        StartCoroutine (DestroyProjectileTime (lifetime));
    }
    private void Awake ()
    {
        col = GetComponent<SphereCollider> ();
        thickness = col.radius;
    }
    private void Update ()
    {
        velocity = Time.deltaTime * speed;

        
        

        transform.Translate (Vector3.forward * velocity, Space.Self);

    }
    private void FixedUpdate ()
    {
        if (!destroyed)
            CheckSoroundings (velocity);
    }
    public void DestroyProjectile ()
    {
        if (gameObject.activeInHierarchy)
            PoolingObjectsSystem.Destroy (gameObject);
    }
    public IEnumerator DestroyProjectileTime (float lifetime)
    {
        yield return new WaitForSeconds (lifetime);
        PoolingObjectsSystem.Destroy (gameObject);
    }
    void CheckSoroundings (float veloc)
    {
        RaycastHit hit;
        Ray ray = new Ray (transform.position, transform.forward);
        if (Physics.Raycast (ray, out hit, veloc + thickness, collideableLayer))
        {
            HitObject (hit.collider, hit.point);
        }
    }
    void HitObject (Collider col, Vector3 pointHit)
    {
        IHitable hit = col.GetComponent<IHitable> ();
        if (col.tag != "Player")
        {
            if (hit != null)
                hit.TakeDamage (damage);

        }
        else
        {
            if (hit != null && !PlayerStats.atacked)
            {
                hit.TakeDamage (damage);
            }
        }
        DestroyProjectile ();
    }

    void CheckStart ()
    {
        Collider[] colliders = new Collider[10];
        int cols = Physics.OverlapSphereNonAlloc (transform.position, thickness, colliders, collideableLayer);
        if (cols > 0)
        {
            HitObject (colliders[0], transform.position);
            destroyed = true;
        }
    }
    private void OnDrawGizmos ()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere (transform.position, .5f);
    }


}
