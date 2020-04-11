using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent (typeof (SpriteRenderer))]
public class CursorController : MonoBehaviour
{
    private PlayerInput input;
    [SerializeField]
    private bool RotateToPlayer = true;
    [SerializeField]
    private bool NormalizePos = true;
    [SerializeField]
    private bool LocalToPlayer = true;
    [SerializeField]
    private float radius = 5.0f;
    private void Start ()
    {
        Cursor.visible = false;
        input = GetComponentInParent<PlayerInput> ();//ref to the player input
    }
    private void Update ()
    {
        if (input.currentControlScheme == "Keyboard+Mouse")
            transform.position = MousePosition ();


        if (RotateToPlayer)
            RotateToTarget ();

        if (NormalizePos)
            transform.position = Normalize ();
    }
    void RotateToTarget ()
    {
        Vector3 relativePos = (StaticInfo.PlayerPos - transform.position).normalized;

        Quaternion rot = Quaternion.LookRotation (transform.forward, -relativePos);
        transform.rotation = rot;
    }

  public static Vector3 MousePosition ()
    {
        Vector3 CursorPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
        CursorPos.y = 0;
        return CursorPos;
    }

    Vector3 Normalize ()
    {
        Vector3 dist = transform.position - StaticInfo.PlayerPos;

        dist = Vector3.ClampMagnitude (dist, radius);
        if (LocalToPlayer)
            dist += StaticInfo.PlayerPos;
        return dist;
    }
    private void OnDrawGizmos ()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere (StaticInfo.PlayerPos, radius);
    }

}
