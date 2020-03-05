using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private LayerMask collideableLayer;
    private float speed = 10.0f;
    private float velocity = 0.0f;
    private float thickness=0.25f;

    private int damage;
    public void Init (float Speed, int Damage, LayerMask Collide, float Thickness)
    {
        speed = Speed;
        collideableLayer = Collide;
        damage = Damage;
        thickness = Thickness;
    }
    private void OnEnable ()
    {
        StartCoroutine (DestroyStart (5.0f));
    }
    private void Update ()
    {
        velocity = Time.deltaTime * speed;
        CheckSoroundings (velocity);
        transform.Translate (Vector3.up * velocity, Space.Self);
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
            hit.TakeDamage (damage);
        }
        PoolingObjectsSystem.Destroy (gameObject);
    }
    IEnumerator DestroyStart (float time)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll (transform.position, .1f, collideableLayer);
        if (colliders.Length < 0)
        {
            HitObject (colliders[0], transform.position);
        }
        yield return new WaitForSeconds (time);
        PoolingObjectsSystem.Destroy (gameObject);
    }
    private void OnDrawGizmos ()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere (transform.position, thickness);
    }


}
