
using System.Collections;
using UnityEngine;
public class Pointer : MonoBehaviour
{
    [SerializeField]
    private bool IsRight = true;


    [SerializeField]
    private float RotSpeed = 1.0f;

    [SerializeField]
    private Transform Scale = null;


    private void Update ()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
        Vector2 dir = (mousePos - (Vector2)transform.position).normalized;

        Quaternion rot = Quaternion.LookRotation (transform.forward, dir);

        if (mousePos.x < transform.position.x && IsRight)
            Flip ();
        if (mousePos.x > transform.position.x && !IsRight)
            Flip ();

        transform.rotation = Quaternion.RotateTowards (transform.rotation, rot, RotSpeed * Time.deltaTime);
    }
    public void Flip ()
    {
        Vector3 scale = Scale.localScale;
        scale.x *= -1;
        Scale.localScale = scale;
        IsRight = !IsRight;
    }
}
