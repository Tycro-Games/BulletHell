using System.Collections;
using UnityEngine;
[RequireComponent (typeof (CircleCollider2D))]
public class Projectile : MonoBehaviour
{
    private LayerMask collideableLayer;
    private float speed = 10.0f;
    private float velocity = 0.0f;
    private float thickness = 0.25f;
    private CircleCollider2D col = null;

    private int damage;
    public void Init (float Speed, int Damage, LayerMask Collide)
    {
        speed = Speed;
        collideableLayer = Collide;
        damage = Damage;
    }
    private void OnEnable ()
    {
        CheckStart ();
        Invoke ("DestroyProjectile", 4f);
    }
    private void Awake ()
    {
        col = GetComponent<CircleCollider2D> ();
        thickness = col.radius;
    }
    private void Update ()
    {
        velocity = Time.deltaTime * speed;
        CheckSoroundings (velocity);
        transform.Translate (Vector3.up * velocity, Space.Self);
    }
    public void DestroyProjectile ()
    {
        PoolingObjectsSystem.Destroy (gameObject);
    }
    void CheckSoroundings (float veloc)
    {
        RaycastHit2D hit = Physics2D.CircleCast (transform.position, thickness, transform.up, veloc, collideableLayer);
        if (hit.collider != null)
        {
            HitObject (hit.collider, hit.point);
        }
    }
    void HitObject (Collider2D col, Vector2 pointHit)
    {
        IHitable hit = col.GetComponent<IHitable> ();
        if (hit != null)
        {
            Debug.Log (col.name + " was hurt with " + damage + "damage");
            hit.TakeDamage (damage);
        }
        Debug.Log (col.name + " hitted");
        DestroyProjectile ();
    }
    void CheckStart ()
    {
        Collider2D[] colliders=new Collider2D[10];
        int cols=Physics2D.OverlapCircleNonAlloc (transform.position, thickness/2,colliders, collideableLayer);
        if (cols > 0)
        {
            HitObject (colliders[0], transform.position);
        }
    }
    private void OnDrawGizmos ()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere (transform.position, thickness);
    }


}
