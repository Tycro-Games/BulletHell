using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class CursorController : MonoBehaviour
{
    [SerializeField]
    private float radius = 5.0f;

    [SerializeField]
    private float smooth = 5.0f;

    private Vector2 velocity = Vector3.zero;

    public static Transform cursorTransform = null;

    private void Start()
    {
        cursorTransform = transform;
        Cursor.visible = false;
    }

    private void Update()
    {
        transform.position = Vector2.SmoothDamp(transform.position, MousePosition(), ref velocity, smooth);
    }

    public static Vector2 CursorPosition()
    {
        return cursorTransform.position;
    }

    public static Vector2 MousePosition()
    {
        Vector2 CursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        return CursorPos;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}