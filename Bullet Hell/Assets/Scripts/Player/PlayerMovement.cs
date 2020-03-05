using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header ("Movement")]
    [SerializeField]
    private float speedMovement = 0.0f;
    Rigidbody2D rb;

    //Input
    Vector2 movement;
    Keyboard kb;
    private void Awake ()
    {
        kb = InputSystem.GetDevice<Keyboard> ();
    }
    void Start ()
    {
        rb = GetComponent<Rigidbody2D> ();


    }
    private void FixedUpdate ()
    {
        Move ();
    }
    public void SetMovement (InputAction.CallbackContext ctx)
    {
        movement = ctx.ReadValue<Vector2> ();
    }
    public void Move ()
    {
        rb.MovePosition (rb.position + movement * Time.fixedDeltaTime * speedMovement);
    }
    public void Turn (InputAction.CallbackContext ctx)
    {

        Vector2 input = ctx.ReadValue<Vector2> ();

        if (input != Vector2.zero)
        {
            Quaternion newRotation = Quaternion.LookRotation (transform.forward, input);
            transform.rotation = newRotation;
        }


    }

}
