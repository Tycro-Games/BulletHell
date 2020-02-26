using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header ("Movement")]
    [SerializeField]
    private float speedMovement = 0.0f;
    [SerializeField]
    private float gravity = 20.0f;
    Rigidbody rb;
    CharacterController characterController;
    //Input
    PlayerInput input;
    Vector2 movement;
    Vector2 lookRotation;
    private void Awake ()
    {

        input = new PlayerInput ();
        input.PlayerMovement.Movement.performed += ctx => movement = ctx.ReadValue<Vector2> ();
        input.PlayerMovement.LookToShoot.performed += ctx => lookRotation = ctx.ReadValue<Vector2> ();
    }
    void Start ()
    {
        rb = GetComponent<Rigidbody> ();
        characterController = GetComponent<CharacterController> ();
    }

    void Update ()
    {
        Turn ();
        Move ();


    }

    void Move ()
    {
        float h = movement.x;
        float v = movement.y;
        Vector3 velocity = new Vector3 (h, 0, v);
        velocity.y -= gravity * Time.deltaTime;
        characterController.Move (velocity * Time.deltaTime * speedMovement);
    }
    void Turn ()
    {
        Vector2 input = lookRotation;
        Vector3 turn = new Vector3 (input.x, 0, input.y);
        if (turn != Vector3.zero)
        {
            Quaternion newRotation = Quaternion.LookRotation (turn);
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
