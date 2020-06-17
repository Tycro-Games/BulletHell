using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent (typeof (SpriteRenderer))]
public class CursorController : MonoBehaviour
{

    private PlayerInput input;
    [SerializeField]
    private float radius = 5.0f;

    [SerializeField]
    private float smooth = 5.0f;
    [SerializeField]
    private float speed = 5.0f;

    private Vector2 velocity = Vector3.zero;

    Transform cam = null;
    private void Start ()
    {
        cam = Camera.main.transform;

        //Cursor.visible = false;

        // input = GetComponentInParent<PlayerInput> ();//ref to the player input
    }
    private void Update ()
    {
        transform.position = MousePosition ();


        //transform.position = Vector2.SmoothDamp (transform.position, MousePosition (), ref velocity, smooth);

    }
    public static Vector2 MousePosition ()
    {
        Vector2 CursorPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);

        return CursorPos;
    }
    private void OnDrawGizmos ()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere (transform.position, radius);
    }

}
