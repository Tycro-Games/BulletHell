using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class PlayerMovement : MonoBehaviour
{
    [Header ("Movement")]
    [SerializeField]
    private Transform transformToTurn = null;
    [SerializeField]
    private float speedMovement = 0.0f;
    Rigidbody2D rb;

    [Header ("Dash")]
    public float DashMultiplier = 1.0f;
    public float DashCooldown = 1.0f;

    //Input
    private Vector2 movement;
    private Vector2 move;
    public Vector2 GetMovement ()
    {
        return movement;
    }
    void Start ()
    {


        rb = GetComponent<Rigidbody2D> ();

        StartCoroutine (Rotate (transformToTurn));
        StartCoroutine (Dash ());
    }
    private void FixedUpdate ()
    {
        Move ();
    }
    public void SetMovement (InputAction.CallbackContext ctx)
    {
        movement = ctx.ReadValue<Vector2> ();
    }
    public IEnumerator Dash ()
    {
        while (true)
        {
            if (Input.GetKeyDown (KeyCode.Space))
            {
                rb.position = (rb.position + DashMultiplier * movement);

                yield return new WaitForSeconds (DashCooldown);
            }
            else
                yield return null;
        }

    }
    private void Move ()
    {
        move = movement;
        move *= Time.fixedDeltaTime * speedMovement;

        rb.MovePosition (rb.position + move);

    }
    IEnumerator Rotate (Transform transformToRotate)
    {

        Vector2 dir = (CursorController.MousePosition () - (Vector2)transform.position).normalized;
        Quaternion newRotation = Quaternion.LookRotation (dir, transform.up);

        transformToRotate.rotation = newRotation;
        while (true)
        {
             dir = (CursorController.MousePosition () - (Vector2)transform.position).normalized;
             newRotation = Quaternion.LookRotation (dir, transform.up);

            transformToRotate.RotateAround (transform.position, Vector3.forward, Vector2.SignedAngle (transformToRotate.localPosition, dir));

            transformToRotate.rotation = newRotation;

            yield return null;
        }
    }


}
