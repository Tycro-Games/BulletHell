using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField]
    private Transform transformToTurn = null;

    [SerializeField]
    private Vector2 Limit = Vector2.zero;

    [SerializeField]
    private Vector2 Offset = Vector2.zero;

    [SerializeField]
    private float speedMovement = 0.0f;

    private Rigidbody2D rb;

    [Header("Dash")]
    public float DashMultiplier = 1.0f;

    public float DashCooldown = 1.0f;
    [HideInInspector]
    public bool Teleported = false;
    //Input
    private Vector2 movement;

    private Vector2 move;

    public Vector2 GetMovement()
    {
        return movement;
    }

    public Vector2 GetMove()
    {
        return move;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Offset = transform.position;
        StartCoroutine(Rotate(transformToTurn));
        StartCoroutine(Dash());
    }

    public void ChangePosition(Vector2 pos, Vector2 offset)
    {
        transform.position = pos;
        Offset = offset;
    }

    private void FixedUpdate()
    {
        Vector2 pos = rb.position;
        Vector2 limits = Limit / 2;

        pos.x = Mathf.Clamp(pos.x, -limits.x + Offset.x, limits.x + Offset.x);
        pos.y = Mathf.Clamp(pos.y, -limits.y + Offset.y, limits.y + Offset.y);

        rb.position = pos;
        Move();
    }

    public void SetMovement(InputAction.CallbackContext ctx)
    {
        movement = ctx.ReadValue<Vector2>();
    }

    public IEnumerator Dash()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.position = (rb.position + DashMultiplier * movement);

                yield return new WaitForSeconds(DashCooldown);
            }
            else
                yield return null;
        }
    }

    private void Move()
    {
        move = movement;
        move *= Time.fixedDeltaTime * speedMovement;

        rb.MovePosition(rb.position + move);
    }

    private IEnumerator Rotate(Transform transformToRotate)
    {
        Vector2 dir = ((Vector2)CursorController.cursorTransform.position - (Vector2)transform.position).normalized;
        Quaternion newRotation = Quaternion.LookRotation(dir, transform.up);

        transformToRotate.rotation = newRotation;
        while (true)
        {
            dir = (CursorController.MousePosition() - (Vector2)transform.position).normalized;
            newRotation = Quaternion.LookRotation(dir, transform.up);

            transformToRotate.RotateAround(transform.position, Vector3.forward, Vector2.SignedAngle(transformToRotate.localPosition, dir));

            transformToRotate.rotation = newRotation;

            yield return null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(Offset, Limit);
    }
}