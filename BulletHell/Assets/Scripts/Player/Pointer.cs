
using System.Collections;
using UnityEngine;
public class Pointer : MonoBehaviour
{
    [SerializeField]
    private bool IsRight = true;

    [SerializeField]
    private Transform Scale = null;

    [SerializeField]
    private Transform ToRotate = null;
    private void Update ()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
        Vector2 dir = (mousePos - (Vector2)transform.position).normalized;

        Quaternion rot = Quaternion.LookRotation (transform.forward, dir);

        ToRotate.rotation = rot;

        if (mousePos.x < transform.position.x && IsRight)
            Flip ();
        if (mousePos.x > transform.position.x && !IsRight)
            Flip ();
    }
    public void Flip ()
    {
        Vector3 scale = Scale.localScale;
        scale.x *= -1;
        Scale.localScale = scale;
        IsRight = !IsRight;
    }
}
