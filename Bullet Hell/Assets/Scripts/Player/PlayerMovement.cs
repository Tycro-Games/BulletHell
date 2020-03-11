using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header ("Movement")]
    [SerializeField]
    private float speedMovement = 0.0f;
    Rigidbody2D rb;

    //Input
    private Camera cam;
    Vector2 movement;
    Mouse mouse;
    private void Awake ()
    {
        cam = Camera.main;
        mouse = InputSystem.GetDevice<Mouse> ();
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
        if (ctx.canceled == true)
            return;

        Vector2 MousePos = ctx.ReadValue<Vector2> ();
        Vector2 input = (cam.ScreenToWorldPoint (MousePos) - transform.position).normalized;
        if (input != Vector2.zero)
        {

            Quaternion newRotation = Quaternion.LookRotation (transform.forward, input);
            transform.rotation = newRotation;
        }


    }

}
