using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header ("Movement")]
    [SerializeField]
    private float speedMovement = 0.0f;
    Rigidbody2D rb;

    //Input
    PlayerInputActions input;
    Vector2 movement;
    Vector2 lookRotation;
    private void Awake ()
    {

        input = new PlayerInputActions ();
        input.PlayerMovement.Movement.performed += ctx => movement = ctx.ReadValue<Vector2> ();
        input.PlayerMovement.LookToShoot.performed += ctx => lookRotation = ctx.ReadValue<Vector2> ();
    }
    void Start ()
    {
        rb = GetComponent<Rigidbody2D> ();
    }

    void FixedUpdate ()
    {
        Turn ();
        Move ();


    }

    void Move ()
    {
        rb.MovePosition (rb.position + movement * Time.fixedDeltaTime * speedMovement);
    }
    void Turn ()
    {
        Vector2 input = lookRotation;
        
        if (input != Vector2.zero)
        {
            Quaternion newRotation = Quaternion.LookRotation (transform.forward, input);
            transform.rotation = newRotation;
        }
    }

    private void OnEnable ()
    {
        input.Enable ();
    }
    private void OnDisable ()
    {
        input.Disable ();
    }
}
