using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float Speed = 10.0f;
    private void OnEnable ()
    {
        StartCoroutine (DestroyThis (5.0f));
    }
    private void Update ()
    {
        transform.Translate (Vector3.up * Time.deltaTime * Speed, Space.Self);
    }
    IEnumerator DestroyThis (float time)
    {
        yield return new WaitForSeconds (time);
        PoolingObjectsSystem.Destroy (gameObject);
    }
}
